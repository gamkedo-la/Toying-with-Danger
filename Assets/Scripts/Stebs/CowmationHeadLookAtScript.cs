using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowmationHeadLookAtScript : MonoBehaviour
{
    private GameObject lookAtTarget;

    private void Awake()
    {
        lookAtTarget = GameObject.FindGameObjectWithTag("Base");
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(lookAtTarget.transform);
    }
}
