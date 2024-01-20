using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//force a sphere collider for effects radius
[RequireComponent(typeof(SphereCollider))]
public class TowerAbility : MonoBehaviour
{
    public List<EnemyScript> enemiesInRadius;
    [System.NonSerialized]
    public float DetectionRadius;
    private SphereCollider sphereCollider;


    private void OnEnable()
    {
        EventManagerScript.EnemyGotDestroyedEvent += HandleEnemyGotDestroyedEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.EnemyGotDestroyedEvent -= HandleEnemyGotDestroyedEvent;
    }

    private void OnValidate()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = DetectionRadius;
    }

    private void HandleEnemyGotDestroyedEvent(GameObject enemy)
    {
        enemiesInRadius.Remove(enemy.GetComponent<EnemyScript>());
    }

    public GameObject GetClosestTarget()
    {
        GameObject closestObject = null;
        float distance = float.MaxValue;

        foreach (EnemyScript enemy in enemiesInRadius)
        {
            float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closestObject = enemy.gameObject;
            }
        }

        return closestObject;
    }
    protected virtual bool RemoveEnemy(EnemyScript enemy)
    {
        if (enemiesInRadius.Contains(enemy))
        {
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
    protected virtual bool AddEnemy(EnemyScript enemy)
    {
        //Check if enemy has already been added to the list
        if (!enemiesInRadius.Contains(enemy))
        {
            enemiesInRadius.Add(enemy);
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
