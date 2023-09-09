using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float damage;
    private float speed = 0;
    private float chrono = 0;

    public void SetupProjectile(Vector3 _orientation, float _speed, float _damage)
    {
        transform.forward = _orientation;
        speed = _speed;
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null) {
            Debug.Log("damage player with projectile");
            _player.Damage(damage);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
        chrono += Time.deltaTime;
        if (chrono >= 3f) Destroy(gameObject);
    }
}
