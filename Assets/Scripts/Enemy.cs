using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour
{
    #region Properties
    [Header("STATS")]
    EnemyData data;
    float currentHealth;
    [SerializeField] UI_HealthBar healthBar;

    [Header("NAVIGATION")]
    [SerializeField] AIPath aiPath;
    [SerializeField] AIDestinationSetter aiDestinationSetter;
    Player target;

    [Header("ATTACK")]
    public float attackTimer;
    #endregion

    #region Methods
    public void SetEnemyStats(EnemyData _data)
    {
        data = _data;
        currentHealth = data.maxHealth;
        healthBar.SetHealthValue(currentHealth, data.maxHealth);
        aiPath.endReachedDistance = data.attackRange;
        aiPath.maxSpeed = data.maxSpeed;
        target = GPCtrl.Instance.Player;
        aiDestinationSetter.target = target.transform;
    }

    private void Attack()
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
        currentHealth -= _damage;
        healthBar.SetHealthValue(currentHealth, data.maxHealth);
        if (currentHealth <= 0)
            Death();
    }

    public void Death()
    {
        Debug.Log("DEATH ENEMY");
        Destroy(gameObject);
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
    #endregion
}
