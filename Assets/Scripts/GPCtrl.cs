using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    #region Methods

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
                Destroy(enemyArray[i].gameObject);
            } 
        }
    }
    #endregion
}
