using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannon : MonoBehaviour
{
    [SerializeField]
    Timer shotRateTimer;

    [SerializeField]
    Transform cannonFirePosition;

    [SerializeField]
    GameObject projectileToFire;
    // Start is called before the first frame update
    void Start()
    {
        shotRateTimer.maxTimeReached += FireProjectile;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FireProjectile()
    {
        Instantiate(projectileToFire,cannonFirePosition.position,transform.rotation);
        Debug.Log("dfs");
    }
}
