using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Enemy : MonoBehaviour
{
    #region Properties
    [BoxGroup("ID")]
    [ReadOnly]
    public string id;

    #region IDSetup
    [BoxGroup("ID")]
    [Button]
    private void SetId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public static bool IsUniqueMonster(string ID)
    {
        Enemy[] enemyArray = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (ID == enemyArray[i].id) return false;
        }
        return true;
    }
    #endregion IDSetup

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

    [Header("AUDIO")]
    [SerializeField] private AudioSource damageSource;
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
        damageSource.Play();
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
        GPCtrl.Instance.AudioCtrl.PlaySound(GPCtrl.Instance.GeneralData.soundList.genericDeath);
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
                PermanentDataHolder.Instance.enemyKilledID.Add(id);
                Destroy(gameObject);
            });
        });
    }
    #endregion

    #region Unity API
    private void Update()
    {
        if (GPCtrl.Instance.pause)
        {
            aiPath.maxSpeed = 0;
            return;
        }
        else aiPath.maxSpeed = data.maxSpeed;

        if (target != null && Vector3.Distance(transform.position, target.transform.position) > 12)
        {
            aiPath.maxSpeed = 0;
        }
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
