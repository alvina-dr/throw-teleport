using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FX_Enemy", menuName = "ScriptableObjects/FXList/FX_Enemy", order = 1)]
public class FXList : ScriptableObject
{
    public GameObject damageParticle;
    public GameObject deathParticle;
}
