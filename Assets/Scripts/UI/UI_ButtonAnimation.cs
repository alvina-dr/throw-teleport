using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Coffee.UIExtensions.Demo
{
    public class UI_ButtonAnimation : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Transform scale;
        [SerializeField] private UIParticle uiParticle;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            if(uiParticle != null) uiParticle.Play();
        }
    }
}


