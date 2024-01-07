using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private float currentLevelTimerDuration;
    private bool timerShouldBeRunning = true;

    [SerializeField] TextMeshProUGUI canvasHUD_TimerTextGameObject;

    #region
    private void OnEnable()
    {
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentLevelTimerDuration = GameManagerScript.totalLevelTime;

        canvasHUD_TimerTextGameObject = gameObject.transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerShouldBeRunning)
        {
            if (currentLevelTimerDuration > 0)
            {
                currentLevelTimerDuration -= Time.deltaTime;

            }
            else
            {
                currentLevelTimerDuration = 0;
                timerShouldBeRunning = false;
                EventManagerScript.InvokeGameOverEvent(GameOverType.GameWon);
            }
        }
        

        canvasHUD_TimerTextGameObject.text = "Time left: " + currentLevelTimerDuration.ToString();
    }

    private void HandleStartRealTimeStageEvent()
    {
        timerShouldBeRunning = true;
    }
}
