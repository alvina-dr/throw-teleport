using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        SetText(PermanentDataHolder.Instance.currentMaterial.ToString());
    }

    public void SetText(string value)
    {
        text.transform.DOScale(1.2f, .1f).OnComplete(() =>
        {
            text.text = value;
            text.transform.DOScale(1f, .1f);
        });
    }
}
