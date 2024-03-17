using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_Canvas_Script : MonoBehaviour
{
    ///Summary
    ///Define behavior of HUD_Canvas
    ///

    #region cached references
    [SerializeField] GameObject notificationTextGameObject;
    private TextMeshProUGUI notificationTextMesh;
    [SerializeField] Slider hitPointsTextGameObject;
    [SerializeField] TextMeshProUGUI preparationStageTextGameObject;
    [SerializeField] TextMeshProUGUI timerTextGameObject;
    [SerializeField] TextMeshProUGUI preparationWallPiecesTextGameObject;
    [SerializeField] TextMeshProUGUI preparationTowerPiecesTextGameObject;
    #endregion

    #region event subscriptions
    private void OnEnable()
    {
        EventManagerScript.PreparationRemainingWallNumberChangedEvent += HandlePreparationRemainingWallNumberChangedEvent;
        EventManagerScript.PreparationRemainingTowerNumberChangedEvent += HandlePreparationRemainingTowerNumberChangedEvent;
        EventManagerScript.OutOfPreparationPiecesEvent += HandleOutOfPreparationPiecesEvent;
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
        EventManagerScript.RealTimeRemainingWallNumberChangedEvent += HandleRealTimeRemainingWallNumberChangedEvent;
        EventManagerScript.RealTimeRemainingTowerNumberChangedEvent += HandleRealTimeRemainingTowerNumberChangedEvent;
        EventManagerScript.OutOfRealTimePiecesEvent += HandleOutOfRealTimePiecesEvent;
        EventManagerScript.ToyReachedBedEvent += HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent += HandleGameOverEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.PreparationRemainingWallNumberChangedEvent -= HandlePreparationRemainingWallNumberChangedEvent;
        EventManagerScript.PreparationRemainingTowerNumberChangedEvent -= HandlePreparationRemainingTowerNumberChangedEvent;
        EventManagerScript.OutOfPreparationPiecesEvent -= HandleOutOfPreparationPiecesEvent;
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
        EventManagerScript.RealTimeRemainingWallNumberChangedEvent -= HandleRealTimeRemainingWallNumberChangedEvent;
        EventManagerScript.RealTimeRemainingTowerNumberChangedEvent -= HandleRealTimeRemainingTowerNumberChangedEvent;
        EventManagerScript.OutOfRealTimePiecesEvent -= HandleOutOfRealTimePiecesEvent;
        EventManagerScript.ToyReachedBedEvent -= HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent -= HandleGameOverEvent;
    }
    #endregion

    private void Start()
    {
        hitPointsTextGameObject.maxValue = GameManagerScript.GameManagerScriptInstance.hitPoints;
        hitPointsTextGameObject.value = GameManagerScript.GameManagerScriptInstance.hitPoints;
        preparationWallPiecesTextGameObject.text = "Preparation Wall Pieces: " + GameManagerScript.GameManagerScriptInstance.totalPreparationWalls.ToString();
        preparationTowerPiecesTextGameObject.text = "Preparation Tower Pieces: " + GameManagerScript.GameManagerScriptInstance.totalPreparationTowers.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            preparationWallPiecesTextGameObject.gameObject.SetActive(!preparationWallPiecesTextGameObject.gameObject.activeSelf);
            preparationTowerPiecesTextGameObject.gameObject.SetActive(!preparationTowerPiecesTextGameObject.gameObject.activeSelf);
        }
    }

    #region preparation stage
    private void HandlePreparationRemainingWallNumberChangedEvent()
    {
        preparationWallPiecesTextGameObject.text = "Preparation Wall Pieces: " + GameManagerScript.GameManagerScriptInstance.preparationStageWallsLeft;
    }

    private void HandlePreparationRemainingTowerNumberChangedEvent()
    {
        preparationTowerPiecesTextGameObject.text = "Preparation Tower Pieces: " + GameManagerScript.GameManagerScriptInstance.preparationStageTowersLeft;
    }

    private void HandleOutOfPreparationPiecesEvent()
    {
        preparationWallPiecesTextGameObject.text = "You're out of preparation pieces. Press space when ready.";
    }
    #endregion

    #region real time stage
    private void HandleStartRealTimeStageEvent()
    {
        timerTextGameObject.gameObject.SetActive(true);
        preparationWallPiecesTextGameObject.text = "Real Time Wall Pieces Left: " + GameManagerScript.GameManagerScriptInstance.totalRealTimeStageWalls;
        preparationTowerPiecesTextGameObject.text = "Real Time Tower Pieces Left: " + GameManagerScript.GameManagerScriptInstance.totalRealTimeStageTowers;
        preparationStageTextGameObject.text = "You now have some more pieces. Stop the toys!";
    }

    private void HandleRealTimeRemainingWallNumberChangedEvent()
    {
        preparationWallPiecesTextGameObject.text = "Real Time Wall Pieces Left: " + GameManagerScript.GameManagerScriptInstance.realTimeStageWallsLeft;
    }

    private void HandleRealTimeRemainingTowerNumberChangedEvent()
    {
        preparationTowerPiecesTextGameObject.text = "Real Time Tower Pieces Left: " + GameManagerScript.GameManagerScriptInstance.realTimeStageTowersLeft;
    }

    private void HandleOutOfRealTimePiecesEvent()
    {
        preparationStageTextGameObject.gameObject.SetActive(true);
        preparationStageTextGameObject.text = "You're out of pieces. Good luck!";
    }

    #region toy reached bed event
    private void HandleToyReachedBedEvent(GameObject enemy)
    {
        DecrementHitPoints();
        UpdateHitPointsText();
        CheckIfAllHitPointsAreGoneAndTriggerGameOverIfAppropriate();
        EventManagerScript.InvokeEnemyGotDestroyedEvent(enemy);
        Destroy(enemy);
    }

    private void DecrementHitPoints()
    {
        GameManagerScript.GameManagerScriptInstance.hitPoints--;
        if (GameManagerScript.GameManagerScriptInstance.hitPoints < 0)
        {
            GameManagerScript.GameManagerScriptInstance.hitPoints = 0;
        }
    }

    private void CheckIfAllHitPointsAreGoneAndTriggerGameOverIfAppropriate()
    {
        if (GameManagerScript.GameManagerScriptInstance.hitPoints == 0)
        {
            EventManagerScript.InvokeGameOverEvent(GameOverType.healthReachedZero);
        }
    }

    private void UpdateHitPointsText()
    {
        hitPointsTextGameObject.value = GameManagerScript.GameManagerScriptInstance.hitPoints;
    }
    #endregion
    #endregion

    private void HandleGameOverEvent(GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameOverType.healthReachedZero:
                notificationTextGameObject.GetComponent<TextMeshProUGUI>().text = "Game Over. Your Health Reached Zero";
                break;
            case GameOverType.NoPathAvailableForEnemy:
                notificationTextGameObject.GetComponent<TextMeshProUGUI>().text = "Game Over. You cannot block enemies path entirely";
                break;
            case GameOverType.GameWon:
                notificationTextGameObject.GetComponent<TextMeshProUGUI>().text = "You win! Go back to bed and get a good night's sleep!";
                break;
            default:
                break;
        }
        
        notificationTextGameObject.SetActive(true);
    }
}
