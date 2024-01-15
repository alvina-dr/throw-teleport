using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class PermanentDataHolder : MonoBehaviour
{
    #region Properties
    [Header("UPGRADE")]
    public Abilities currentAbilities;

    [Header("RESSOURCES")]
    public int currentMaterial;
    public bool hasProjectile = false;

    [Header("FADE_DARK")]
    [SerializeField] private CanvasGroup darkBackground;

    [Header("MUSIC")]
    [SerializeField] private AudioSource musicSource;

    [Header("STATS")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;

    [Header("REMEMBER")]
    public List<string> enemyKilledID = new List<string>();
    public List<string> dropPickUpID = new List<string>();
    public string formerRoom;
    public bool Room2Locked = true;

    [Header("DIALOGS")]
    public List<UI_DialogBox.DialogEntry> startDialog;
    public List<UI_DialogBox.DialogEntry> unlockTeleportDialog;
    #endregion

    #region Classes
    public class Abilities
    {
        public bool abilityTeleport = false;
        public bool abilityDash = true;
        public bool abilityDrag = false;
        public bool abilityAutomaticAttack = false;
    }
    #endregion

    #region Methods
    public void AddMaterial(int _num)
    {
        currentMaterial += _num;
    }

    public void FadeIn(Action onComplete)
    {
        darkBackground.DOFade(1, .3f).OnComplete(() =>
        {
            onComplete.Invoke();
        });
    }

    public void FadeOut()
    {
        darkBackground.DOFade(0, .3f);
    }

    public void OpenDoor()
    {
        Room2Locked = false;
    }
    #endregion

    #region Singleton
    public static PermanentDataHolder Instance { get; private set; }
    #endregion

    #region Unity API
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //check if in save
        if (currentAbilities == null) currentAbilities = new Abilities();
        formerRoom = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        DOVirtual.DelayedCall(.5f, () =>
        {
            if (formerRoom == "Base")
            {
                GPCtrl.Instance.UICtrl.dialogBox.ValidateDialog(startDialog);
                musicSource.Play();
            }
        });
    }

    private void Update()
    {
        if (formerRoom == "Room_2" && currentAbilities.abilityTeleport == false && FindObjectsOfType<Enemy>().Length == 0)
        {
            currentAbilities.abilityTeleport = true;
            GPCtrl.Instance.UICtrl.dialogBox.ValidateDialog(unlockTeleportDialog);
        }
    }
    #endregion
}
