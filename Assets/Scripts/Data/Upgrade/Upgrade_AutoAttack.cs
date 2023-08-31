using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/AutoAttack")]
public class Upgrade_AutoAttack : UpgradeData
{
    public override void Upgrade(PermanentDataHolder _PDH)
    {
        base.Upgrade(_PDH);
        _PDH.currentAbilities.abilityAutomaticAttack = true;
    }

    public override bool CheckUpgrade(PermanentDataHolder _PDH)
    {
        return _PDH.currentAbilities.abilityAutomaticAttack;
    }
}
