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
        currentLevelTimerDuration -= Time.deltaTime;

        canvasHUD_TimerTextGameObject.text = "Time left: " + currentLevelTimerDuration.ToString();

        if (currentLevelTimerDuration <= 0)
        {
            currentLevelTimerDuration = 0;
        }
    }
}
