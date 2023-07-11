using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;

    public float floatDistanceForTriggeringBaseInvasion = 1.0f;

    public float wallPositionMovementFloat = 1f;

    public float cowmationSpawnInterval = 10.0f;

    public float level_1_time_duration = 30.0f;

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
}
