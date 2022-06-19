using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    #region Public
    [Header("AudioSorce")]
    public AudioSource BGMSorce;
    public AudioSource SESorce;

    [Header("SoundResource")]
    public AudioClip BGM;
    public List<AudioClip> SE = new List<AudioClip>();
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBGM(ulong delay = 0)
    {
        BGMSorce.clip = BGM;
        BGMSorce.Play(delay);
    }


}
