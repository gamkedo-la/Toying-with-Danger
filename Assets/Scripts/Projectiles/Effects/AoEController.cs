using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Lifetime))]
public class AoEController : MonoBehaviour
{
    Lifetime lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = GetComponent<Lifetime>();
        lifetime.MaxLifeTimeReachedEvent += DestroySelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthComponent>()?.RemoveHealth(1);
        }
    }
}
