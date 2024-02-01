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
        [SerializeField] private float biggerScale;
        [SerializeField] private float normalScale;
        [SerializeField] private GameObject showSelect;

        public void OnSelect(BaseEventData eventData)
        {
            if (scale != null) scale.DOScale(biggerScale == 0 ? 1.05f : biggerScale, .2f);
            if (showSelect != null) showSelect.SetActive(true);
        }
        public void OnDeselect(BaseEventData eventData)
        {
            if (scale != null) scale.DOScale(normalScale == 0 ? 1 : normalScale, .2f);
            if (showSelect != null) showSelect.SetActive(false);
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


