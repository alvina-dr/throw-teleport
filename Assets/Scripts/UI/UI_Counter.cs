using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        SetText(PermanentDataHolder.Instance.currentMaterial);
    }

    public void SetText(int value)
    {
        text.text = value.ToString();
    }
}
