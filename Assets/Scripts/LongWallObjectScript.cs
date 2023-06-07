using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWallObjectScript : MonoBehaviour
{
    public bool highlighted = false;
    [SerializeField] Material highlightMaterial;

    private float positionMovementFloat;

    private GameObject[] enemies;

    private void Start()
    {
        positionMovementFloat = GameManagerScript.GameManagerScriptInstance.wallPositionMovementFloat;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void Update()
    {
        Handle_W_Key();
        Handle_A_Key();
        Handle_S_Key();
        Handle_D_Key();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.transform.GetComponent<RedEnemyScript>().fullyBlocked = true;
        }    
    }

    private List<GameObject> CheckForFullyBlockedEnemies()
    {
        List<GameObject> fullyBlockedEnemies = new List<GameObject>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.transform.GetComponent<RedEnemyScript>().fullyBlocked == true)
            {
                fullyBlockedEnemies.Add(enemies[i]);
            }    
        }

        return fullyBlockedEnemies;
    }   
    
    private void PushBackFullyBlockedEnemies()
    {
        List<GameObject> fullyBlockedEnemies = CheckForFullyBlockedEnemies();

        for (int i = 0; i < fullyBlockedEnemies.Count; i++)
        {
            Debug.Log(fullyBlockedEnemies[i]);
        }    

        if (fullyBlockedEnemies.Count != 0)
        {
            for (int i = 0; i < fullyBlockedEnemies.Count; i++)
            {
                fullyBlockedEnemies[i].GetComponent<Rigidbody>().AddForce(Vector3.back * 10);
                fullyBlockedEnemies[i].GetComponent<RedEnemyScript>().fullyBlocked = false;

                Vector3 currentPosition = fullyBlockedEnemies[i].transform.position;
                Vector3 newPosition;

                float currentZ_Position_Float = currentPosition.z;
                float newZ_Position_Float = currentZ_Position_Float += 5.0f;

                newPosition.x = currentPosition.x;
                newPosition.y = currentPosition.y;
                newPosition.z = newZ_Position_Float;

                fullyBlockedEnemies[i].transform.position = newPosition;
            }
        }     
    }    

    private void Handle_W_Key()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (highlighted)
            {
                PushBackFullyBlockedEnemies();

                MoveWallForward();
            }              
        }    
    }

    private void MoveWallForward()
    {
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 newPosition;

        float currentZ_Position_Float = currentPosition.z;
        float newZ_Position_Float = currentZ_Position_Float += positionMovementFloat;

        newPosition.x = currentPosition.x;
        newPosition.y = currentPosition.y;
        newPosition.z = newZ_Position_Float;

        gameObject.transform.position = newPosition;
    }    

    private void Handle_A_Key()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (highlighted)
            {
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 newPosition;

                float currentX_Position_Float = currentPosition.x;
                float newX_Position_Float = currentX_Position_Float -= positionMovementFloat;

                newPosition.x = newX_Position_Float;
                newPosition.y = currentPosition.y;
                newPosition.z = currentPosition.z;

                gameObject.transform.position = newPosition;
            }
        }
    }

    private void Handle_S_Key()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (highlighted)
            {
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 newPosition;

                float currentZ_Position_Float = currentPosition.z;
                float newZ_Position_Float = currentZ_Position_Float -= positionMovementFloat;

                newPosition.x = currentPosition.x;
                newPosition.y = currentPosition.y;
                newPosition.z = newZ_Position_Float;

                gameObject.transform.position = newPosition;
            }
        }
    }

    private void Handle_D_Key()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (highlighted)
            {
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 newPosition;

                float currentX_Position_Float = currentPosition.x;
                float newX_Position_Float = currentX_Position_Float += positionMovementFloat;

                newPosition.x = newX_Position_Float;
                newPosition.y = currentPosition.y;
                newPosition.z = currentPosition.z;

                gameObject.transform.position = newPosition;
            }
        }
    }
    public void HandleClick()
    {
        gameObject.transform.GetComponent<MeshRenderer>().material = highlightMaterial;
        highlighted = true;
    }    
}
