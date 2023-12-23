using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChristianEnemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent.destination=ChristianManager.instance.mainBase.transform.position;
    }

    private void Update()
    {
        //navMeshAgent.des
    }
}
