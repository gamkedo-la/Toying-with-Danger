using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;


    #region designer properties
    public enum GameState { preparationStage, realTimeStage, gameOver }
    public GameState currentGameState = GameState.preparationStage;

    [Tooltip("The total number of walls the player can place during the preparation stage.")]
    public int totalPreparationWalls = 7;

    public int preparationStageWallsLeft = 7;

    [Tooltip("The total number of walls the player can place during the real time stage.")]
    public int totalRealTimeStageWalls = 7;

    public int realTimeStageWallsLeft = 7;

    [Tooltip("The total number of walls the player can place during the preparation stage.")]
    public int totalPreparationTowers = 2;

    public int preparationStageTowersLeft = 2;

    [Tooltip("The total number of walls the player can place during the real time stage.")]
    public int totalRealTimeStageTowers = 2;

    public int realTimeStageTowersLeft = 2;

    [Tooltip("The total hit points at the start of the level.")]
    public int hitPoints = 3;

    [Tooltip("Total amount of time for the level.")]
    public float totalLevelTime = 40.0f;

    public bool preventBlockPlacement = false;

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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        //end preparation stage and start real-time stage when space bar is pressed
        if (Input.GetKeyUp(KeyCode.Space) && GameManagerScript.GameManagerScriptInstance.currentGameState == GameManagerScript.GameState.preparationStage)
        {
            if (GameManagerScript.GameManagerScriptInstance.preparationStageWallsLeft == 0 && GameManagerScript.GameManagerScriptInstance.preparationStageTowersLeft == 0)
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
        GameManagerScript.GameManagerScriptInstance.currentGameState = GameManagerScript.GameState.realTimeStage;
    }

    private void HandleGameOverEvent(GameOverType gameOverType)
    {
        currentGameState = GameState.gameOver;
    }
}
