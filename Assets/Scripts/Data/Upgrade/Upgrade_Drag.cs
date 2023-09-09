using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Drag")]
public class Upgrade_Drag : UpgradeData
{
    public override void Upgrade(PermanentDataHolder _PDH)
    {
        base.Upgrade(_PDH);
        _PDH.currentAbilities.abilityDrag = true;
    }

    public override bool CheckUpgrade(PermanentDataHolder _PDH)
    {
        return _PDH.currentAbilities.abilityDrag;
    }
}
