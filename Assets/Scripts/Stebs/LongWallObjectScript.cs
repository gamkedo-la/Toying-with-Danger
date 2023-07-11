using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWallObjectScript : MonoBehaviour
{
    public bool highlighted = false;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material unhighlightedMaterial;

    private float positionMovementFloat;

    private GameObject[] enemies;

    private Vector3 mOffset;
    private float mZCoord;
    private float mDistanceToScreen;
    public float sensitivityX = 1f;
    public float sensitivityZ = 1f;

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

            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Destroy(enemyRigidbody);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.AddComponent<Rigidbody>();
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

        for (int i = 0; i < fullyBlockedEnemies.Count - 1; i++)
        {
            Debug.Log("fullyBlockedEnemies[i].name: " + fullyBlockedEnemies[i].name);
        }

        return fullyBlockedEnemies;
    }   
    
    private void PushBackFullyBlockedEnemies()
    {
        List<GameObject> fullyBlockedEnemies = CheckForFullyBlockedEnemies();

        for (int i = 0; i < fullyBlockedEnemies.Count - 1; i++)
        {
            Debug.Log("fullyBlockedEnemies[i].name: " + fullyBlockedEnemies[i].name);
        }
        if (fullyBlockedEnemies.Count != 0)
        {
            for (int i = 0; i < fullyBlockedEnemies.Count; i++)
            {
                fullyBlockedEnemies[i].transform.Translate(0,0, -7.5f);
                fullyBlockedEnemies[i].GetComponent<RedEnemyScript>().fullyBlocked = false;
            }
        }     
    }    

    private void Handle_W_Key()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (highlighted)
            {
                //PushBackFullyBlockedEnemies();

                MoveWallForward();
                //navigationBakerStebsScript.BuildNavMesh();
            }              
        }    
    }

    private void MoveWallForward()
    {
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 newPosition;

        float currentZ_Position_Float = currentPosition.z;
        float newZ_Position_Float = (currentZ_Position_Float += positionMovementFloat);

        newPosition.x = currentPosition.x;
        newPosition.y = currentPosition.y;
        newPosition.z = newZ_Position_Float;

        gameObject.transform.position = newPosition;
    }    

    private void Handle_A_Key()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Debug.Log("a key up");
            if (highlighted)
            {
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 newPosition;

                float currentX_Position_Float = currentPosition.x;
                float newX_Position_Float = (currentX_Position_Float -= positionMovementFloat);

                newPosition.x = newX_Position_Float;
                newPosition.y = currentPosition.y;
                newPosition.z = currentPosition.z;

                gameObject.transform.position = newPosition;

                //navigationBakerStebsScript.BuildNavMesh();
            }
        }
    }

    private void Handle_S_Key()
    {
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (highlighted)
            {
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 newPosition;

                float currentZ_Position_Float = currentPosition.z;
                float newZ_Position_Float = (currentZ_Position_Float -= positionMovementFloat);

                newPosition.x = currentPosition.x;
                newPosition.y = currentPosition.y;
                newPosition.z = newZ_Position_Float;

                gameObject.transform.position = newPosition;

                //navigationBakerStebsScript.BuildNavMesh();
            }
        }
    }

    private void Handle_D_Key()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("d key up");
            if (highlighted)
            {
                Vector3 currentPosition = gameObject.transform.position;
                Vector3 newPosition;

                float currentX_Position_Float = currentPosition.x;
                float newX_Position_Float = (currentX_Position_Float += positionMovementFloat);

                newPosition.x = newX_Position_Float;
                newPosition.y = currentPosition.y;
                newPosition.z = currentPosition.z;

                gameObject.transform.position = newPosition;

                //navigationBakerStebsScript.BuildNavMesh();
            }
        }
    }
    public void HandleClick(RaycastHit raycastHit)
    {
        if (!highlighted && raycastHit.transform.gameObject == gameObject)
        {
            gameObject.transform.GetComponent<MeshRenderer>().material = highlightMaterial;
            highlighted = true;
        }
        else
        {
            gameObject.transform.GetComponent<MeshRenderer>().material = unhighlightedMaterial;
            highlighted = false;
        }
        
    }

    void OnMouseDown()
    {
        mDistanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mDistanceToScreen;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPos() + mOffset;
        newPosition.y = transform.position.y; // Preserve the original Y position
        newPosition.x = transform.position.x + ((newPosition.x - transform.position.x) * sensitivityX); // X Sensitivity
        newPosition.z = transform.position.z + ((newPosition.z - transform.position.z) * sensitivityZ); // Z Sensitivity
        transform.position = newPosition;
    }
}
