using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManagerScriptInstance;


    #region designer properties
    [Tooltip("The total hit points at the start of the level.")]
    public static int hitPoints = 3;
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
}
