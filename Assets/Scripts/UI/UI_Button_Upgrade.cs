using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_Button_Upgrade : MonoBehaviour, ISelectHandler, IDeselectHandler
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
        upgradeNameText.text = upgradeData.upgradeName;
        upgradeCostText.text = upgradeData.upgradeCost.ToString();
        UpdateButton();
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

    public void Upgrade()
    {
        if (upgradeData.CheckUpgrade(PermanentDataHolder.Instance)) return;
        if(upgradeData.upgradeCost > PermanentDataHolder.Instance.currentMaterial)
        {
            //not enough material feedback
            transform.DOShakePosition(.2f, .5f);
        } else
        {
            upgradeData.Upgrade(PermanentDataHolder.Instance);
            upgradeCostText.gameObject.SetActive(false);
            PermanentDataHolder.Instance.currentMaterial -= upgradeData.upgradeCost;
            upgradeMenu.UpdateMenu();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.DOScale(transform.localScale + Vector3.one * .1f, .2f);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(transform.localScale - Vector3.one * .1f, .2f);
    }

}
