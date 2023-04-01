using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    #region SoundEffect instance
    public static SoundEffectManager _instance;

    public static SoundEffectManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("SoundEffectManager is Null");

            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    public AudioSource weaponSFX;
    public AudioSource stanceChangeMeleeSFX;
    public AudioSource stanceChangeRangedSFX;
    public AudioSource rewardSFX;
    public AudioSource combatSFX;
    public AudioSource mapSFX;
    public AudioSource shopSFX;
    

}
