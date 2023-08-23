using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_ValueBar : MonoBehaviour
{
    #region Properties
    [SerializeField] private Slider bar;
    #endregion

    #region Methods
    public void SetBarValue(float _currentValue, float _maxValue)
    {
        bar.DOValue(_currentValue / _maxValue, .2f);
    }
    #endregion
}
