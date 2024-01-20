using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerScriptableObject", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public float towerRadius;
}
