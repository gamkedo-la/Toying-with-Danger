using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    private List<GameObject> listOfMagnetizedEnemies;

    private GameObject magnetArm1Object;
    private GameObject magnetArm2Object;
    private GameObject magnetBackObject;

    [SerializeField] Material magnetArmHighlightMaterial;
    [SerializeField] Material magnetArmUnhighlightedMaterial;

    [SerializeField] Material magnetBackHighlightMaterial;
    [SerializeField] Material magnetBackUnhighlightedMaterial;


    public bool highlighted = false;

    private float positionMovementFloat;

    private Vector3 mOffset;
    private float mZCoord;
    private float mDistanceToScreen;
    public float sensitivityX = 1f;
    public float sensitivityZ = 1f;

    private float magnetizationSensitivity = 0;


    private void Start()
    {
        positionMovementFloat = GameManagerScript.GameManagerScriptInstance.wallPositionMovementFloat;


        magnetArm1Object = gameObject.transform.Find("MagnetArm1Object").gameObject;
        magnetArm2Object = gameObject.transform.Find("MagnetArm2Object").gameObject;
        magnetBackObject = gameObject.transform.Find("MagnetBackObject").gameObject;

        listOfMagnetizedEnemies = new List<GameObject>();
    }

    private void Update()
    {
        if (listOfMagnetizedEnemies.Count != 0)
        {
            Debug.Log("should be magnetizing");
            HandleListOfMagnetizedEnemies();
        }

        Handle_W_Key();
        Handle_A_Key();
        Handle_S_Key();
        Handle_D_Key();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("magnet trigger triggered");
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
            Debug.Log("direction: " + direction);

            Rigidbody enemyRigidbody = listOfMagnetizedEnemies[i].gameObject.transform.GetComponent<Rigidbody>();
            Debug.Log("enemyRigidboy: " + enemyRigidbody);
            enemyRigidbody.AddForce(direction * magnetizationSensitivity);
        }
    }

    public void HandleClick(RaycastHit raycastHit)
    {
        //if (!highlighted && raycastHit.transform.gameObject == gameObject)
        //{
        //    magnetArm1Object.transform.GetComponent<MeshRenderer>().material = magnetArmHighlightMaterial;
        //    magnetArm2Object.transform.GetComponent<MeshRenderer>().material = magnetArmHighlightMaterial;
        //    magnetBackObject.transform.GetComponent<MeshRenderer>().material = magnetBackHighlightMaterial;

        //    highlighted = true;
        //}
        //else
        //{
        //    magnetArm1Object.transform.GetComponent<MeshRenderer>().material = magnetArmUnhighlightedMaterial;
        //    magnetArm2Object.transform.GetComponent<MeshRenderer>().material = magnetArmUnhighlightedMaterial;
        //    magnetBackObject.transform.GetComponent<MeshRenderer>().material = magnetBackUnhighlightedMaterial;

        //    highlighted = false;
        //}

    }

    private void Handle_W_Key()
    {
        if (Input.GetKeyUp(KeyCode.W))
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

                //navigationBakerStebsScript.BuildNavMesh();
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

                //navigationBakerStebsScript.BuildNavMesh();
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

                //navigationBakerStebsScript.BuildNavMesh();
            }
        }
    }

    void OnMouseDown()
    {
        mDistanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        magnetizationSensitivity = 10f;
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

    private void OnMouseUp()
    {
        magnetizationSensitivity = 0f;
    }
}
