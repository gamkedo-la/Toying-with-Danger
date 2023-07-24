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
        EventManagerScript.PreparationPuzzlePiecePlacementEvent += HandlePreparationPuzzlePiecePlacementEvent;
        EventManagerScript.RealTimePuzzlePiecePlacementEvent += HandleRealTimePuzzlePiecePlacementEvent;
    }

    private void OnDisable()
    {
        EventManagerScript.StartRealTimeStageEvent -= HandleStartRealTimeStageEvent;
        EventManagerScript.PreparationPuzzlePiecePlacementEvent -= HandlePreparationPuzzlePiecePlacementEvent;
        EventManagerScript.RealTimePuzzlePiecePlacementEvent -= HandleRealTimePuzzlePiecePlacementEvent;
    }
    #endregion

    private void Start()
    {
        InstantiatePuzzlePiece();
    }
    private void Update()
    {
        if (currentPuzzlePiece != null)
        {
            MoveCurrentPuzzlePiece();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GameManagerScript.currentGameState == GameManagerScript.GameState.preparationStage)
            {
               EventManagerScript.InvokePreparationPuzzlePiecePlacementEvent();
            }  
            else if (GameManagerScript.currentGameState == GameManagerScript.GameState.realTimeStage)
            {
                print("mouseUp recognizing realTimeStage gameState");
                EventManagerScript.InvokeRealTimePuzzlePiecePlacementEvent();
            }
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

        // Instantiate the prefab at the mouse's position and make it the current puzzle piece
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

    private void PlaceCurrentPuzzlePiece()
    {
        currentPuzzlePiece = null;

        if (GameManagerScript.currentGameState == GameManagerScript.GameState.preparationStage)
        {
            PlacePreparationStagePuzzlePiece();
        }
        else if (GameManagerScript.currentGameState == GameManagerScript.GameState.realTimeStage)
        {
            print("inside placeCurrentPuzzlePiece");
            PlaceRealTimeStagePuzzlePiece();
        }
    }

    private void PlacePreparationStagePuzzlePiece()
    {
        GameManagerScript.preparationStagePuzzlePiecesLeft--;

        if (GameManagerScript.preparationStagePuzzlePiecesLeft == 0)
        {
            EventManagerScript.InvokeOutOfPreparationPiecesEvent();
        }

        if (GameManagerScript.preparationStagePuzzlePiecesLeft > 0)
        {
            InstantiatePuzzlePiece();
        }
    }

    private void PlaceRealTimeStagePuzzlePiece()
    {
        GameManagerScript.realTimeStagePuzzlePiecesLeft--;
        if (GameManagerScript.realTimeStagePuzzlePiecesLeft == 0)
        {
            EventManagerScript.InvokeOutOfRealTimePiecesEvent();
        }

        if (GameManagerScript.realTimeStagePuzzlePiecesLeft > 0)
        {
            print("should be placing real time piece");
            InstantiatePuzzlePiece();
        }
    }

    private void HandlePreparationPuzzlePiecePlacementEvent()
    {
        if (GameManagerScript.preparationStagePuzzlePiecesLeft > 0)
        {
            PlaceCurrentPuzzlePiece();
        } 
    }

    private void HandleRealTimePuzzlePiecePlacementEvent()
    {
        if (GameManagerScript.realTimeStagePuzzlePiecesLeft > 0)
        {
            PlaceCurrentPuzzlePiece();
        }
    }
}
