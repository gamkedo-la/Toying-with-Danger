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

    public NavMeshPathStatus currentPathStatus;

    public int destructionPoints = 1;

    #region event subscriptions

    #endregion
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckIfAgentReachedDestination();
        CheckAgentsPathStatus();
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
            //EventManagerScript.InvokeToyReachedBedEvent();
        }
        
    }

    private void SetAgentDestination()
    {
        if (!isEnemyReachedLastDestination)
            agent.SetDestination(nextDestination);
    }

    private void CheckAgentsPathStatus()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            currentPathStatus = NavMeshPathStatus.PathComplete;
        }
        else if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            currentPathStatus = NavMeshPathStatus.PathPartial;
        }
        else if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            currentPathStatus = NavMeshPathStatus.PathInvalid;
        }
    }
}
