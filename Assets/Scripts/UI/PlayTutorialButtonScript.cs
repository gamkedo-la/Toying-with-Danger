using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelOneButtonScript : MonoBehaviour
{
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
    public void HandlePlayLevelOneButtonClick()
    {
        Debug.Log(LoadingManager.Instance);
        SetGameManagerValues();
        LoadingManager.Instance.LoadScene(GameEnumsNamespace.GameSceneEnums.TutorialLevel);
    }

    private void SetGameManagerValues()
    {
        GameManagerScript.GameManagerScriptInstance.totalPreparationWalls = totalPreparationWalls;
        GameManagerScript.GameManagerScriptInstance.preparationStageWallsLeft = preparationStageWallsLeft;
        GameManagerScript.GameManagerScriptInstance.totalRealTimeStageWalls = totalRealTimeStageWalls;
        GameManagerScript.GameManagerScriptInstance.realTimeStageWallsLeft = realTimeStageWallsLeft;
        GameManagerScript.GameManagerScriptInstance.totalPreparationTowers = totalPreparationTowers;
        GameManagerScript.GameManagerScriptInstance.preparationStageTowersLeft = preparationStageTowersLeft;
        GameManagerScript.GameManagerScriptInstance.totalRealTimeStageTowers = totalRealTimeStageTowers;
        GameManagerScript.GameManagerScriptInstance.realTimeStageTowersLeft = realTimeStageTowersLeft;
        GameManagerScript.GameManagerScriptInstance.hitPoints = hitPoints;
        GameManagerScript.GameManagerScriptInstance.totalLevelTime = totalLevelTime;
    }
}
