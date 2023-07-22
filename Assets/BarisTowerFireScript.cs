using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarisTowerFireScript : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectilePrefab;
    public BarisTowerDetectionScript detectionScript;

    public float rate;
    public float delta;
    // Update is called once per frame
    void Update()
    {
        if (delta < rate)
        {
            delta += Time.deltaTime;
        }
        else
        {
            delta = 0;
            FireProjectile();
        }
    }
    public void FireProjectile()
    {
        if (detectionScript.enemies.Count > 0)
        {
            detectionScript.enemies[0].KillEnemy();
            detectionScript.enemies.RemoveAt(0);
        }       
    }
}
