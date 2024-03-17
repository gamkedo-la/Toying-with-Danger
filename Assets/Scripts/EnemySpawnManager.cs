using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region designer properties
    [SerializeField]
    private int totalNumberOfEnemiesToSpawn;

    [SerializeField]
    [Tooltip("How often this spawn point will spawn an enemy.")]
    private float spawnInterval;

    private LineRenderer lineRenderer;
    #endregion

    private float timeSinceLastEnemySpawned = 3;

    #region cached references
    [SerializeField]
    private GameObject enemyToSpawn;//Maybe we can make this as a scriptableobject or an array;
    [SerializeField]
    private GameObject enemyDestinations;
    #endregion

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.GameManagerScriptInstance.currentGameState == GameManagerScript.GameState.realTimeStage)
        {
            SpawnEnemyWithInterval();
        }
        UpdateDrawLineColor();
    }

    private void SpawnEnemyWithInterval()
    {
        if (timeSinceLastEnemySpawned >= spawnInterval)
        {
            SpawnEnemy();
        }
        else
        {
            timeSinceLastEnemySpawned += Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPositionVector3 = new Vector3(transform.position.x, 0.2f, transform.position.z);

        if (totalNumberOfEnemiesToSpawn > 0)
        {
            GameObject enemy = GameObject.Instantiate(enemyToSpawn, spawnPositionVector3, Quaternion.identity, transform);
            enemy.GetComponent<EnemyScript>().SetDestinationObject(enemyDestinations);
            totalNumberOfEnemiesToSpawn--;
            timeSinceLastEnemySpawned = 0;
        }
    }

    private void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, enemyDestinations.transform.GetChild(0).position);
    }

    private void UpdateDrawLineColor()
    {
        lineRenderer.material.color = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, 1));
    }
}
