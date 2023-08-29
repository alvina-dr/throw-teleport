using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Button_Upgrade : MonoBehaviour
{
    private UpgradeData upgradeData;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void SetupButton(UpgradeData _data)
    {
        upgradeData = _data;
        buttonText.text = upgradeData.upgradeName;
    }

    public void Upgrade()
    {
        upgradeData.Upgrade(GPCtrl.Instance.Player);
        button.interactable = false;
        //GPCtrl.Instance.UICtrl.upgradeMenu
    }
}
