using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region designer properties
    [Tooltip("The speed at which a puzzle piece follows the mouse cursor.")]
    [SerializeField] float followSpeed = 15f;
    #endregion

    #region cached references
    [SerializeField] GameObject defaultPuzzlePiece;
    #endregion

    private GameObject currentPuzzlePiece;

    #region event subscriptions
    private void OnEnable()
    {
        EventManagerScript.StartRealTimeStageEvent += HandleStartRealTimeStageEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
    }
    #endregion

    private void Update()
    {
        if (currentPuzzlePiece != null)
        {
            MoveCurrentPuzzlePiece();
        }
    }
    private void HandleStartRealTimeStageEvent()
    {
        InstantiatePuzzlePiece();
    }

    private void InstantiatePuzzlePiece()
    {
        // Get the mouse position in screen space
        Vector3 mousePositionScreenSpace = Input.mousePosition;

        // Convert the mouse position to world space
        Vector3 mousePositionWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreenSpace.x, mousePositionScreenSpace.y, 10f));

        // Instantiate the prefab at the mouse's position
        currentPuzzlePiece = Instantiate(defaultPuzzlePiece, mousePositionWorldSpace, Quaternion.identity);
    }

    private void MoveCurrentPuzzlePiece()
    {
        // Get the mouse position in screen space
        Vector3 mousePositionScreenSpace = Input.mousePosition;

        // Convert the mouse position to world space
        Vector3 mousePositionWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreenSpace.x, mousePositionScreenSpace.y, 10f));

        // Apply clamping to the Y-coordinate
        mousePositionWorldSpace.y = 0.5f;

        // Set the puzzle piece's position towards the mouse cursor smoothly
        currentPuzzlePiece.transform.position = Vector3.Lerp(currentPuzzlePiece.transform.position, mousePositionWorldSpace, Time.deltaTime * followSpeed);
    }
}
