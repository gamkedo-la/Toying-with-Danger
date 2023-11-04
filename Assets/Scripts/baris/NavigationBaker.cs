using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public static NavigationBaker Instance { get; private set; }

    public List<NavMeshSurface> surfaces;
    public List<NavMeshObstacle> obstacles;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    public void BuildNavMesh()
    {

        for (int i = 0; i < surfaces.Count; i++)
        {
            print("surfaces[i]: " + surfaces[i]);
            surfaces[i].BuildNavMesh();
        }
    }

    public void HandleWallDestructionEvent(GameObject wallToBeDestroyed)
    {
        print("calling handleWallDestructionEvent");
        Destroy(wallToBeDestroyed);
        BuildNavMesh();
    }

}