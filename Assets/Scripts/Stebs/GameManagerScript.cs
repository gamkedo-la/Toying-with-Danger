using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;


    #region designer properties
    public enum GameState { preparationStage, realTimeStage, gameOver }
    public static GameState currentGameState = GameState.preparationStage;

    [Tooltip("The total number of puzzle pieces the player can place during the preparation stage.")]
    public static int totalPreparationStagePuzzlePieces = 5;
    public static int preparationStagePuzzlePiecesLeft = 5;

    [Tooltip("The total hit points at the start of the level.")]
    public static int hitPoints = 3;

    [Tooltip("Total amount of time for the level.")]
    public static float totalLevelTime = 30.0f;

    #endregion

    #region event subscriptions

    private void OnEnable()
    {
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
        EventManagerScript.GameOverEvent += HandleGameOverEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
        EventManagerScript.GameOverEvent -= HandleGameOverEvent;
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

    private void HandleGameOverEvent()
    {
        currentGameState = GameState.gameOver;
    }
}
