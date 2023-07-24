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
    [SerializeField] TextMeshProUGUI hitPointsTextGameObject;
    [SerializeField] TextMeshProUGUI preparationStageTextGameObject;
    [SerializeField] TextMeshProUGUI timerTextGameObject;
    [SerializeField] TextMeshProUGUI preparationPiecesTextGameObject;
    #endregion

    #region event subscriptions
    private void OnEnable()
    {
        EventManagerScript.PreparationPuzzlePiecePlacementEvent += HandlePreparationPuzzlePiecePlacementEvent;
        EventManagerScript.OutOfPreparationPiecesEvent += HandleOutOfPreparationPiecesEvent;
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
        EventManagerScript.RealTimePuzzlePiecePlacementEvent += HandleRealTimePuzzlePiecePlacementEvent;
        EventManagerScript.OutOfRealTimePiecesEvent += HandleOutOfRealTimePiecesEvent;
        EventManagerScript.ToyReachedBedEvent += HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent += HandleGameOverEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.PreparationPuzzlePiecePlacementEvent -= HandlePreparationPuzzlePiecePlacementEvent;
        EventManagerScript.OutOfPreparationPiecesEvent -= HandleOutOfPreparationPiecesEvent;
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
        EventManagerScript.RealTimePuzzlePiecePlacementEvent -= HandleRealTimePuzzlePiecePlacementEvent;
        EventManagerScript.OutOfRealTimePiecesEvent -= HandleOutOfRealTimePiecesEvent;
        EventManagerScript.ToyReachedBedEvent -= HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent -= HandleGameOverEvent;
    }
    #endregion

    private void Start()
    {
        hitPointsTextGameObject.text = "Hit points: " + GameManagerScript.hitPoints.ToString();
        preparationPiecesTextGameObject.text = "Preparation Pieces: " + GameManagerScript.totalPreparationStagePuzzlePieces.ToString();
    }

    #region preparation stage
    private void HandlePreparationPuzzlePiecePlacementEvent()
    {
        preparationPiecesTextGameObject.text = "Preparation Pieces: " + GameManagerScript.preparationStagePuzzlePiecesLeft;
    }

    private void HandleOutOfPreparationPiecesEvent()
    {
        preparationStageTextGameObject.text = "You're out of preparation pieces. Press space when ready.";
    }
    #endregion

    #region real time stage
    private void HandleStartRealTimeStageEvent()
    {
        preparationStageTextGameObject.gameObject.SetActive(false);
        timerTextGameObject.gameObject.SetActive(true);
        preparationPiecesTextGameObject.text = "Real Time Pieces Left: " + GameManagerScript.totalRealTimeStagePuzzlePieces;
    }

    private void HandleRealTimePuzzlePiecePlacementEvent()
    {
        preparationPiecesTextGameObject.text = "Real Time Pieces Left: " + GameManagerScript.realTimeStagePuzzlePiecesLeft;
    }

    private void HandleOutOfRealTimePiecesEvent()
    {
        preparationStageTextGameObject.gameObject.SetActive(true);
        preparationStageTextGameObject.text = "You're out of pieces. Good luck!";
    }

    #region toy reached bed event
    private void HandleToyReachedBedEvent()
    {
        DecrementHitPoints();
        UpdateHitPointsText();
        CheckIfAllHitPointsAreGoneAndTriggerGameOverIfAppropriate();
    }

    private void DecrementHitPoints()
    {
        GameManagerScript.hitPoints--;
        if (GameManagerScript.hitPoints < 0)
        {
            GameManagerScript.hitPoints = 0;
        }
    }

    private void CheckIfAllHitPointsAreGoneAndTriggerGameOverIfAppropriate()
    {
        if (GameManagerScript.hitPoints == 0)
        {
            EventManagerScript.InvokeGameOverEvent();
        }
    }

    private void UpdateHitPointsText()
    {
        hitPointsTextGameObject.text = "Hit points: " + GameManagerScript.hitPoints.ToString();
    }
    #endregion
    #endregion

    private void HandleGameOverEvent()
    {
        notificationTextGameObject.GetComponent<TextMeshProUGUI>().text = "Game Over";
        notificationTextGameObject.SetActive(true);
    }
}
