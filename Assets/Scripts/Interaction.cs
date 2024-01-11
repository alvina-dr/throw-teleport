using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] List<UI_DialogBox.DialogEntry> _dialogList;

    public void Interact()
    {
        onInteract?.Invoke();
    }

    public void Dialog()
    {
        GPCtrl.Instance.UICtrl.dialogBox.ValidateDialog(_dialogList);
        Debug.Log("INTERACT");
    }
}
