using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonScript : MonoBehaviour
{
    public GameObject mainMenuVerticalGroup;
    public GameObject helpMenuVerticalGroup;
    public void HandleHelpButtonClick()
    {
        mainMenuVerticalGroup.SetActive(false);
        helpMenuVerticalGroup.SetActive(true);
    }
}
