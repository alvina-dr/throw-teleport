using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Button_Upgrade : MonoBehaviour
{
    private UpgradeData upgradeData;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;

    public void SetupButton(UpgradeData _data)
    {
        upgradeData = _data;
        upgradeNameText.text = upgradeData.upgradeName;
        upgradeCostText.text = upgradeData.upgradeCost.ToString();
        if (_data.upgradeCost > PermanentDataHolder.Instance.currentMaterial)
        {
            button.interactable = false;
            upgradeCostText.color = Color.red;
        }
    }

    public void Upgrade()
    {
        upgradeData.Upgrade(PermanentDataHolder.Instance);
        button.interactable = false;
        upgradeCostText.gameObject.SetActive(false);
        PermanentDataHolder.Instance.currentMaterial -= upgradeData.upgradeCost;
        //GPCtrl.Instance.UICtrl.upgradeMenu
    }
}
