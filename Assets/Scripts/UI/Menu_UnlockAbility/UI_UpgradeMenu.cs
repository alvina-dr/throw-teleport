using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class UI_UpgradeMenu : MonoBehaviour
{
    #region Properties
    public CanvasGroup canvasGroup;
    private List<UI_Button_Upgrade> upgradeButtonList = new List<UI_Button_Upgrade>();
    private List<UpgradeData> upgradeDataList = new List<UpgradeData>();
    public UI_AbilityChoice abilityChoice;
    [SerializeField] private Transform upgradeGridLayout;
    [SerializeField] private UI_Button_Upgrade buttonUpgradePrefab;
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI abilityDescription;
    [SerializeField] private TextMeshProUGUI unlockCost;
    [SerializeField] private Button unlockButton;
    #endregion

    #region Methods
    public void UpdateMenu()
    {
        for (int i = 0; i < upgradeButtonList.Count; i++)
        {
            upgradeButtonList[i].UpdateButton();
        }
        unlockCost.text = UnlockCost().ToString();
        if (UnlockCost() > PermanentDataHolder.Instance.currentMaterial)
        {
            unlockButton.interactable = false;
        } else
        {
            unlockButton.interactable = true;
        }
    }

    public void UpdateAbilityInfo(UpgradeData _data)
    {
        abilityName.text = _data.upgradeName;
        abilityDescription.text = _data.upgradeDescription;
        Debug.Log("abilityName" + _data.name);
    }

    public void OpenMenu()
    {
        canvasGroup.DOFade(1, .3f).OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            GPCtrl.Instance.Player.blockPlayerMovement = true;
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
            GPCtrl.Instance.Player.blockPlayerMovement = false;
        });
    }

    public void UnlockChoiceUpgrade()
    {
        if (UnlockCost() > PermanentDataHolder.Instance.currentMaterial)
        {
            unlockButton.transform.DOShakePosition(.2f, .5f);
        } else
        {
            List<UpgradeData> _tempDataList = upgradeDataList.FindAll(x => x.CheckUpgrade(PermanentDataHolder.Instance) == false);
            List<UpgradeData> _chosenAbilities = new List<UpgradeData>();

            for (int i = 0; i < 3; i++)
            {
                UpgradeData _data = _tempDataList[Random.Range(0, _tempDataList.Count)];
                _chosenAbilities.Add(_data);
                _tempDataList.Remove(_data);
                if (_tempDataList.Count == 0) break;
            }

            abilityChoice.Open(_chosenAbilities);
            PermanentDataHolder.Instance.currentMaterial -= UnlockCost();
            GPCtrl.Instance.UICtrl.materialCount.SetText(PermanentDataHolder.Instance.currentMaterial.ToString());
        }
    }

    public int UnlockCost()
    {
        return upgradeDataList.FindAll(x => x.CheckUpgrade(PermanentDataHolder.Instance)).Count * 100;
    }
    #endregion

    #region Unity API
    private void Awake()
    {
        UpgradeData[] upgradeArray = Resources.LoadAll<UpgradeData>("Upgrade");
        for (int i = 0; i < upgradeArray.Length; i++)
        {
            UI_Button_Upgrade _button = Instantiate(buttonUpgradePrefab, upgradeGridLayout);
            _button.SetupButton(upgradeArray[i], this);
            upgradeButtonList.Add(_button);
            upgradeDataList.Add(upgradeArray[i]);
        }
        UpdateMenu();
    }
    #endregion
}
