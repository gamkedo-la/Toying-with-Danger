using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

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
    private void OnEnable()
    {
        EventManagerScript.GameOverEvent += HandleGameOverEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.GameOverEvent -= HandleGameOverEvent;
    }

    #endregion

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (GameManagerScript.currentGameState == GameManagerScript.GameState.realTimeStage)
        {
            //CheckIfAgentReachedDestination();
            CheckAgentsPathStatus();
        }
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
        var path = new NavMeshPath();
        if (agent.destination != null && agent.hasPath)
        {
            if (agent.CalculatePath(agent.destination, path))
            {
                switch (path.status)
                {
                    case NavMeshPathStatus.PathComplete:
                        //no problem here
                        break;
                    case NavMeshPathStatus.PathPartial:
                        EventManagerScript.InvokeGameOverEvent(GameOverType.NoPathAvailableForEnemy);
                        //this means destination is not possible to reach, we end game here.
                        break;
                    default:
                        //this means destination is not possible to reach, we end game here.
                        EventManagerScript.InvokeGameOverEvent(GameOverType.NoPathAvailableForEnemy);
                        break;
                }
            }
        }
    }

    private void HandleGameOverEvent(GameOverType gameOverType)
    {
        agent.destination = agent.transform.position;
    }
}
