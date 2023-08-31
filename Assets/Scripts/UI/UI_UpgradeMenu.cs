using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_UpgradeMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private List<UI_Button_Upgrade> upgradeButtonList = new List<UI_Button_Upgrade>();
    [SerializeField] private Transform upgradeGridLayout;
    [SerializeField] private UI_Button_Upgrade buttonUpgradePrefab;

    private void Awake()
    {
        UpgradeData[] upgradeArray = Resources.LoadAll<UpgradeData>("Upgrade");
        for (int i = 0; i < upgradeArray.Length; i++)
        {
            UI_Button_Upgrade _button = Instantiate(buttonUpgradePrefab, upgradeGridLayout);
            _button.SetupButton(upgradeArray[i], this);
            upgradeButtonList.Add(_button);
        }
    }

    public void UpdateMenu()
    {
        for (int i = 0; i < upgradeButtonList.Count; i++)
        {
            upgradeButtonList[i].UpdateButton();
        }
    }

    public void OpenMenu()
    {
        canvasGroup.DOFade(1, .3f).OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            EventSystem.current.SetSelectedGameObject(upgradeButtonList[0].gameObject);
            //Time.timeScale = 0;
        });
    }

    public void CloseMenu()
    {
        //Time.timeScale = 1;
        canvasGroup.DOFade(0, .3f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
}
