using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartConfirmationMenu : MonoBehaviour
{
    #region event subscriptions

    private void OnEnable()
    {
        EventManagerScript.ConfirmLevelStartEvent += OpenLevelStartConfirmationMenuEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.ConfirmLevelStartEvent -= OpenLevelStartConfirmationMenuEvent;
    }

    #endregion

    private void OpenLevelStartConfirmationMenuEvent()
    {
        GameManagerScript.preventBlockPlacement = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HandleBeginLevelClick()
    {
        EventManagerScript.InvokeStartRealTimeStageEvent();
        StartCoroutine("WaitToExitMenu");
    }

    public void HandleExitMenu()
    {
        StartCoroutine("WaitToExitMenu");
    }

    // Prevents a block from being placed under the menu buttons which would happen if menu is closed and block placement is reenabled in the same frame
    private IEnumerator WaitToExitMenu()
    {
        yield return null;

        GameManagerScript.preventBlockPlacement = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

}