using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    ///Summary
    ///Define audio manager behavior
    ///

    #region cached references
    private AudioSource myAudioSource;
    #endregion

    private void Awake()
    {
        myAudioSource = gameObject.transform.GetComponent<AudioSource>();
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
        myAudioSource.Play();
    }
}
