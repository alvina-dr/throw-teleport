using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    #region Properties
    [Header("STATS")]
    public EnemyData data;
    float currentHealth;
    [SerializeField] UI_ValueBar healthBar;

    [Header("NAVIGATION")]
    [SerializeField] AIPath aiPath;
    [SerializeField] AIDestinationSetter aiDestinationSetter;
    public Player target;

    [Header("ATTACK")]
    public float attackTimer;

    [Header("FX")]
    public FXList enemyFX;
    public BlinkColor blinkColor;
    public Animator animator;
    #endregion

    #region Methods
    public void SetEnemyStats(EnemyData _data)
    {
        data = _data;
        currentHealth = data.maxHealth;
        healthBar.SetBarValue(currentHealth, data.maxHealth);
        aiPath.endReachedDistance = data.attackRange;
        aiPath.maxSpeed = data.maxSpeed;
        target = GPCtrl.Instance.Player;
        aiDestinationSetter.target = target.transform;
    }

    public virtual void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= data.attackSpeed)
        {
            attackTimer = 0;
            target.Damage(data.damage);
        }
    }

    public void Damage(float _damage)
    {
        if (currentHealth <= 0) return;
        Instantiate(enemyFX.damageParticle, transform);
        currentHealth -= _damage;
        healthBar.SetBarValue(currentHealth, data.maxHealth);
        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            blinkColor.Blink();
        }
    }

    public void Death()
    {
        Debug.Log("DEATH ENEMY");
        Instantiate(GPCtrl.Instance.GeneralData.lootObject).transform.position = transform.position;
        Projectile[] _projectileArr = transform.GetComponentsInChildren<Projectile>();
        if (_projectileArr.Length > 0)
        {
            for (int i = 0; i < _projectileArr.Length; i++)
            {
                _projectileArr[i].Recall();
            }
        }
        aiPath.maxSpeed = 0;
        Instantiate(enemyFX.deathParticle).transform.position = transform.position;
        transform.DOScale(1.3f, .2f).OnComplete(() =>
        {
            transform.DOScale(.8f, .1f).OnComplete(() => 
            {
                Projectile[] _projectileArr = transform.GetComponentsInChildren<Projectile>();
                if (_projectileArr.Length > 0)
                {
                    for (int i = 0; i < _projectileArr.Length; i++)
                    {
                        _projectileArr[i].Recall();
                    }
                }
                Destroy(gameObject);
            });
        });
    }
    #endregion

    #region Unity API
    private void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= data.attackRange)
        {
            Attack();
        } else
        {
            attackTimer = data.attackSpeed / 2;
        }
    }

    private void Start()
    {
        if (data != null) SetEnemyStats(data);
    }
    #endregion
}
