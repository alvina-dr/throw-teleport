using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Dash")]
public class Upgrade_Dash : UpgradeData
{
    public override void Upgrade(PermanentDataHolder _PDH)
    {
        base.Upgrade(_PDH);
        _PDH.currentAbilities.abilityDash = true;
    }

    public override bool CheckUpgrade(PermanentDataHolder _PDH)
    {
        return _PDH.currentAbilities.abilityDash;
    }
}
