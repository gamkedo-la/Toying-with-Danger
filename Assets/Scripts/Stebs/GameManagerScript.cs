using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;


    #region designer properties
    [Tooltip("The total hit points at the start of the level.")]
    public static int hitPoints;
    #endregion

    #region event subscriptions

    private void OnEnable()
    {
        EventManagerScript.ToyReachedBedEvent += HandleToyReachedBedEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.ToyReachedBedEvent -= HandleToyReachedBedEvent;
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

    private void HandleToyReachedBedEvent()
    {
        DecrementHitPoints();
        CheckIfAllHitPointsAreGoneAndTriggerGameOverIfAppropriate();
    }

    private void DecrementHitPoints()
    {
        hitPoints--;
    }

    private void CheckIfAllHitPointsAreGoneAndTriggerGameOverIfAppropriate()
    {
        if (hitPoints == 0)
        {
            EventManagerScript.InvokeGameOverEvent();
        }
    }
}
