using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject firstSelectedObject;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
