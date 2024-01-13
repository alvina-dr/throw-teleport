using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LaunchDungeon : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private string specialEntry;
    [SerializeField] private bool askConfirmation = false;
    [SerializeField] private UnityEvent askConfirmationEvent;
    [SerializeField] private UnityEvent hideAskConfirmationEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            if (askConfirmation)
            {
                askConfirmationEvent?.Invoke();
            } else
            {
                ChangeScene();
            }
        }
    }

    public void ChangeScene()
    {
        if (specialEntry != "") PermanentDataHolder.Instance.formerRoom = specialEntry;
        PermanentDataHolder.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene(sceneName);
            //PermanentDataHolder.Instance.formerRoom = sceneName;
            PermanentDataHolder.Instance.FadeOut();
        });
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            if (askConfirmation)
            {
                hideAskConfirmationEvent?.Invoke();
            }
        }
    }

}
