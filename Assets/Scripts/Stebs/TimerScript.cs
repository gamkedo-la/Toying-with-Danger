using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private float currentLevelTimerDuration;

    private TextMeshProUGUI textmeshComponent;

    [SerializeField] TextMeshProUGUI winLoseTextmeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelTimerDuration = GameManagerScript.GameManagerScriptInstance.level_1_time_duration;

        textmeshComponent = gameObject.transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelTimerDuration -= Time.deltaTime;

        textmeshComponent.text = currentLevelTimerDuration.ToString();

        if (currentLevelTimerDuration <= 0)
        {
            currentLevelTimerDuration = 0;
            winLoseTextmeshProUGUI.text = "You win!";
        }
    }
}
