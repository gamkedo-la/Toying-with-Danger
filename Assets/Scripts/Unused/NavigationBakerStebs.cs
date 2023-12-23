using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBakerStebs : MonoBehaviour
{
    public static NavigationBakerStebs Instance { get; private set; }

    public List<NavMeshSurface> surfaces;
    public List<NavMeshObstacle> obstacles;

    [SerializeField] GameObject groundObjectParent;

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
            surfaces[i].BuildNavMesh();
        }
    }

}
