using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FaceCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + new Vector3(0, 0, 1));
    }
}
