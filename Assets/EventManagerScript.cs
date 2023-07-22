using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    ///Summary
    ///Define delegates, events, and invocations
    ///

    //delegates are the type of methods which an event can accept. When the event is invoked, all delegates which have subscribed to the event will be called. An example of different types of events could be
    //ones which take no parameters and ones which take one parameter, such as GameOverEventEmptyDelegate() and GameOverEventStringDelegate(string GameOverNotificationString)
    //Take note that the events defined below the delegates take similarly named delegates so they are linked. For example GameOverEventDelegate() goes with GameOverEvent by declaring it as a
    //GameOverEventDelegate type.
    #region delegates
    public delegate void ToyReachedBedEventDelegate();
    public delegate void GameOverEventDelegate();
    public delegate void StartRealTimeStageEventDelegate();
    #endregion

    //events are specific occurrences that happen in the game. For example, GameOverEvent. When the game over event is called, perhaps a notification manager will display "game over", the audio manager will
    //play game over sfx, and a menu manager will display options for trying the level again or going back to the main menu
    #region events
    public static event ToyReachedBedEventDelegate ToyReachedBedEvent;
    public static event GameOverEventDelegate GameOverEvent;
    public static event StartRealTimeStageEventDelegate StartRealTimeStageEvent;
    #endregion

    //Event invocations are the one location where an event is triggered, and then the event will call all event methods stored in it.
    #region invocations
    public static void InvokeToyReachedBedEvent()
    {
        ToyReachedBedEvent.Invoke();
    }

    public static void InvokeGameOverEvent()
    {
        GameOverEvent.Invoke();
    }

    public static void InvokeStartRealTimeStageEvent()
    {
        StartRealTimeStageEvent.Invoke();
    }
    #endregion
}
