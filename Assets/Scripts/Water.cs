using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null)
        {
            PermanentDataHolder.Instance.FadeIn(() =>
            {
                _player.transform.position = respawnPoint.position;
                _player.Damage(10.0f);
                PermanentDataHolder.Instance.FadeOut();
            });
        }
    }
}
