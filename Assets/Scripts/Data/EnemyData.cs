using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public Enemy enemyObject;
    public float maxHealth;
    public float maxSpeed;
    
    [Header("ATTACK")]
    public float attackSpeed;
    public float attackRange;
    public float damage;

    [Header("LOOT")]
    public int material;
}
