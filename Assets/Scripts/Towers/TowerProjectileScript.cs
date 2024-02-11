using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    public GameObject target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthComponent>()?.RemoveHealth(1);
            Destroy(gameObject);
        }
    }
}
