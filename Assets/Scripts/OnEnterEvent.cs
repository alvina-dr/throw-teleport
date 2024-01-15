using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterEvent : MonoBehaviour
{

    [SerializeField] private UnityEvent onInteract;
    [SerializeField] List<UI_DialogBox.DialogEntry> _dialogList;
    
    private void OnTriggerEnter(Collider other)
    {
        onInteract?.Invoke();
    }

    public void Dialog()
    {
        GPCtrl.Instance.UICtrl.dialogBox.ValidateDialog(_dialogList);
    }
}
