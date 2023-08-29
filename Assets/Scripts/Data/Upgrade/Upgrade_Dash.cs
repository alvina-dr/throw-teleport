using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Dash")]
public class Upgrade_Dash : UpgradeData
{
    public override void Upgrade(Player _player)
    {
        base.Upgrade(_player);
        _player.currentAbilities.abilityDash = true;
    }
}
