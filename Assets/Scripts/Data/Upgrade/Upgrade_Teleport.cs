using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Teleport")]
public class Upgrade_Teleport : UpgradeData
{
    public override void Upgrade(PermanentDataHolder _PDH)
    {
        base.Upgrade(_PDH);
        _PDH.currentAbilities.abilityTeleport = true;
    }

    public override bool CheckUpgrade(PermanentDataHolder _PDH)
    {
        return _PDH.currentAbilities.abilityTeleport;
    }
}
