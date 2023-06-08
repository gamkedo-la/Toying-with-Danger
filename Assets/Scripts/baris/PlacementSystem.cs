using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private Grid grid;

    private Vector3 lastPosition;


    void Update()
    {
        Vector3 mousePosition = GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }

    private Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100, LayerMask.NameToLayer("default")))
        {
            Vector3Int gridPosition = grid.WorldToCell(raycastHit.point);
            lastPosition = grid.CellToWorld(gridPosition);
        }

        return lastPosition;
    }
}
