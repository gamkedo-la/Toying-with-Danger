using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedEnemyScript : MonoBehaviour
{
    private GameObject myNavMeshTargetObject;
    private NavMeshAgent myNavMeshAgent;

    private float floatDistanceForSuccessfulInvasion;

    public bool fullyBlocked = false;

    private void Awake()
    {
        myNavMeshTargetObject = GameObject.FindGameObjectWithTag("Base");
        myNavMeshAgent = gameObject.transform.GetComponent<NavMeshAgent>();

        myNavMeshAgent.SetDestination(myNavMeshTargetObject.transform.position);

        floatDistanceForSuccessfulInvasion = GameManagerScript.GameManagerScriptInstance.floatDistanceForTriggeringBaseInvasion;
    }

    private void Update()
    {
        if (CheckForSuccessfulInvasion() == true)
        {
            Debug.Log("invaded");
            Destroy(gameObject);
        }

        CheckForOutOfBoundsAndDestroyIfSo();
    }

    private void CheckForOutOfBoundsAndDestroyIfSo()
    {
        bool isOnNavMesh = NavMesh.SamplePosition(myNavMeshAgent.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas);

        if (!isOnNavMesh)
        {
            Destroy(myNavMeshAgent.gameObject);
        }
    }    

    private bool CheckForSuccessfulInvasion()
    {
        bool hasBeenInvaded = false;

        if (CalculateDistanceFromTarget() < floatDistanceForSuccessfulInvasion)
        {
            hasBeenInvaded = true;
        }

        return hasBeenInvaded;
    }

    private float CalculateDistanceFromTarget()
    {
        return Vector3.Distance(gameObject.transform.position, myNavMeshTargetObject.transform.position);
    }
}
