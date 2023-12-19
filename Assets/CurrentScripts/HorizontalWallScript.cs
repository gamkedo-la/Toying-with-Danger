using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HorizontalWallScript : MonoBehaviour
{
    private int hitPoints = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<EnemyScript>().currentPathStatus == NavMeshPathStatus.PathPartial &&
            collision.gameObject.GetComponent<EnemyScript>().destructionPoints > 0)
        {
            hitPoints--;
            collision.gameObject.GetComponent<EnemyScript>().destructionPoints--;

            if (hitPoints < 1)
            {
                EventManagerScript.InvokeToyBlowsUpWallEvent(gameObject);
            }
        }

    }
}
