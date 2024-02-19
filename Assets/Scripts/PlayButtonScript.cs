using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    public void HandlePlayButtonClick()
    {
        Debug.Log(LoadingManager.Instance);
        LoadingManager.Instance.LoadScene(GameEnumsNamespace.GameSceneEnums.Main);
    }
}
