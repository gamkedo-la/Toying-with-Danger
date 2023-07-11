using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MudScript : MonoBehaviour
{
    public bool highlighted = false;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material unhighlightedMaterial;

    private float positionMovementFloat;

    private Vector3 mOffset;
    private float mZCoord;
    private float mDistanceToScreen;
    public float sensitivityX = 1f;
    public float sensitivityZ = 1f;

    private void Start()
    {
        positionMovementFloat = GameManagerScript.GameManagerScriptInstance.wallPositionMovementFloat;
    }
    private void Update()
    {
        Handle_W_Key();
        Handle_A_Key();
        Handle_S_Key();
        Handle_D_Key();
    }

    private void Handle_W_Key()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (highlighted)
            {
                //PushBackFullyBlockedEnemies();

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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<RedEnemyScript>().muddable)
        {
            other.gameObject.GetComponent<NavMeshAgent>().speed *= 4.0f;
        }
        else if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<RedEnemyScript>().muddable)
        {
            other.gameObject.GetComponent<NavMeshAgent>().speed *= 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<RedEnemyScript>().muddable)
        {
            other.gameObject.GetComponent<NavMeshAgent>().speed *= 0.25f;
        }
        else if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<RedEnemyScript>().muddable)
        {
            other.gameObject.GetComponent<NavMeshAgent>().speed *= 2.0f;
        }
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
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
