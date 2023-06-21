using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    private List<GameObject> listOfMagnetizedEnemies;

    private void Start()
    {
        listOfMagnetizedEnemies = new List<GameObject>();
    }

    private void Update()
    {
        if (listOfMagnetizedEnemies.Count != 0)
        {
            HandleListOfMagnetizedEnemies();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            listOfMagnetizedEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < listOfMagnetizedEnemies.Count; i++)
            {
                if (other.gameObject == listOfMagnetizedEnemies[i])
                {
                    Rigidbody enemyRigidbody = listOfMagnetizedEnemies[i].transform.GetComponent<Rigidbody>();
                    enemyRigidbody.velocity = Vector3.zero;
                    enemyRigidbody.angularVelocity = Vector3.zero;
                    listOfMagnetizedEnemies.Remove(listOfMagnetizedEnemies[i]);      
                }
            }
        }
    }

    private void HandleListOfMagnetizedEnemies()
    {
        for (int i = 0; i < listOfMagnetizedEnemies.Count; i++)
        {
            Vector3 direction = gameObject.transform.position - listOfMagnetizedEnemies[i].gameObject.transform.position;
            direction.Normalize();

            Rigidbody enemyRigidbody = listOfMagnetizedEnemies[i].gameObject.transform.GetComponent<Rigidbody>();
            enemyRigidbody.AddForce(direction * 0.5f);
        }
    }
}
