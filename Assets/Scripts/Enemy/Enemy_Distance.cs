using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Distance : Enemy
{
    [Header("DISTANCE")]
    [SerializeField] Generic_Projectile projectile;
    [SerializeField] Transform projectileOrigin;

    public override void Attack()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, 10 * Time.deltaTime, 0);
        attackTimer += Time.deltaTime;
        if (attackTimer >= data.attackSpeed)
        {
            attackTimer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Attack");
        Generic_Projectile _projectile = Instantiate(projectile);
        _projectile.transform.position = projectileOrigin.position;
        _projectile.SetupProjectile(transform.forward, 1000f, data.damage);
    }
}
