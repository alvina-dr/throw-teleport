using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Properties
    [Header("MOVEMENT")]
    public Vector3 moveDirection;
    public float currentSpeed;
    public float runningSpeed;
    public float aimingSpeed;
    public Rigidbody rb;
    public bool blockPlayerMovement;
    public GameObject mesh;
    public InputManager inputManager;
    public Animator animator;

    [Header("DASH")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private bool isDashing = false;
    private bool canDash = true;

    [Header("STATS")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [Header("PROJECTILE SYSTEM")]
    public Vector3 aimDirection;
    [SerializeField] List<Projectile> fixedProjectileList = new List<Projectile>();
    [SerializeField] List<Projectile> localProjectileList = new List<Projectile>();
    [SerializeField] List<Projectile> goneProjectileList = new List<Projectile>();
    [SerializeField] Projectile currentProjectile;
    [SerializeField] Transform projectileHolder;

    [Header("FX")]
    public FX_Player playerFX;
    [SerializeField] private BlinkColor blinkColor;
    [SerializeField] private Transform smokeSource;

    [Header("SOUND")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource walkAudioSource;
    private int shootIterator = 0;

    [Header("INTERACTION SYSTEM")]
    private List<Interaction> interactionList = new List<Interaction>();
    #endregion

    #region Methods
    public void AimInputSystem(InputAction.CallbackContext context)
    {
        Aim();
    }

    public void Aim()
    {
        if (blockPlayerMovement) return;
        if (interactionList.Count > 0) return;
        if (localProjectileList.Count == 0 || currentProjectile != null) return; //will have to show empty projectile list to player
        currentProjectile = localProjectileList[0];
        localProjectileList.Remove(currentProjectile);
        currentProjectile.ChangeMod(Projectile.ProjectileMode.Aiming);
        currentProjectile.transform.position = projectileHolder.position;
        animator.SetBool("Aiming", true);
    }

    public void ShootInputSystem(InputAction.CallbackContext context)
    {
        if (blockPlayerMovement) return;
        if (interactionList.Count > 0)
        {
            interactionList[interactionList.Count - 1].Interact();
        }
        else
        {
            if (currentProjectile == null) return; //will have to show empty projectile list to player
            currentProjectile.transform.SetParent(null);
            currentProjectile.currentSpeed = 2000;
            currentProjectile.ChangeMod(Projectile.ProjectileMode.Physic);
            goneProjectileList.Add(currentProjectile);
            currentProjectile.currentPlayer = this;
            currentProjectile = null;
            CinemachineShake.Instance.ShakeCamera(5, .2f);
            animator.SetBool("Aiming", false);
            audioSource.clip = GPCtrl.Instance.GeneralData.soundList.shootSoundList[shootIterator];
            shootIterator++;
            audioSource.Play();
            if (shootIterator >= GPCtrl.Instance.GeneralData.soundList.shootSoundList.Count) shootIterator = 0;
        }
    }

    public void Recall(InputAction.CallbackContext context)
    {
        if (blockPlayerMovement) return;
        if (goneProjectileList.Count == 0) return;
        goneProjectileList[0].Recall();
        goneProjectileList.Remove(goneProjectileList[0]);
    }

    public void Teleport(InputAction.CallbackContext context)
    {
        if (blockPlayerMovement) return;
        if (!PermanentDataHolder.Instance.currentAbilities.abilityTeleport) return;
        if (goneProjectileList.Count == 0) return;
        transform.position = goneProjectileList[0].teleportPoint.position;
        goneProjectileList[0].Recall();
        Instantiate(playerFX.teleportParticle, transform);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (blockPlayerMovement) return;
        if (!PermanentDataHolder.Instance.currentAbilities.abilityDash) return;
        if (!canDash) return;
        isDashing = true;
        canDash = false;
        DOTween.To(() => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView, x => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView = x, 24, .2f);
        StartCoroutine(StopDashing());
        Instantiate(playerFX.dashParticle, smokeSource).transform.position = smokeSource.transform.position;
        animator.SetTrigger("Dashing");
        audioSource.clip = GPCtrl.Instance.GeneralData.soundList.playerDash;
        audioSource.Play();
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        DOTween.To(() => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView, x => CinemachineShake.Instance.cinemachineVirtualCamera.m_Lens.FieldOfView = x, 23, .2f);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void GetProjectile(Projectile _projectile)
    {
        Debug.Log("GET PROJECTILE");
        if (localProjectileList.Contains(_projectile)) return;
        localProjectileList.Add(_projectile);
        _projectile.transform.SetParent(projectileHolder);
        if (!fixedProjectileList.Contains(_projectile))
            fixedProjectileList.Add(_projectile);
    }

    public void Damage(float _damage)
    {
        if (currentHealth <= 0) return;
            Instantiate(playerFX.damageParticle, transform).transform.position = transform.position;
        CinemachineShake.Instance.ShakeCamera(3, .1f);
        currentHealth -= _damage;
        GPCtrl.Instance.UICtrl.healthBar.SetBarValue(currentHealth, maxHealth);
        GPCtrl.Instance.UICtrl.healthCount.SetText(currentHealth.ToString() + "/" + maxHealth.ToString());
        if (currentHealth <= 0)
            Death();
        else
            blinkColor.Blink();
    }

    public void Death()
    {
        Debug.Log("DEATH");
        blockPlayerMovement = true;
        animator.SetTrigger("Dying");
        DOVirtual.DelayedCall(.8f, () => {
            PermanentDataHolder.Instance.FadeIn(() =>
            {
                PermanentDataHolder.Instance.currentMaterial = 0;
                PermanentDataHolder.Instance.enemyKilledID.Clear();
                SceneManager.LoadScene("Base");
                PermanentDataHolder.Instance.FadeOut();
            });
        });

        //death animation
        //show game over text
        //game over screen, back to base
    }
    #endregion

    #region UnityAPI
    private void Awake()
    {
        inputManager = new InputManager();
    }

    private void Start()
    {
        GPCtrl.Instance.UICtrl.healthBar.SetBarValue(currentHealth, maxHealth);
        GPCtrl.Instance.UICtrl.healthCount.SetText(currentHealth.ToString() + "/" + maxHealth.ToString());
        if (!PermanentDataHolder.Instance.hasProjectile)
        {
            Destroy(currentProjectile);
            currentProjectile = null;
        }
    }

    private void OnEnable()
    {
        inputManager.Enable();
        inputManager.Player.Shoot.started += AimInputSystem;
        inputManager.Player.Shoot.canceled += ShootInputSystem;
        inputManager.Player.Recall.performed += Recall;
        inputManager.Player.Teleport.performed += Teleport;
        inputManager.Player.Dash.performed += Dash;
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }

    void Update()
    {
        if (blockPlayerMovement)
        {
            moveDirection = Vector3.zero;
            return;
        }
        moveDirection = new Vector3(inputManager.Player.MoveDirection.ReadValue<Vector2>().x, 0, inputManager.Player.MoveDirection.ReadValue<Vector2>().y).normalized;
        aimDirection = new Vector3(inputManager.Player.AimDirection.ReadValue<Vector2>().x, 0, inputManager.Player.AimDirection.ReadValue<Vector2>().y).normalized;
        if (currentProjectile == null) aimDirection = moveDirection; //if player not looking anywhere, look where it goes

        //Rotation system
        if (aimDirection != Vector3.zero && currentProjectile == null) mesh.transform.forward = Vector3.RotateTowards(mesh.transform.forward, aimDirection, 10 * Time.deltaTime, 0);
        else if (aimDirection != Vector3.zero && currentProjectile != null) mesh.transform.forward = aimDirection;

        if (currentProjectile != null) //IS AIMING
        {
            currentSpeed = aimingSpeed;
            currentProjectile.transform.forward = mesh.transform.forward;
            if (moveDirection != Vector3.zero) animator.speed = 1; // moving
            else animator.speed = 0;
            walkAudioSource.pitch = 0.7f;
        } else
        {
            currentSpeed = runningSpeed;
            animator.speed = 1;
            walkAudioSource.pitch = 1;
        }

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("Running", true);
            walkAudioSource.clip = GPCtrl.Instance.GeneralData.soundList.footstepNormal;
            if (!walkAudioSource.isPlaying) walkAudioSource.Play();
        }
        else
        {
            animator.SetBool("Running", false);
            if (walkAudioSource.isPlaying) walkAudioSource.Stop();
        }
    }
    private void FixedUpdate()
    {
        if(isDashing)
        {
            rb.velocity = new Vector3(moveDirection.x * dashSpeed, rb.velocity.y, moveDirection.z * dashSpeed);
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileDrop>() != null) //PROJECTILE
        {
            other.GetComponent<ProjectileDrop>().projectile.currentPlayer = this;
        }
        if(other.GetComponent<Drop>() != null) //DROP
        {
            other.GetComponent<Drop>().currentPlayer = this;
        }
        if (other.GetComponent<Interaction>() != null)
        {
            if (!interactionList.Contains(other.GetComponent<Interaction>()))
            {
                Debug.Log("interaction add to list : " + other.GetComponent<Interaction>().name);
                interactionList.Add(other.GetComponent<Interaction>());
                GPCtrl.Instance.UICtrl.callToAction.ShowCallToAction(other.transform.position);
                GPCtrl.Instance.UICtrl.inputIndication.ShowInputIndication();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Interaction _interaction = other.GetComponent<Interaction>();
        if (_interaction != null)
        {
            if(interactionList.Contains(_interaction))
            {
                if (other.GetComponent<Interaction>() == interactionList[interactionList.Count - 1] && interactionList.Count > 1)
                {
                    interactionList.Remove(_interaction);
                    GPCtrl.Instance.UICtrl.callToAction.ShowCallToAction(interactionList[interactionList.Count - 1].transform.position);
                    GPCtrl.Instance.UICtrl.inputIndication.ShowInputIndication();
                } else
                {
                    interactionList.Remove(_interaction);
                    GPCtrl.Instance.UICtrl.callToAction.HideCallToAction();
                    GPCtrl.Instance.UICtrl.inputIndication.HideInputIndication();
                }
            }
        }
    }
    #endregion

    #region Classes
    public class ProjectileEntry
    {
        public Projectile projectile;
        public bool inInventory;
    }
    #endregion
}
