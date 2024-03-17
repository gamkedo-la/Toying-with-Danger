using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManagerScript : MonoBehaviour
{
    public static DebugManagerScript Instance { get; private set; }

#if UNITY_EDITOR
    void Update()
    {
        //Rebuild All navmeshes
        if (Input.GetKeyUp(KeyCode.F1))
        {
            NavigationBaker.Instance.BuildNavMesh();
        }
        //Add 1 more wall
        if (Input.GetKeyUp(KeyCode.F2))
        {
            switch (GameManagerScript.GameManagerScriptInstance.currentGameState)
            {
                case GameManagerScript.GameState.preparationStage:
                    GameManagerScript.GameManagerScriptInstance.preparationStageWallsLeft += 1;
                    EventManagerScript.InvokePreparationRemainingWallNumberChangedEvent();
                    break;
                case GameManagerScript.GameState.realTimeStage:
                    GameManagerScript.GameManagerScriptInstance.realTimeStageWallsLeft += 1;
                    EventManagerScript.InvokeRealTimeRemainingWallNumberChangedEvent();
                    break;
                case GameManagerScript.GameState.gameOver:
                    break;
                default:
                    break;
            }
        }
    }
#endif
}

