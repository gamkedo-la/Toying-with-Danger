using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This timer script is to be reused
/// </summary>
public class Timer : MonoBehaviour
{
    #region Events
    public delegate void MaxTimeReachedEvent();
    public event MaxTimeReachedEvent maxTimeReached;
    #endregion

    public string timerName;
    float currentTime;
    public float MaxTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < MaxTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            MaxTimeReached();
            currentTime = 0;
        }
    }

    void MaxTimeReached()
    {
        maxTimeReached.Invoke();
    }
}
