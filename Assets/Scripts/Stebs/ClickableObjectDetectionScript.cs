using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjectDetectionScript : MonoBehaviour
{
    private GameObject[] clickableGameObjectsArray;

    private void Awake()
    {
        clickableGameObjectsArray = GameObject.FindGameObjectsWithTag("Clickable");

        for (int i = 0; i < clickableGameObjectsArray.Length; i++)
        {
            Debug.Log("clickableGameObjectsArray: " + clickableGameObjectsArray);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (CheckIfClickWasOnClickableObject(out RaycastHit raycastHit) != null)
            {
                HandleHighlights(raycastHit);
            }
        }
    }

    private GameObject CheckIfClickWasOnClickableObject(out RaycastHit raycastHit)
    {
        GameObject potentiallyClickableObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast (ray.origin, ray.direction * 10, out raycastHit))
        {
            if (raycastHit.transform.gameObject.tag == "Clickable")
            {
                potentiallyClickableObject = raycastHit.transform.gameObject;
            }
        }

        return potentiallyClickableObject;
    }

    private void HandleHighlights(RaycastHit raycastHit)
    {
        for (int i = 0; i < clickableGameObjectsArray.Length; i++)
        {
            clickableGameObjectsArray[i].GetComponent<LongWallObjectScript>().HandleClick(raycastHit);
        }
    }
}
