using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBulletTower : TowerAbility
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
            GameObject target = GetClosestTarget();
            if (target != null)
            {
                delta = 0;
                FireProjectile(target);
            }
        }
    }
    public void FireProjectile(GameObject target)
    {
        Debug.Log("firing at enemy " + target.name);
        TowerProjectileScript projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<TowerProjectileScript>();
        projectile.target = target;
    }
}
