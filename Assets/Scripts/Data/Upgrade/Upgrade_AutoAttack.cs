using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/AutoAttack")]
public class Upgrade_AutoAttack : UpgradeData
{
    public override void Upgrade(Player _player)
    {
        base.Upgrade(_player);
        _player.currentAbilities.abilityAutomaticAttack = true;
    }
}
