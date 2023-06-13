using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianTowerRadiusDetection : MonoBehaviour
{ 

    public float DetectionRadius;
    public SphereCollider sphereCollider;
    public List<ChristianEnemy> enemies;

    private void OnValidate()
    {
        sphereCollider.radius= DetectionRadius;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetClosestTarget()
    {
        GameObject closestObject = null;
        float distance = float.MaxValue;

        foreach(ChristianEnemy enemy in enemies)
        {
            if(Vector3.Distance(transform.position, enemy.transform.position) < distance)
            {
                closestObject = enemy.gameObject;
            }
        }

        return(closestObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ChristianEnemy>() !=null)
        {
            enemies.Add(other.gameObject.GetComponent<ChristianEnemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ChristianEnemy>() != null)
        {
            enemies.Remove(other.gameObject.GetComponent<ChristianEnemy>());
        }
    }
}
