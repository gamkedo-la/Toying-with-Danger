using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianTower : MonoBehaviour
{
    public ChristianCannonRotation cannonRotation;
    public ChristianTowerRadiusDetection towerRadiusDetection;

    private void Update()
    {
        if(towerRadiusDetection.enemies.Count> 0)
        {
            cannonRotation.SetTarget(towerRadiusDetection.GetClosestTarget());
        }
    }
}
