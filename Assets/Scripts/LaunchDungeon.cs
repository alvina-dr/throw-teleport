using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchDungeon : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        PermanentDataHolder.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene(sceneName);
            PermanentDataHolder.Instance.FadeOut();
        });
    }
}
