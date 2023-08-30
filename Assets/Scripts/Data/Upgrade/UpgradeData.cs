using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/Upgrade/", order = 0)]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;
    public int upgradeCost;
    public virtual void Upgrade(PermanentDataHolder _PDH)
    {

    }
}
