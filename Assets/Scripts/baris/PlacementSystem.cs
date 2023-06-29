using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private Grid grid;

    private Vector3 lastPosition;

    [Header("Wall")]
    [SerializeField]
    private GameObject wallObject;
    [SerializeField]
    private GameObject wallParentObject;

    [Header("Enemy")]
    [SerializeField]
    private GameObject enemyObject;
    [SerializeField]
    private GameObject enemyParentObject;
    [SerializeField]
    private GameObject enemyDestinations;

    [Header("Tower")]
    [SerializeField]
    private GameObject towerObject;
    [SerializeField]
    private GameObject towerParentObject;


    void Update()
    {
        Vector3 mousePosition = GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnWall();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            SpawnTower();
        }
    }

    private void SpawnWall()
    {
        Vector3 mousePosition = GetSelectedMapPosition();
        var wall = GameObject.Instantiate(wallObject, mousePosition, Quaternion.identity, wallParentObject.transform);
        NavigationBaker.Instance.surfaces.Add(wall.GetComponent<NavMeshSurface>());
        NavigationBaker.Instance.BuildNavMesh();
    }
    private void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(enemyObject, enemyParentObject.transform.position, Quaternion.identity, wallParentObject.transform);
        enemy.GetComponent<EnemyScript>().SetDestinationObject(enemyDestinations);
    }
    private void SpawnTower()
    {
        Vector3 mousePosition = GetSelectedMapPosition();
        var tower = GameObject.Instantiate(towerObject, mousePosition, Quaternion.identity, towerParentObject.transform);
        NavigationBaker.Instance.surfaces.Add(tower.GetComponent<NavMeshSurface>());
        NavigationBaker.Instance.BuildNavMesh();
    }

    private Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100, LayerMask.NameToLayer("Ground")))
        {
            Vector3Int gridPosition = grid.WorldToCell(raycastHit.point);
            lastPosition = grid.CellToWorld(gridPosition);
        }

        return lastPosition;
    }
}
