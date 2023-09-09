using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FX_Player", menuName = "ScriptableObjects/FXList/FX_Player", order = 1)]
public class FX_Player : FXList
{
    [Header("PLAYER SPECIFIC")]
    public GameObject teleportParticle;
    public GameObject dashParticle;
}
