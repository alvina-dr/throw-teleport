using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCtrl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Enemy[] enemyArray = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemyArray.Length; i++)
            {
                Destroy(enemyArray[i]);
            }
        }
    }
}
