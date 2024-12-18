using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    #region Properties
    public enum ProjectileMode
    {
        Physic = 0,
        Drop = 1,
        Inventory = 2,
        Recall = 3,
        Aiming = 4,
        StartInventory = 5
    }

    [SerializeField] ProjectileMode projectileMode;
    public Player currentPlayer;

    [Header("PHYSIC")]
    [SerializeField] GameObject physic;
    [SerializeField] Rigidbody rb;
    public float currentSpeed;
    public Vector3 direction;
    [SerializeField] private ParticleSystem hitParticleSystem;
    [SerializeField] public Transform teleportPoint;

    [Header("DROP")]
    [SerializeField] GameObject drop;

    [Header("STATS")]
    public float damage;

    [Header("FX")]
    [SerializeField] private TrailRenderer projectileTrail;
    #endregion

    public void ChangeMod(ProjectileMode _projectileMode)
    {
        projectileMode = _projectileMode;
        switch(projectileMode)
        {
            case ProjectileMode.Physic:
                rb.isKinematic = false;
                projectileTrail.gameObject.SetActive(true);
                break;
            case ProjectileMode.Drop:
                currentSpeed = 0;
                rb.isKinematic = true;
                currentPlayer = null;
                physic.SetActive(false);
                drop.SetActive(true);
                transform.DOScale(1, .3f);
                drop.transform.DORotate(new Vector3(0, 360, 0), 1).SetLoops(-1, LoopType.Restart);
                drop.transform.DOMoveY(drop.transform.position.y + .2f, 3).SetLoops(-1, LoopType.Yoyo);
                rb.velocity = Vector3.zero;
                break;
            case ProjectileMode.Inventory:
                projectileTrail.gameObject.SetActive(false);
                transform.DOScale(0, .3f).OnComplete(() =>
                {
                    drop.SetActive(false);
                    physic.SetActive(false);
                    DOTween.Kill(drop);
                    currentPlayer.GetProjectile(this);
                    if (currentPlayer.inputManager.Player.Shoot.IsPressed())
                    {
                        currentPlayer.Aim();
                    }
                });
                rb.velocity = Vector3.zero;
                break;
            case ProjectileMode.Recall:
                transform.SetParent(null);
                currentSpeed = 0;
                rb.isKinematic = true;
                break;
            case ProjectileMode.Aiming:
                drop.SetActive(false);
                physic.SetActive(true);
                transform.localScale = Vector3.zero;
                transform.DOScale(1, .3f);
                DOTween.Kill(drop);
                break;
            case ProjectileMode.StartInventory:
                rb.isKinematic = true;
                currentSpeed = 0;
                projectileTrail.gameObject.SetActive(false);
                transform.localScale = Vector3.zero;
                transform.position = currentPlayer.transform.position;
                drop.SetActive(false);
                physic.SetActive(false);
                DOTween.Kill(drop);
                currentPlayer.GetProjectile(this);
                //if (currentPlayer.inputManager.Player.Shoot.IsPressed())
                //{
                //    currentPlayer.Aim();
                //}
                break;
        }
    }

    public void OnCollision()
    {
        if (projectileMode == ProjectileMode.Physic)
        {
            currentSpeed = 0;
            hitParticleSystem.Play();
        }
    }

    public void Recall()
    {
        ChangeMod(ProjectileMode.Recall);
    }

    #region Unity API
    private void Start()
    {
        ChangeMod(projectileMode);
    }

    private void Update()
    {
        if(projectileMode == ProjectileMode.Recall)
        {
            transform.position = Vector3.Lerp(transform.position, currentPlayer.transform.position, Time.deltaTime * 10);
            transform.forward = currentPlayer.transform.position;
            if (Vector3.Distance(transform.position, currentPlayer.transform.position) < 2)
            {
                ChangeMod(ProjectileMode.Inventory);
            }
        }

        if(projectileMode == ProjectileMode.Drop && currentPlayer != null)
        {
            transform.position = Vector3.Lerp(transform.position, currentPlayer.transform.position, Time.deltaTime * 3);
            if (Vector3.Distance(transform.position, currentPlayer.transform.position) < 2)
            {
                ChangeMod(ProjectileMode.Inventory);
            }
        }
    }

    private void FixedUpdate()
    {
        if (projectileMode == ProjectileMode.Physic)
        {
            rb.velocity = transform.forward * currentSpeed * Time.deltaTime;
        }
    }
    #endregion
}
