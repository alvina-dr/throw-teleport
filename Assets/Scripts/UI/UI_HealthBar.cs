using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_HealthBar : MonoBehaviour
{
    #region Properties
    [SerializeField] private Slider healthBar;
    #endregion

    #region Methods
    public void SetHealthValue(float _currentHealth, float _maxHealth)
    {
        healthBar.value = _currentHealth / _maxHealth;
    }
    #endregion
}
