using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerSlowAbility : TowerAbility
{
    public float slowAmount;
    /// <summary>
    /// This block will handle all the logic for removing the enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    protected override bool RemoveEnemy(EnemyScript enemy)
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
    protected override bool AddEnemy(EnemyScript enemy)
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
}
