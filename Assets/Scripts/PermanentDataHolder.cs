using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentDataHolder : MonoBehaviour
{
    #region Properties
    [Header("UPGRADE")]
    public Abilities currentAbilities;

    [Header("RESSOURCES")]
    public int currentMaterial;
    #endregion

    #region Classes
    public class Abilities
    {
        public bool abilityTeleport = true;
        public bool abilityDash = false;
        public bool abilityDrag = false;
        public bool abilityAutomaticAttack = false;
    }
    #endregion

    #region Singleton
    public static PermanentDataHolder Instance { get; private set; }
    #endregion

    #region Unity API
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //check if in save
        if (currentAbilities == null) currentAbilities = new Abilities();
    }
    #endregion
}
