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
    #endregion

    private float timeSinceLastEnemySpawned = 3;

    #region cached references
    [SerializeField]
    private GameObject enemyToSpawn;//Maybe we can make this as a scriptableobject or an array;
    [SerializeField]
    private GameObject enemyDestinations;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.currentGameState == GameManagerScript.GameState.realTimeStage)
        {
            SpawnEnemyWithInterval();
        }       
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
        if (totalNumberOfEnemiesToSpawn > 0)
        {
            GameObject enemy = GameObject.Instantiate(enemyToSpawn, transform.position, Quaternion.identity, transform);
            enemy.GetComponent<EnemyScript>().SetDestinationObject(enemyDestinations);
            totalNumberOfEnemiesToSpawn--;
            timeSinceLastEnemySpawned = 0;
        }
    }
}
