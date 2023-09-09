using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InputIndication : MonoBehaviour
{
    public void ShowInputIndication()
    {
        gameObject.SetActive(true);
    }

    public void HideInputIndication()
    {
        gameObject.SetActive(false);
    }
}
