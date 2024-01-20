using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class TowerFactory
{
    private static string towerScriptableObjectFolderPath = "Towers";
    private static List<TowerScriptableObject> allTowerScriptableObjects;
    static void InstantiateTowerFactory()
    {
        if (allTowerScriptableObjects == null)
        {
            allTowerScriptableObjects = Resources.LoadAll(towerScriptableObjectFolderPath, typeof(TowerScriptableObject)).Cast<TowerScriptableObject>().ToList();
        }
        
    }

    public static GameObject CreateRandomTower(Transform parent, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        InstantiateTowerFactory();
        TowerScriptableObject randomTowerScriptableObject = allTowerScriptableObjects[Random.Range(0, allTowerScriptableObjects.Count)];
        var towerGameObject = GameObject.Instantiate(randomTowerScriptableObject.prefab, spawnPosition, spawnRotation, parent);
        towerGameObject.GetComponent<TowerAbility>().DetectionRadius = randomTowerScriptableObject.towerRadius;
        return towerGameObject;
    }
}
