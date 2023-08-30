using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;

    public void Interact()
    {
        onInteract?.Invoke();
    }
}
