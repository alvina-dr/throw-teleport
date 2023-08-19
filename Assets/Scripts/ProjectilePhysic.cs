using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectilePhysic : MonoBehaviour
{
    [SerializeField] Projectile projectile;

    private void OnTriggerEnter(Collider other)
    {
        projectile.OnCollision();
        if(other.GetComponent<Enemy>() != null)
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            _enemy.Damage(projectile.damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
