using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class UI_ButtonAnimation : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform scale;

    public void OnSelect(BaseEventData eventData)
    {
        if (scale != null) scale.DOScale(1.05f, .2f);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (scale != null) scale.DOScale(1, .2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDeselect(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnSelect(null);
    }
}
