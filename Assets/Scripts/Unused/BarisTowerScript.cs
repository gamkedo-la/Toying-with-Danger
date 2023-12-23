using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarisTowerScript : MonoBehaviour
{
    public ChristianCannonRotation cannonRotation;
    public BarisTowerDetectionScript towerRadiusDetection;

    private void Update()
    {
        if (towerRadiusDetection.enemies.Count > 0)
        {
            cannonRotation.SetTarget(towerRadiusDetection.GetClosestTarget());
        }
    }
}
