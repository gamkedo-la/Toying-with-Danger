using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;

    public void SetDestination(Vector3 destination)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination;
    }
}
