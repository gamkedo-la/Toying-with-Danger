using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerDescriptionButtonScript : MonoBehaviour
{
    public GameObject TowerMenuVerticalGroup;
    public GameObject helpMenuVerticalGroup;

    public GameObject towerButtonPrefab;
    public Transform towerVerticalGroupTransform;
    public GameObject towerPlaceHolder;
    public TextMeshProUGUI descriptionTextForTower;
    public void TowerDescriptionButtonClick()
    {
        TowerMenuVerticalGroup.SetActive(true);
        helpMenuVerticalGroup.SetActive(false);

        List<TowerScriptableObject> towerScriptableObjects = TowerFactory.GetAllTowerScriptableObjects();

        for (int i = 0; i < towerScriptableObjects.Count; i++)
        {
            GameObject towerButton = GameObject.Instantiate(towerButtonPrefab, towerVerticalGroupTransform);
            towerButton.GetComponentInChildren<TextMeshProUGUI>().text = towerScriptableObjects[i].nameForUI;
            TowerInformationButtonScript towerInformationButtonScript = towerButton.GetComponent<TowerInformationButtonScript>();
            towerInformationButtonScript.towerScriptableObject = towerScriptableObjects[i];
            towerInformationButtonScript.towerPlaceHolder = towerPlaceHolder;
            towerInformationButtonScript.descriptionTextForTower = descriptionTextForTower;
        }
    }
}
