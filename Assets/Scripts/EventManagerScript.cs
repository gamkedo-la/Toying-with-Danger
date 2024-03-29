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
    public delegate void PreparationPuzzlePiecePlacementEventDelegate();
    public delegate void OutOfPreparationPiecesEventDelegate();
    public delegate void StartRealTimeStageEventDelegate();
    public delegate void RealTimePuzzlePiecePlacementEventDelegate();
    public delegate void OutOfRealTimePiecesEventDelegate();
    public delegate void ToyBlowsUpWallEventDelegate(GameObject wallToBeDestroyed);
    public delegate void ToyReachedBedEventDelegate(GameObject enemy);
    public delegate void GameOverEventDelegate(GameOverType gameOverType);
    public delegate void PreparationRemainingWallNumberChangedEventDelegate();
    public delegate void RealTimeRemainingWallNumberChangedEventDelegate();
    public delegate void PreparationRemainingTowerNumberChangedEventDelegate();
    public delegate void RealTimeRemainingTowerNumberChangedEventDelegate();
    public delegate void EnemyGotDestroyedEventDelegate(GameObject enemy);
    public delegate void ConfirmLevelStartWithRemainingPreparationPieces();
    #endregion

    //events are specific occurrences that happen in the game. For example, GameOverEvent. When the game over event is called, perhaps a notification manager will display "game over", the audio manager will
    //play game over sfx, and a menu manager will display options for trying the level again or going back to the main menu
    #region events
    public static event PreparationPuzzlePiecePlacementEventDelegate PreparationPuzzlePiecePlacementEvent;
    public static event OutOfPreparationPiecesEventDelegate OutOfPreparationPiecesEvent;
    public static event StartRealTimeStageEventDelegate StartRealTimeStageEvent;
    public static event RealTimePuzzlePiecePlacementEventDelegate RealTimePuzzlePiecePlacementEvent;
    public static event OutOfRealTimePiecesEventDelegate OutOfRealTimePiecesEvent;
    public static event ToyBlowsUpWallEventDelegate ToyBlowsUpWallEvent;
    public static event ToyReachedBedEventDelegate ToyReachedBedEvent;
    public static event GameOverEventDelegate GameOverEvent;
    public static event PreparationRemainingWallNumberChangedEventDelegate PreparationRemainingWallNumberChangedEvent;
    public static event RealTimeRemainingWallNumberChangedEventDelegate RealTimeRemainingWallNumberChangedEvent;
    public static event PreparationRemainingWallNumberChangedEventDelegate PreparationRemainingTowerNumberChangedEvent;
    public static event RealTimeRemainingWallNumberChangedEventDelegate RealTimeRemainingTowerNumberChangedEvent;
    public static event EnemyGotDestroyedEventDelegate EnemyGotDestroyedEvent;
    public static event ConfirmLevelStartWithRemainingPreparationPieces ConfirmLevelStartEvent;
    #endregion

    //Event invocations are the one location where an event is triggered, and then the event will call all event methods stored in it.
    #region invocations
    public static void InvokeToyReachedBedEvent(GameObject enemy)
    {
        ToyReachedBedEvent.Invoke(enemy);
    }

    public static void InvokeGameOverEvent(GameOverType gameOverType)
    {
        GameOverEvent.Invoke(gameOverType);
    }

    public static void InvokeStartRealTimeStageEvent()
    {
        StartRealTimeStageEvent.Invoke();
    }

    public static void InvokePreparationPuzzlePiecePlacementEvent()
    {
        PreparationPuzzlePiecePlacementEvent.Invoke();
    }

    public static void InvokeOutOfPreparationPiecesEvent()
    {
        OutOfPreparationPiecesEvent.Invoke();
    }

    public static void InvokeRealTimePuzzlePiecePlacementEvent()
    {
        RealTimePuzzlePiecePlacementEvent.Invoke();
    }

    public static void InvokeOutOfRealTimePiecesEvent()
    {
        OutOfRealTimePiecesEvent.Invoke();
    }

    public static void InvokeToyBlowsUpWallEvent(GameObject wallToBeDestroyed)
    {
        ToyBlowsUpWallEvent.Invoke(wallToBeDestroyed);
    }

    public static void InvokePreparationRemainingWallNumberChangedEvent()
    {
        PreparationRemainingWallNumberChangedEvent.Invoke();
    }
    public static void InvokeRealTimeRemainingWallNumberChangedEvent()
    {
        RealTimeRemainingWallNumberChangedEvent.Invoke();
    }
    public static void InvokePreparationRemainingTowerNumberChangedEvent()
    {
        PreparationRemainingTowerNumberChangedEvent.Invoke();
    }
    public static void InvokeRealTimeRemainingTowerNumberChangedEvent()
    {
        RealTimeRemainingTowerNumberChangedEvent.Invoke();
    }
    public static void InvokeEnemyGotDestroyedEvent(GameObject enemy)
    {
        if (EnemyGotDestroyedEvent != null)
        {
            EnemyGotDestroyedEvent.Invoke(enemy);
        }
    }

    public static void InvokeConfirmLevelStartEvent()
    {
        ConfirmLevelStartEvent.Invoke();
    }
    #endregion
}
