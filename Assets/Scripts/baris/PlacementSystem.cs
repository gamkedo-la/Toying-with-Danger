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

    [SerializeField]
    private GameObject wallObject;
    [SerializeField]
    private GameObject wallParentObject;

    [SerializeField]
    private GameObject enemyObject;
    [SerializeField]
    private GameObject enemyParentObject;
    [SerializeField]
    private GameObject enemyDestination;


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
    }

    private void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(enemyObject, enemyParentObject.transform.position, Quaternion.identity, wallParentObject.transform);
        enemy.GetComponent<EnemyScript>().SetDestination(enemyDestination.transform.position);
    }

    private void SpawnWall()
    {
        Vector3 mousePosition = GetSelectedMapPosition();
        var wall = GameObject.Instantiate(wallObject, mousePosition, Quaternion.identity, wallParentObject.transform);
        NavigationBaker.Instance.surfaces.Add(wall.GetComponent<NavMeshSurface>());
        NavigationBaker.Instance.BuildNavMesh();
    }

    private Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100, LayerMask.NameToLayer("default")))
        {
            Vector3Int gridPosition = grid.WorldToCell(raycastHit.point);
            lastPosition = grid.CellToWorld(gridPosition);
        }

        return lastPosition;
    }
}
