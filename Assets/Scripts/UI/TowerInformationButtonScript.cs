using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TowerInformationButtonScript : MonoBehaviour
{
    public TowerScriptableObject towerScriptableObject;
    public TextMeshProUGUI descriptionTextForTower;
    public GameObject towerPlaceHolder;

    public void HandleTowerInformationButtonClick()
    {
        for (int i = 0; i < towerPlaceHolder.transform.childCount; i++)
        {
            GameObject.Destroy(towerPlaceHolder.transform.GetChild(i).gameObject);
        }

        descriptionTextForTower.text = towerScriptableObject.descriptionForMainMenu;
        var towerObject = GameObject.Instantiate(towerScriptableObject.prefab, towerPlaceHolder.transform.position, towerPlaceHolder.transform.rotation, towerPlaceHolder.transform);
        towerObject.GetComponent<Collider>().enabled = false;
    }
}
