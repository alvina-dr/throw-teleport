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
        Recall = 3
    }
    [SerializeField] ProjectileMode projectileMode;
    public Player currentPlayer;

    [Header("Physic")]
    [SerializeField] GameObject physic;
    [SerializeField] Rigidbody rb;
    public float currentSpeed;
    public Vector3 direction;

    [Header("Drop")]
    [SerializeField] GameObject drop;
    #endregion

    public void ChangeMod(ProjectileMode _projectileMode)
    {
        Debug.Log("CHANGE MOD : " + _projectileMode);
        projectileMode = _projectileMode;
        switch(projectileMode)
        {
            case ProjectileMode.Physic:
                drop.SetActive(false);
                physic.SetActive(true);
                transform.localScale = Vector3.zero;
                transform.DOScale(1, .3f);
                DOTween.Kill(drop);
                break;
            case ProjectileMode.Drop:
                currentSpeed = 0;
                currentPlayer = null;
                physic.SetActive(false);
                drop.SetActive(true);
                transform.DOScale(1, .3f);
                drop.transform.DORotate(new Vector3(0, 360, 0), 1).SetLoops(-1, LoopType.Restart);
                drop.transform.DOMoveY(drop.transform.position.y + .5f, 1).SetLoops(-1, LoopType.Yoyo);
                rb.velocity = Vector3.zero;
                break;
            case ProjectileMode.Inventory:
                transform.DOScale(0, .3f).OnComplete(() =>
                {
                    drop.SetActive(false);
                    physic.SetActive(false);
                    DOTween.Kill(drop);
                    Debug.Log("to inventory");
                    currentPlayer.AddProjectile(this);
                    if (currentPlayer.inputManager.Player.Shoot.IsPressed())
                    {
                        Debug.Log("was aiming already");
                        currentPlayer.Aim();
                    }
                });
                rb.velocity = Vector3.zero;
                break;
            case ProjectileMode.Recall:
                currentSpeed = 0;
                break;
        }
    }

    public void OnCollision()
    {
        currentSpeed = 0;
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

    private void FixedUpdate()
    {
        if(projectileMode == ProjectileMode.Physic)
        {
            rb.velocity = transform.forward * currentSpeed * Time.deltaTime;
        }

        if(projectileMode == ProjectileMode.Recall)
        {
            transform.position = Vector3.Lerp(transform.position, currentPlayer.transform.position, Time.deltaTime * 10);
            Debug.Log("projectile position : " + transform.position + " | player position" + currentPlayer.transform.position);
            //transform.forward = currentPlayer.transform.position;
            //Debug.Log("recalling");
            if (Vector3.Distance(transform.position, currentPlayer.transform.position) < 2)
            {
                Debug.Log("back in inventory");
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
    #endregion
}
