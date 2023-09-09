using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UI_AbilityChoice : MonoBehaviour
{
    [SerializeField] private List<UI_Button_Upgrade> upgradeButtonList = new List<UI_Button_Upgrade>();
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI abilityDescription;
    private UpgradeData currentAbility;

    public void Open(List<UpgradeData> _upgradeDataList)
    {
        canvasGroup.DOFade(1, .3f).OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
        for (int i = 0; i < upgradeButtonList.Count; i++)
        {
            if(i > _upgradeDataList.Count - 1)
            {
                upgradeButtonList[i].gameObject.SetActive(false);
            }
            else
            {
                upgradeButtonList[i].SetupButton(_upgradeDataList[i], GPCtrl.Instance.UICtrl.upgradeMenu);
            }
        }
    }

    public void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, .3f);
    }

    public void DisplayAbility(UpgradeData _data)
    {
        currentAbility = _data;
        abilityName.text = _data.upgradeName;
        abilityDescription.text = _data.upgradeDescription;
    }

    public void ConfirmAbility()
    {
        if (currentAbility == null) return;
        currentAbility.Upgrade(PermanentDataHolder.Instance);
        GPCtrl.Instance.UICtrl.upgradeMenu.UpdateMenu();
        Close();
    }
}
