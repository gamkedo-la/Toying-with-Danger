using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private Queue<Transform> enemyDestinations = new Queue<Transform>();
    private Vector3 nextDestination;

    [SerializeField]
    private float destinationReachedTreshold = 1.0f;
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
            Debug.Log("Enemy penetrated to the castle!");
        }
        
    }

    private void SetAgentDestination()
    {
        agent.SetDestination(nextDestination);
    }

    public void KillEnemy()
    {
        GameObject.Destroy(gameObject);
    }
}
