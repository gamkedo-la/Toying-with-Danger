using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManagerScript : MonoBehaviour
{
    public static DebugManagerScript Instance { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            NavigationBaker.Instance.BuildNavMesh();
        }
    }
}
