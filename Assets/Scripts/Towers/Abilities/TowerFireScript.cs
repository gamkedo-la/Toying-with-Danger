using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFireScript : TowerAbility
{
    public Transform firePosition;
    public GameObject projectilePrefab;

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
        if (GetClosestTarget() != null)
        {
            Debug.Log("firing at enemy " + GetClosestTarget().name);
        }
    }
}
