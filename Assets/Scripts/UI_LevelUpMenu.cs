using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_LevelUpMenu : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void OpenMenu()
    {
        canvasGroup.DOFade(1, .3f).OnComplete(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            Time.timeScale = 0;
        });
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        canvasGroup.DOFade(0, .3f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
}
