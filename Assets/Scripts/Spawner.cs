using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    public float frequency;
    public List<EnemyData> enemyList = new List<EnemyData>();
    public float enemyNumber;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > frequency)
        {
            timer = 0;
            for (int i = 0; i < enemyNumber; i++)
            {
                EnemyData _data = enemyList[Random.Range(0, enemyList.Count - 1)];
                Enemy _enemy = Instantiate(_data.enemyObject);
                _enemy.SetEnemyStats(_data);
            }

        }
    }
}
