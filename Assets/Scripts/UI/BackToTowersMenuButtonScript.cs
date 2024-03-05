using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTowersMenuButtonScript : MonoBehaviour
{
    public GameObject towersMenuVerticalGroup;
    public GameObject helpMenuVerticalGroup;
    public void HandleBackToTowersMenuButtonClick()
    {
        helpMenuVerticalGroup.SetActive(true);
        towersMenuVerticalGroup.SetActive(false);
    }
}
