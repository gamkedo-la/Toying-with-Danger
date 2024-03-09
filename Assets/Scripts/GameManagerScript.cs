using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;


    #region designer properties
    public enum GameState { preparationStage, realTimeStage, gameOver }
    public static GameState currentGameState = GameState.preparationStage;

    [Tooltip("The total number of walls the player can place during the preparation stage.")]
    public static int totalPreparationWalls = 10;

    public static int preparationStageWallsLeft = 10;

    [Tooltip("The total number of walls the player can place during the real time stage.")]
    public static int totalRealTimeStageWalls = 10;

    public static int realTimeStageWallsLeft = 10;

    [Tooltip("The total number of walls the player can place during the preparation stage.")]
    public static int totalPreparationTowers = 2;

    public static int preparationStageTowersLeft = 2;

    [Tooltip("The total number of walls the player can place during the real time stage.")]
    public static int totalRealTimeStageTowers = 2;

    public static int realTimeStageTowersLeft = 2;

    [Tooltip("The total hit points at the start of the level.")]
    public static int hitPoints = 3;

    [Tooltip("Total amount of time for the level.")]
    public static float totalLevelTime = 20.0f;

    public static bool preventBlockPlacement = false;

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
            if (GameManagerScript.preparationStageWallsLeft == 0 && GameManagerScript.preparationStageTowersLeft == 0)
            {
                EventManagerScript.InvokeStartRealTimeStageEvent();
            }
            else
            {
                EventManagerScript.InvokeConfirmLevelStartEvent();
            }
        }
    }

    private void HandleStartRealTimeStageEvent()
    {
        GameManagerScript.currentGameState = GameManagerScript.GameState.realTimeStage;
    }

    private void HandleGameOverEvent(GameOverType gameOverType)
    {
        currentGameState = GameState.gameOver;
    }
}
