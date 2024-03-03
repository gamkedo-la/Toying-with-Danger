using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenuButtonScript : MonoBehaviour
{
    public GameObject mainMenuVerticalGroup;
    public GameObject helpMenuVerticalGroup;
    public void HandleBackToMainMenuButtonClick()
    {
        mainMenuVerticalGroup.SetActive(true);
        helpMenuVerticalGroup.SetActive(false);
    }
}
