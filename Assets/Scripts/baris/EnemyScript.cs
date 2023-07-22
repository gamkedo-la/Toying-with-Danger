using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private Queue<Transform> enemyDestinations = new Queue<Transform>();
    private Vector3 nextDestination;
    [SerializeField]
    private int PlayerHealth = 3;

    [SerializeField]
    private float destinationReachedTreshold = 1.0f;

    private bool isEnemyReachedLastDestination = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckIfAgentReachedDestination();
    }

    private void CheckIfAgentReachedDestination()
    {

        if (agent.remainingDistance < destinationReachedTreshold)
        {
            SetNextDestination();
            SetAgentDestination();
        }
    }

    public void SetDestinationObject(GameObject destination)
    {
        for (int i = 0; i < destination.transform.childCount; i++)
        {
            enemyDestinations.Enqueue(destination.transform.GetChild(i));
        }
        SetNextDestination();
        SetAgentDestination();
    }

    private void SetNextDestination()
    {
        if (enemyDestinations.Count > 0)
        {
            nextDestination = enemyDestinations.Dequeue().position;
        }
        else
        {
            Debug.Log("An enemy has reached to its destination");
            isEnemyReachedLastDestination = true;
            GameObject.Destroy(gameObject);
        }
        
    }

    private void SetAgentDestination()
    {
        if (!isEnemyReachedLastDestination)
            agent.SetDestination(nextDestination);
    }

    public void KillEnemy()
    {
        GameObject.Destroy(gameObject);
    }
}
