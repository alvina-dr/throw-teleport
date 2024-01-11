using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_DialogBox : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI nameTextUnderline;
    public TextMeshProUGUI dialogText;
    private bool waitValidation = true;
    [HideInInspector]
    public List<DialogEntry> dialogList = new List<DialogEntry>();
    private int index = -1;

    public void ShowDialogList(List<DialogEntry> _dialogList)
    {
        canvasGroup.alpha = 1;
        dialogList = _dialogList;
        ShowDialog(dialogList[index]);
    } 

    public void ShowDialog(DialogEntry _entry)
    {
        transform.localScale = Vector3.zero;
        nameText.text = _entry.name;
        nameTextUnderline.text = _entry.name;
        dialogText.text = _entry.dialog;
        transform.DOScale(1.1f, .2f).OnComplete(() =>
        {
            transform.DOScale(1f, .2f).OnComplete(() =>
            {
                waitValidation = true;
            });
        });
    }

    public void ValidateDialog(List<DialogEntry> _dialogList)
    {
        if (!waitValidation) return;
        waitValidation = false;
        index++;
        if (index == 0) ShowDialogList(_dialogList); 
        else
        {
            if (index >= dialogList.Count) CloseDialogBox();
            else
            {
                ShowDialog(dialogList[index]);
            }
        }
    }

    public void CloseDialogBox()
    {
        canvasGroup.alpha = 0;
        index = -1;
        waitValidation = true;
    }

    [System.Serializable]
    public class DialogEntry
    {
        public string name;
        public string dialog;
    }
}
