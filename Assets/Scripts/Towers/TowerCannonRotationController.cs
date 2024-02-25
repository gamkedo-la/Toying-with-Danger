using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script should only handle aiming of the cannon at a target
/// </summary>
public class TowerCannonRotationController : MonoBehaviour
{
    public TowerAbility towerAbility;

    public bool aimsAtTarget;

    GameObject targetToAimAt;

    //every frame the rotation should update to look at a target if specified
    //too and there is a target to aim at
    private void Update()
    {
        if(aimsAtTarget)
            targetToAimAt = towerAbility.GetClosestTarget();
        else
            targetToAimAt = null;
        
        if (targetToAimAt != null)        
            transform.LookAt(targetToAimAt.transform);
        
    }

    //logic for setting the towers target
    public void SetAimTarget(GameObject targetToAimAt)
    {
        this.targetToAimAt = targetToAimAt;
    }
}
