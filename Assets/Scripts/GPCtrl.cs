using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GPCtrl : MonoBehaviour
{
    #region Singleton
    public static GPCtrl Instance { get; private set; }
    #endregion

    #region Properties
    public enum GPCtrlMode
    {
        Dungeon = 0,
        QG = 1
    }
    public GPCtrlMode Mode;
    public Player Player;
    public UI_Ctrl UICtrl;
    public GeneralData GeneralData;
    public AudioCtrl AudioCtrl;
    public List<Transform> roomStartPointList = new List<Transform>();
    public bool pause = false;

    [Header("LOCKED DOOR")]
    public GameObject teleporter;
    public GameObject doorLocked;

    [Header("SCIENTIST")]
    public GameObject scientist1;
    public GameObject scientist2;

    #endregion

    #region Methods
    public void Pause(bool _value)
    {
        pause = _value;
        if (pause)
        {
            Player.blockPlayerMovement = true;
        } else
        {
            Player.blockPlayerMovement = false;
        }
    }

    public void UnlockTeleport()
    {
        PermanentDataHolder.Instance.UnlockTeleport();
    }

    public void OpenDoor()
    {
        PermanentDataHolder.Instance.OpenDoor();
    }
    #endregion

    #region Unity API
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        Enemy[] enemyArray = FindObjectsOfType<Enemy>(); //DELETE ENEMIES ALREADY KILLED
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (PermanentDataHolder.Instance.enemyKilledID.FindAll(x => x == enemyArray[i].id).Count > 0) {
                enemyArray[i].DOKill();
                Destroy(enemyArray[i].gameObject);
            } 
        }

        Drop[] dropArray = FindObjectsOfType<Drop>(); //DELETE ENEMIES ALREADY KILLED
        for (int i = 0; i < dropArray.Length; i++)
        {
            if (PermanentDataHolder.Instance.dropPickUpID.FindAll(x => x == dropArray[i].id).Count > 0)
            {
                dropArray[i].DOKill();
                Destroy(dropArray[i].gameObject);
            }
        }
        if (SceneManager.GetActiveScene().name == "Room_Teleport_1" && PermanentDataHolder.Instance.Room2Locked == false)
        {
            teleporter.SetActive(true);
            doorLocked.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "2_Room_1" && PermanentDataHolder.Instance.Room2Locked == false)
        {
            teleporter.SetActive(true);
            doorLocked.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "Base" && PermanentDataHolder.Instance.willUnlockTeleport)
        {
            scientist2.SetActive(true);
            scientist1.SetActive(false);
        }
    }

    private void Start()
    {
        PermanentDataHolder.Instance.OnSceneLaunch();
    }
    #endregion
}
