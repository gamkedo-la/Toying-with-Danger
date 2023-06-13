using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristianCannonRotation : MonoBehaviour
{
    public GameObject cannonObject;

    public GameObject target;
    private void Update()
    {
        AimCannon();
    }
    public void AimCannon()
    {
        if(target != null)
            cannonObject.transform.LookAt(target.transform.position);
        else
            cannonObject.transform.rotation= Quaternion.identity;
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

}
