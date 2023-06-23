using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianProjectile : MonoBehaviour
{
    public float force;
    public Rigidbody rigidbody;

    float timeAlive;
    public float maxLifetime;

    public void FireProjectile(Vector3 position, Vector3 eulerAngles)
    {
        transform.position = position;
        transform.eulerAngles= eulerAngles;
        rigidbody.AddForce(force * transform.forward, ForceMode.Impulse);
    }

    private void Update()
    {
        if(timeAlive >= maxLifetime) {
            Destroy(gameObject);
        }

        timeAlive += Time.deltaTime;
    }
}
