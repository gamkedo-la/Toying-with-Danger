using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateTowerVerticalGroup : MonoBehaviour
{
    public GameObject towerButtonPrefab;
    public Transform towerVerticalGroupTransform;

    private void Start()
    {
        List<TowerScriptableObject> towerScriptableObjects = TowerFactory.GetAllTowerScriptableObjects();

        for (int i = 0; i < towerScriptableObjects.Count; i++)
        {
            GameObject towerButton = GameObject.Instantiate(towerScriptableObjects[i].prefab, towerVerticalGroupTransform);
        }
    }
}
