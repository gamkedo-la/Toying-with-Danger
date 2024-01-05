using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//force a sphere collider for effects radius
[RequireComponent(typeof(SphereCollider))]
public class TowerSlowAbility : MonoBehaviour
{
    public List<EnemyScript> enemiesInRadius;
    public float slowAmount;
    SphereCollider effectRadius;

    private void Awake()
    {
        //localize sphere collider
        effectRadius = GetComponent<SphereCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// This block will handle all the logic for removing the enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    bool RemoveEnemy(EnemyScript enemy)
    {
        if (enemiesInRadius.Contains(enemy))
        {
            enemy.GetComponent<NavMeshAgent>().speed += slowAmount;
            enemiesInRadius.Remove(enemy);
        }
        else
        {
            Debug.LogError("Could not find enemy in list");
            return false;
        }

        return true;
    }

    /// <summary>
    /// This block handles the logic of adding enemies
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    bool AddEnemy(EnemyScript enemy)
    {
        //Check if enemy has already been added to the list
        if (!enemiesInRadius.Contains(enemy))
        {
            enemiesInRadius.Add(enemy);
            enemy.GetComponent<NavMeshAgent>().speed -= slowAmount;
        }
        else
        {
            Debug.LogWarning("Enemy Already added to list");
            return false;
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            //localize enemy in radius
            var enemyScript = other.gameObject.GetComponent<EnemyScript>();
            
            //check if enemy was succesfully added to enemy list
            if (AddEnemy(enemyScript))
            {

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            //localize enemy in radius
            var enemyScript = other.gameObject.GetComponent<EnemyScript>();

            //check if enemy was succesfully removed to enemy list
            if (RemoveEnemy(enemyScript))
            {

            }
        }
    }
}
