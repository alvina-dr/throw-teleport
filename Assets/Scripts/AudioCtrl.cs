using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    #region Properties
    [SerializeField] private AudioSource audioSource;
    #endregion

    #region Methods
    public void PlaySound(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
    #endregion
}
