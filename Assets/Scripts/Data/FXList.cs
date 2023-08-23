using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FXList", menuName = "ScriptableObjects/FXList", order = 1)]
public class FXList : ScriptableObject
{
    public GameObject bloodParticle;
    public GameObject deathParticle;

    [Header("PLAYER SPECIFIC")]
    public GameObject teleportParticle;
}
