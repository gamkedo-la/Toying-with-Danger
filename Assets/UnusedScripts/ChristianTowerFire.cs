using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianTowerFire : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectilePrefab;

    public float rate;
    public float delta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delta<rate)
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
        ChristianProjectile projectile = Instantiate(projectilePrefab).GetComponent<ChristianProjectile>();
        projectile.FireProjectile(firePosition.position, firePosition.eulerAngles);
    }
}
