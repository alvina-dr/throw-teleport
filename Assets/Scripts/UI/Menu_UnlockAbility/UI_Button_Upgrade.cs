using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Button_Upgrade : MonoBehaviour
{
    private UpgradeData upgradeData;
    private UI_UpgradeMenu upgradeMenu;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private Image background;
    [SerializeField] private Color buyColor;
    [SerializeField] private Color boughtColor;

    public void SetupButton(UpgradeData _data, UI_UpgradeMenu _menu)
    {
        upgradeData = _data;
        upgradeMenu = _menu;
        upgradeNameText.text = upgradeData.upgradeLetter;
        upgradeCostText.text = upgradeData.upgradeCost.ToString();
    }

    public void UpdateButton()
    {
        if (upgradeData.CheckUpgrade(PermanentDataHolder.Instance))
        {
            background.color = boughtColor;
        }
        else {
            background.color = buyColor;
            if (upgradeData.upgradeCost > PermanentDataHolder.Instance.currentMaterial)
            {
                upgradeCostText.color = Color.red;
            }
        }
    }

    public void SelectUpgrade()
    {
        upgradeMenu.UpdateAbilityInfo(upgradeData);
        if (upgradeData.CheckUpgrade(PermanentDataHolder.Instance)) return;
    }

    public void SelectAbility()
    {
        GPCtrl.Instance.UICtrl.upgradeMenu.abilityChoice.DisplayAbility(upgradeData);
    }
}
