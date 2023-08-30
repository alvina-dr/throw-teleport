using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralData", menuName = "ScriptableObjects/GeneralData", order = 1)]
public class GeneralData : ScriptableObject
{
    [Header("PREFABS")]
    public GameObject lootObject;
}
