using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : MonoBehaviour
{
    #region Properties
    [Header("MOVEMENT")]
    public Vector3 moveDirection;
    public float speed;
    public Rigidbody rb;
    public bool blockPlayerMovement;
    public GameObject mesh;
    public InputManager inputManager;

    [Header("DASH")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private bool isDashing = false;
    private bool canDash = true;

    [Header("STATS")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private int maxExperience;
    [SerializeField] private int currentExperience;

    [Header("PROJECTILE SYSTEM")]
    public Vector3 aimDirection;
    [SerializeField] List<Projectile> fixedProjectileList = new List<Projectile>();
    [SerializeField] List<Projectile> localProjectileList = new List<Projectile>();
    [SerializeField] List<Projectile> goneProjectileList = new List<Projectile>();
    [SerializeField] Projectile currentProjectile;
    [SerializeField] Transform projectileHolder;

    [Header("FX")]
    public FXList playerFX;
    #endregion

    #region Methods
    public void AimInputSystem(InputAction.CallbackContext context)
    {
        Aim();
    }

    public void Aim()
    {
        if (localProjectileList.Count == 0 || currentProjectile != null) return; //will have to show empty projectile list to player
        currentProjectile = localProjectileList[0];
        localProjectileList.Remove(currentProjectile);
        currentProjectile.ChangeMod(Projectile.ProjectileMode.Aiming);
        currentProjectile.transform.position = projectileHolder.position;
    }

    public void ShootInputSystem(InputAction.CallbackContext context)
    {
        if (currentProjectile == null) return; //will have to show empty projectile list to player
        currentProjectile.transform.SetParent(null);
        currentProjectile.currentSpeed = 2000;
        currentProjectile.ChangeMod(Projectile.ProjectileMode.Physic);
        goneProjectileList.Add(currentProjectile);
        currentProjectile.currentPlayer = this;
        currentProjectile = null;
        CinemachineShake.Instance.ShakeCamera(5, .2f);
    }

    public void Recall(InputAction.CallbackContext context)
    {
        if (goneProjectileList.Count == 0) return;
        goneProjectileList[0].Recall();
        goneProjectileList.Remove(goneProjectileList[0]);
    }

    public void Teleport(InputAction.CallbackContext context)
    {
        if (goneProjectileList.Count == 0) return;
        transform.position = goneProjectileList[0].teleportPoint.position;
        goneProjectileList[0].Recall();
        Instantiate(playerFX.teleportParticle, transform);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (!canDash) return;
        isDashing = true;
        canDash = false;
        Debug.Log("dash");
        StartCoroutine(StopDashing());
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        canDash = true;
        isDashing = false;
    }

    public void GetProjectile(Projectile _projectile)
    {
        if (localProjectileList.Contains(_projectile)) return;
        localProjectileList.Add(_projectile);
        _projectile.transform.SetParent(projectileHolder);
        if (!fixedProjectileList.Contains(_projectile))
            fixedProjectileList.Add(_projectile);
    }

    public void Damage(float _damage)
    {
        Instantiate(playerFX.bloodParticle, transform).transform.position = transform.position;
        CinemachineShake.Instance.ShakeCamera(3, .1f);
        currentHealth -= _damage;
        GPCtrl.Instance.UICtrl.healthBar.SetBarValue(currentHealth, maxHealth);
        if (currentHealth <= 0)
            Death();
    }

    public void Death()
    {
        Debug.Log("DEATH");
    }

    public void Experience(int _experience)
    {
        currentExperience += _experience;
        GPCtrl.Instance.UICtrl.experienceBar.SetBarValue(currentExperience, maxExperience);
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Debug.Log("LEVEL UP");
        maxExperience *= 2;
        currentExperience = 0;
        GPCtrl.Instance.UICtrl.levelUpMenu.OpenMenu();
        GPCtrl.Instance.UICtrl.experienceBar.SetBarValue(currentExperience, maxExperience);
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
        if (blockPlayerMovement) return;
        moveDirection = new Vector3(inputManager.Player.MoveDirection.ReadValue<Vector2>().x, 0, inputManager.Player.MoveDirection.ReadValue<Vector2>().y).normalized;
        aimDirection = new Vector3(inputManager.Player.AimDirection.ReadValue<Vector2>().x, 0, inputManager.Player.AimDirection.ReadValue<Vector2>().y).normalized;
        if (aimDirection != Vector3.zero) mesh.transform.forward = aimDirection;
        if (currentProjectile != null) currentProjectile.transform.forward = mesh.transform.forward;

    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            rb.velocity = new Vector3(moveDirection.x * dashSpeed, rb.velocity.y, moveDirection.z * dashSpeed);
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProjectileDrop>() != null)
        {
            other.GetComponent<ProjectileDrop>().projectile.currentPlayer = this;
        }
        if(other.GetComponent<Drop>() != null)
        {
            other.GetComponent<Drop>().currentPlayer = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
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
