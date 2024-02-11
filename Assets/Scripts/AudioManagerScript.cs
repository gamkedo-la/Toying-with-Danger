using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    ///Summary
    ///Define audio manager behavior
    ///

    #region cached references
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public static AudioManagerScript Instance { get; private set; }
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic(string name)
    {
        Sound audioClip = Array.Find(musicSounds, c => c.name == name);

        if (audioClip == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = audioClip.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound audioClip = Array.Find(sfxSounds, c => c.name == name);

        if (audioClip == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(audioClip.clip);
        }
    }

    #region event subscriptions
    private void OnEnable()
    {
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
    }
    #endregion

    private void HandleStartRealTimeStageEvent()
    {
        Instance.PlayMusic("StartGameMusic");
    }
}
