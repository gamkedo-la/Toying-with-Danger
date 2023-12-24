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
        EventManagerScript.PreparationRemainingWallNumberChangedEvent += HandlePreparationRemainingWallNumberChangedEvent;
        EventManagerScript.OutOfPreparationPiecesEvent += HandleOutOfPreparationPiecesEvent;
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
        EventManagerScript.RealTimeRemainingWallNumberChangedEvent += HandleRealTimeRemainingWallNumberChangedEvent;
        EventManagerScript.OutOfRealTimePiecesEvent += HandleOutOfRealTimePiecesEvent;
        EventManagerScript.ToyReachedBedEvent += HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent += HandleGameOverEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.PreparationRemainingWallNumberChangedEvent -= HandlePreparationRemainingWallNumberChangedEvent;
        EventManagerScript.OutOfPreparationPiecesEvent -= HandleOutOfPreparationPiecesEvent;
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
        EventManagerScript.RealTimeRemainingWallNumberChangedEvent -= HandleRealTimeRemainingWallNumberChangedEvent;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            preparationStageTextGameObject.gameObject.SetActive(!preparationStageTextGameObject.gameObject.activeSelf);
        }
    }

    #region preparation stage
    private void HandlePreparationRemainingWallNumberChangedEvent()
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
        timerTextGameObject.gameObject.SetActive(true);
        preparationPiecesTextGameObject.text = "Real Time Pieces Left: " + GameManagerScript.totalRealTimeStagePuzzlePieces;
        preparationStageTextGameObject.text = "You now have some more pieces. Stop the toys!";
    }

    private void HandleRealTimeRemainingWallNumberChangedEvent()
    {
        preparationPiecesTextGameObject.text = "Real Time Pieces Left: " + GameManagerScript.realTimeStagePuzzlePiecesLeft;
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
        Destroy(enemy);
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
