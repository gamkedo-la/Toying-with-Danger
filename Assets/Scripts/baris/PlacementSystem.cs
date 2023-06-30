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
    private MeshRenderer mouseIndicatorsMeshRenderer;

    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Grid grid2;

    [SerializeField]
    Material unusableIndicatorMaterial;
    [SerializeField]
    Material usableIndicatorMaterial;

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

    private void Start()
    {
        mouseIndicatorsMeshRenderer = mouseIndicator.transform.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector3 mousePosition = GetSelectedMapPosition();
        //Debug.Log("mousePosition: " + mousePosition);
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
        //Debug.Log("mousePos: " + mousePos);
        mousePos.z = Camera.main.nearClipPlane;
        //Debug.Log("mousePos.z: " + mousePos.z);

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100/*, LayerMask.NameToLayer("Ground")*/))//
        {
            if (raycastHit.transform.tag == "Ground")
            {
                mouseIndicatorsMeshRenderer.material = usableIndicatorMaterial;
            }
            else
            {
                mouseIndicatorsMeshRenderer.material = unusableIndicatorMaterial;
            }


            if (raycastHit.transform.gameObject.name == "GroundWithGrid")
            {
                Debug.Log("grid1 hit");
                Vector3Int gridPosition = grid.WorldToCell(raycastHit.point);
                lastPosition = grid.CellToWorld(gridPosition);
            }
            else if (raycastHit.transform.gameObject.name == "GroundWithGrid (1)")
            {
                Debug.Log("grid2 hit");
                Vector3Int gridPosition = grid2.WorldToCell(raycastHit.point);
                lastPosition = grid2.CellToWorld(gridPosition);
            }    
        }

        return lastPosition;
    }
}
