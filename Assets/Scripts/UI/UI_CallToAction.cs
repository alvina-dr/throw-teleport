using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_CallToAction : MonoBehaviour
{
    Vector3 worldPosition;
    [SerializeField] private Image icon;
    public void ShowCallToAction(Vector3 _worldPosition)
    {
        transform.GetChild(0).localPosition = new Vector3(0, 130, 0);
        icon.transform.localScale = Vector3.one * .4f;
        gameObject.SetActive(true);
        worldPosition = _worldPosition;
        transform.GetChild(0).DOKill();
        transform.GetChild(0).DOScale(1.1f, .2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.GetChild(0).DOScale(1, .1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                transform.GetChild(0).DOLocalMoveY(140, .3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                icon.transform.DOScale(.48f, .2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                //transform.DORotate(new Vector3(0, 0, 10f), .3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            });
        });
    }

    public void HideCallToAction()
    {
        transform.GetChild(0).DOKill();
        icon.transform.DOKill();
        transform.GetChild(0).DOScale(1.1f, .2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.GetChild(0).DOScale(0, .1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                gameObject.SetActive(false);
                transform.GetChild(0).localPosition = new Vector3(0, 130, 0);
                icon.transform.localScale = Vector3.one * .4f;
            });
        });
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(worldPosition);
    }
}
