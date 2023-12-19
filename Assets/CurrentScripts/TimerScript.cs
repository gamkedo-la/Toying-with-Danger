using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private float currentLevelTimerDuration;

    [SerializeField] TextMeshProUGUI canvasHUD_TimerTextGameObject;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelTimerDuration = GameManagerScript.totalLevelTime;

        canvasHUD_TimerTextGameObject = gameObject.transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevelTimerDuration > 0)
        {
            currentLevelTimerDuration -= Time.deltaTime;
            
        }
        else
        {
            currentLevelTimerDuration = 0;
        }

        canvasHUD_TimerTextGameObject.text = "Time left: " + currentLevelTimerDuration.ToString();
    }
}
