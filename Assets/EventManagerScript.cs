using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    ///Summary
    ///Define delegates, events, and invocations
    ///

    #region delegates
    public delegate  void ToyReachedBedEventDelegate();
    #endregion

    #region events
    public static event ToyReachedBedEventDelegate ToyReachedBedEvent;
    #endregion

    #region invocations
    public static void InvokeToyReachedBedEvent()
    {
        ToyReachedBedEvent.Invoke();
    }
    #endregion
}
