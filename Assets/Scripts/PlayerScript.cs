using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region designer properties
    [Tooltip("The speed at which a puzzle piece follows the mouse cursor.")]
    [SerializeField] float followSpeed = 15f;
    #endregion

    #region cached references
    [SerializeField] GameObject defaultPuzzlePiece;
    private Vector3 wallSizeVector3;
    private LayerMask unplacedWallLayerMask; //to be used to ignore the wall piece the player is trying to place
    private bool canPlaceCurrentPieceInCurrentGridCell = false;

    MeshRenderer mouseIndicatorsMeshRenderer;

    [SerializeField] Grid grid;
    #endregion

    private GameObject currentPuzzlePiece;
    private Collider currentPuzzleObjectCollider;
    private NavMeshSurface currentPuzzleObjectNavMeshSurface;
    private Vector3 lastPositionForMouse;
    private float wallZOffset;
    private PuzzlePieceType currentPuzzlePieceType = PuzzlePieceType.Wall;

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
        NavigationBaker.Instance.BuildNavMesh();
        InstantiatePuzzlePiece();
        
        wallZOffset = grid.cellSize.z / 2;
        wallSizeVector3 = defaultPuzzlePiece.GetComponentInChildren<Renderer>().bounds.size;
        int unplacedWallLayer = LayerMask.NameToLayer("UnplacedWall");
        unplacedWallLayerMask = ~(1 << unplacedWallLayerMask);
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
                EventManagerScript.InvokeRealTimePuzzlePiecePlacementEvent();
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            currentPuzzlePieceType = currentPuzzlePieceType == PuzzlePieceType.Wall ? PuzzlePieceType.Tower : PuzzlePieceType.Wall;
            if (currentPuzzlePiece is not null)
            {
                Destroy(currentPuzzlePiece);
                currentPuzzlePiece = null;
            }
            InstantiatePuzzlePiece();
        }
    }

    private bool CanPlaceWall(Vector3 gridCellCenterVector3)
    {
        Vector3 halfExtentsVector3 = wallSizeVector3 * 0.45f;
        Vector3 overlapBoxCenterVector3 = new Vector3(gridCellCenterVector3.x, gridCellCenterVector3.y + (wallSizeVector3.y * 0.5f), gridCellCenterVector3.z);

        Collider[] arrayOfCollidersAboveGridCell = Physics.OverlapBox(overlapBoxCenterVector3, halfExtentsVector3, Quaternion.identity, unplacedWallLayerMask);
        return arrayOfCollidersAboveGridCell.Length == 0;
    }

    private void HandleStartRealTimeStageEvent()
    {
        if (currentPuzzlePiece is not null)
        {
            Destroy(currentPuzzlePiece);
            currentPuzzlePiece = null;
        }
        InstantiatePuzzlePiece();
    }

    private void InstantiatePuzzlePiece()
    {
        Vector3 mousePositionWorldSpace;
        if (GetSelectedMapPosition())
        {
            mousePositionWorldSpace = lastPositionForMouse;
        }
        else
        {
            // Get the mouse position in screen space
            Vector3 mousePositionScreenSpace = Input.mousePosition;

            // Convert the mouse position to world space
            mousePositionWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreenSpace.x, mousePositionScreenSpace.y, 10f));

            
        }

        // Apply clamping to the Y-coordinate
        mousePositionWorldSpace.y = -10f;
        mousePositionWorldSpace.z += wallZOffset;

        //// Get the mouse position in screen space
        //Vector3 mousePositionScreenSpace = Input.mousePosition;

        //// Convert the mouse position to world space
        //Vector3 mousePositionWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreenSpace.x, mousePositionScreenSpace.y, 10f));

        // Instantiate the prefab at the mouse's position and make it the current puzzle piece
        switch (currentPuzzlePieceType)
        {
            case PuzzlePieceType.Wall:
                currentPuzzlePiece = Instantiate(defaultPuzzlePiece, mousePositionWorldSpace, Quaternion.identity);
                break;
            case PuzzlePieceType.Tower:
                currentPuzzlePiece = TowerFactory.CreateRandomTower(transform, mousePositionWorldSpace, Quaternion.identity);
                currentPuzzlePiece.GetComponent<TowerAbility>().enabled = false;
                break;
            default:
                break;
        }
        
        currentPuzzleObjectCollider = currentPuzzlePiece.GetComponent<Collider>();
        currentPuzzleObjectNavMeshSurface = currentPuzzlePiece.GetComponent<NavMeshSurface>();
        if (currentPuzzleObjectCollider.enabled == true)
        {
            currentPuzzleObjectCollider.enabled = false;
            currentPuzzleObjectNavMeshSurface.enabled = false;
        }
    }

    private void MoveCurrentPuzzlePiece()
    {
        Vector3 mousePositionWorldSpace;
        if (GetSelectedMapPosition())
        {
            mousePositionWorldSpace = lastPositionForMouse;
        }
        else
        {
            // Get the mouse position in screen space
            Vector3 mousePositionScreenSpace = Input.mousePosition;
            mousePositionScreenSpace.z = Camera.main.transform.position.y;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePositionScreenSpace);
            // Convert the mouse position to world space
            mousePositionWorldSpace = worldPosition;
        }

        // Apply clamping to the Y-coordinate
        mousePositionWorldSpace.y = 0f;
        mousePositionWorldSpace.z += wallZOffset;


        // Set the puzzle piece's position towards the mouse cursor smoothly
        currentPuzzlePiece.transform.position = mousePositionWorldSpace;

        if (CanPlaceWall(mousePositionWorldSpace))
        {
            canPlaceCurrentPieceInCurrentGridCell = true;
            //print("can place current puzzle piece");
        }
        else
        {
            canPlaceCurrentPieceInCurrentGridCell = false;
            //print("can't place current puzzle piece");
        }
    }

    private bool GetSelectedMapPosition()
    {
        bool isGroundFound = false;

        Vector3 mousePos = Input.mousePosition;
        //Debug.Log("mousePos: " + mousePos);
        mousePos.z = Camera.main.nearClipPlane;
        //Debug.Log("mousePos.z: " + mousePos.z);

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit raycastHit;

        int layerMask = 1 << LayerMask.NameToLayer("IgnoreRaycast");
        layerMask = ~layerMask; // This inverts the layerMask, allowing the raycast to interact with everything EXCEPT the IgnoreRaycast layer.


        //if (Physics.Raycast(raycastHit, out raycastHit, 100/*, LayerMask.NameToLayer("Ground")*/))//
        //{
        //if (raycastHit.transform.tag == "Ground")
        //{
        //    mouseIndicatorsMeshRenderer.material = usableIndicatorMaterial;
        //}
        //else
        //{
        //    mouseIndicatorsMeshRenderer.material = unusableIndicatorMaterial;
        //}

        if (Physics.Raycast(ray, out raycastHit, 100, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
            Vector3Int gridPosition = grid.WorldToCell(raycastHit.point);
            lastPositionForMouse = grid.CellToWorld(gridPosition);
            isGroundFound = true;
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }

        //if (raycastHit.transform.tag == "Ground")
        //{
        //    Vector3Int gridPosition = grid.WorldToCell(raycastHit.point);
        //    lastPositionForMouse = grid.CellToWorld(gridPosition);
        //    isGroundFound = true;
        //}
    //}

        return isGroundFound;
    }

    private void PlaceCurrentPuzzlePiece()
    {
        if (!GetSelectedMapPosition() || !canPlaceCurrentPieceInCurrentGridCell || GameManagerScript.preventBlockPlacement)
        {
            return;
        }


        NavigationBaker.Instance.surfaces.Add(currentPuzzlePiece.GetComponent<NavMeshSurface>());
        NavigationBaker.Instance.BuildNavMesh();
        currentPuzzleObjectCollider.enabled = true;
        currentPuzzleObjectNavMeshSurface.enabled = true;
        currentPuzzlePiece.layer = 8; //change from unplaced layer to placed layer

        if (currentPuzzlePieceType == PuzzlePieceType.Tower)
        {
            currentPuzzlePiece.GetComponent<TowerAbility>().enabled = true;
        }

        currentPuzzlePiece = null;
        currentPuzzleObjectCollider = null;
        currentPuzzleObjectNavMeshSurface = null;


        if (currentPuzzlePieceType == PuzzlePieceType.Wall)
        {
            AudioManagerScript.Instance.PlaySfx("WallPlaced");
        }
        else if (currentPuzzlePieceType == PuzzlePieceType.Tower)
        {
            AudioManagerScript.Instance.PlaySfx("TowerPlaced");
        }


        if (GameManagerScript.currentGameState == GameManagerScript.GameState.preparationStage)
        {
            PlacePreparationStagePuzzlePiece();
        }
        else if (GameManagerScript.currentGameState == GameManagerScript.GameState.realTimeStage)
        {
            PlaceRealTimeStagePuzzlePiece();
        }
    }

    private void PlacePreparationStagePuzzlePiece()
    {
        switch (currentPuzzlePieceType)
        {
            case PuzzlePieceType.Wall:
                GameManagerScript.preparationStageWallsLeft--;
                EventManagerScript.InvokePreparationRemainingWallNumberChangedEvent();
                if (GameManagerScript.preparationStageWallsLeft > 0)
                {
                    InstantiatePuzzlePiece();
                }
                break;
            case PuzzlePieceType.Tower:
                GameManagerScript.preparationStageTowersLeft--;
                EventManagerScript.InvokePreparationRemainingTowerNumberChangedEvent();
                if (GameManagerScript.preparationStageTowersLeft > 0)
                {
                    InstantiatePuzzlePiece();
                }
                break;
            default:
                break;
        }

        if (GameManagerScript.preparationStageWallsLeft == 0 && GameManagerScript.preparationStageTowersLeft == 0)
        {
            EventManagerScript.InvokeOutOfPreparationPiecesEvent();
        }
    }

    private void PlaceRealTimeStagePuzzlePiece()
    {
        switch (currentPuzzlePieceType)
        {
            case PuzzlePieceType.Wall:
                GameManagerScript.realTimeStageWallsLeft--;
                EventManagerScript.InvokeRealTimeRemainingWallNumberChangedEvent();
                if (GameManagerScript.realTimeStageWallsLeft > 0)
                {
                    InstantiatePuzzlePiece();
                }
                break;
            case PuzzlePieceType.Tower:
                GameManagerScript.realTimeStageTowersLeft--;
                EventManagerScript.InvokeRealTimeRemainingTowerNumberChangedEvent();
                if (GameManagerScript.realTimeStageTowersLeft > 0)
                {
                    InstantiatePuzzlePiece();
                }
                break;
            default:
                break;
        }

        if (GameManagerScript.realTimeStageWallsLeft == 0 && GameManagerScript.realTimeStageTowersLeft == 0)
        {
            EventManagerScript.InvokeOutOfRealTimePiecesEvent();
        }
    }

    private void HandlePreparationPuzzlePiecePlacementEvent()
    {
        switch (currentPuzzlePieceType)
        {
            case PuzzlePieceType.Wall:
                if (GameManagerScript.preparationStageWallsLeft > 0)
                {
                    PlaceCurrentPuzzlePiece();
                }
                break;
            case PuzzlePieceType.Tower:
                if (GameManagerScript.preparationStageTowersLeft > 0)
                {
                    PlaceCurrentPuzzlePiece();
                }
                break;
            default:
                break;
        }
        
    }

    private void HandleRealTimePuzzlePiecePlacementEvent()
    {
        switch (currentPuzzlePieceType)
        {
            case PuzzlePieceType.Wall:
                if (GameManagerScript.realTimeStageWallsLeft > 0)
                {
                    PlaceCurrentPuzzlePiece();
                }
                break;
            case PuzzlePieceType.Tower:
                if (GameManagerScript.realTimeStageTowersLeft > 0)
                {
                    PlaceCurrentPuzzlePiece();
                }
                break;
            default:
                break;
        }
    }
}
