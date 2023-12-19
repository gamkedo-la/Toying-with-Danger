using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedDestinationScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EventManagerScript.InvokeToyReachedBedEvent(other.gameObject);
        }
    }
}
