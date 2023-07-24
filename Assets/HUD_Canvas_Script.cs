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
    #endregion

    #region event subscriptions
    private void OnEnable()
    {
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
        EventManagerScript.ToyReachedBedEvent += HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent += HandleGameOverEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
        EventManagerScript.ToyReachedBedEvent -= HandleToyReachedBedEvent;
        EventManagerScript.GameOverEvent -= HandleGameOverEvent;
    }
    #endregion

    private void Start()
    {
        hitPointsTextGameObject.text = "Hit points: " + GameManagerScript.hitPoints.ToString();
    }

    private void HandleGameOverEvent()
    {
        notificationTextGameObject.GetComponent<TextMeshProUGUI>().text = "Game Over";
        notificationTextGameObject.SetActive(true);
    }

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

    private void HandleStartRealTimeStageEvent()
    {
        preparationStageTextGameObject.gameObject.SetActive(false);
        timerTextGameObject.gameObject.SetActive(true);
    }
}
