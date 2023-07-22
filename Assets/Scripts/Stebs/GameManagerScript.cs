using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;


    #region designer properties
    public enum GameState { preparationStage, realTimeStage, gameOver }
    public static GameState currentGameState = GameState.preparationStage;

    [Tooltip("The total hit points at the start of the level.")]
    public static int hitPoints = 3;

    #endregion

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

    public void Awake()
    {
        if (GameManagerScriptInstance == null)
        {
            GameManagerScriptInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //end preparation stage and start real-time stage when space bar is pressed
        if (Input.GetKeyUp(KeyCode.Space) && GameManagerScript.currentGameState == GameManagerScript.GameState.preparationStage)
        {
            EventManagerScript.InvokeStartRealTimeStageEvent();
        }
    }

    private void HandleStartRealTimeStageEvent()
    {
        GameManagerScript.currentGameState = GameManagerScript.GameState.realTimeStage;
    }
}
