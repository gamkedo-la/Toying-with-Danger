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

    static GameObject CreateTowerInner(TowerScriptableObject towerScriptableObject, Transform parent, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        var towerGameObject = GameObject.Instantiate(towerScriptableObject.prefab, spawnPosition, spawnRotation, parent);
        towerGameObject.GetComponent<TowerAbility>().SetColliderRadius(towerScriptableObject.towerRadius);
        return towerGameObject;
    }

    public static GameObject CreateRandomTower(Transform parent, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        InstantiateTowerFactory();
        TowerScriptableObject randomTowerScriptableObject = allTowerScriptableObjects[Random.Range(0, allTowerScriptableObjects.Count)];
        var towerGameObject = CreateTowerInner(randomTowerScriptableObject, parent, spawnPosition, spawnRotation);
        return towerGameObject;
    }
    public static GameObject CreateTower(TowerScriptableObject towerScriptableObject, Transform parent, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        InstantiateTowerFactory();
        var towerGameObject = CreateTowerInner(towerScriptableObject, parent, spawnPosition, spawnRotation);
        return towerGameObject;
    }

    public static List<TowerScriptableObject> GetAllTowerScriptableObjects()
    {
        InstantiateTowerFactory();
        return allTowerScriptableObjects;
    }
}
