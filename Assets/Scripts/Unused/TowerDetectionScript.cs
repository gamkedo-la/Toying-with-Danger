using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetectionScript : MonoBehaviour
{

    public float DetectionRadius;
    public SphereCollider sphereCollider;
    public List<GameObject> enemies;

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
        sphereCollider.radius = DetectionRadius;
    }


    private void HandleEnemyGotDestroyedEvent(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public GameObject GetClosestTarget()
    {
        GameObject closestObject = null;
        float distance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < distance)
            {
                closestObject = enemy;
            }
        }

        return closestObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            enemies.Remove(other.gameObject);
        }
    }
}