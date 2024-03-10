using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 5;
    [SerializeField]
    private float rotationMagnitude = 0.2f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Mathf.Sin(Time.time * rotationSpeed) * rotationMagnitude);
    }

}
