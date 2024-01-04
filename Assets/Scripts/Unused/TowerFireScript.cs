using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFireScript : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectilePrefab;
    public TowerDetectionScript detectionScript;

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
        if (detectionScript.GetClosestTarget() != null)
        {
            Debug.Log("firing at enemy " + detectionScript.GetClosestTarget().name);
        }
        
        //if (detectionScript.enemies.Count > 0)
        //{
        //    detectionScript.enemies.RemoveAt(0);
        //}       
    }
}
