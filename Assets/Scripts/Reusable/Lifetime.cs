using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    #region Events

    public delegate void MaxLifeTimeReached();
    public event MaxLifeTimeReached MaxLifeTimeReachedEvent;
    #endregion
    [SerializeField]
    float lifeTime;

    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < lifeTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            LifeTimeReached();
        }
    }
    void LifeTimeReached()
    {
        MaxLifeTimeReachedEvent();
    }
}
