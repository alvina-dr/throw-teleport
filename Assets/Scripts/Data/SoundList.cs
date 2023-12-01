using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "ScriptableObjects/SoundList", order = 1)]
public class SoundList : ScriptableObject
{
    [Header("SOUND")]
    public List<AudioClip> shootSoundList = new List<AudioClip>();
    public AudioClip footstepNormal;
    public AudioClip lootExperience;
    public AudioClip playerDamage;
    public AudioClip playerDash;
    public AudioClip genericDeath;

    [Header("MUSIC")]
    public AudioClip genericMusic;
}
