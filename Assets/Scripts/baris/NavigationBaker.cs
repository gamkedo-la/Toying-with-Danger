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
            print("surfaces[i]: " + surfaces.Count);
            surfaces[i].BuildNavMesh();
        }
    }

    public void HandleWallDestructionEvent(GameObject wallToBeDestroyed)
    {
        print("calling handleWallDestructionEvent");
        NavMeshSurface surfacesToRemove = Instance.surfaces.Find(x => x == wallToBeDestroyed.GetComponent<NavMeshSurface>());
        wallToBeDestroyed.GetComponent<NavMeshSurface>().enabled = false;
        surfaces.Remove(surfacesToRemove);
        Destroy(wallToBeDestroyed);
        BuildNavMesh();


        // TODO: fix the walls disappering bug
        //Baris, trying out different solutions to the problem.
        //print("calling handleWallDestructionEvent");
        //NavMeshSurface surfacesToRemove = Instance.surfaces.Find(x => x == wallToBeDestroyed.GetComponent<NavMeshSurface>());
        //wallToBeDestroyed.GetComponent<NavMeshSurface>().enabled = false;
        //wallToBeDestroyed.GetComponent<NavMeshSurface>().RemoveData();
        //surfaces.Remove(surfacesToRemove);
        //surfacesToRemove.enabled = false;
        //surfacesToRemove.RemoveData();
        //surfacesToRemove.ignoreNavMeshAgent = true;
        //UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
        
        //Destroy(wallToBeDestroyed);
        //NavMesh.RemoveAllNavMeshData();

        //RebuildNavMeshAfterDelay();
    }
    //private IEnumerator RebuildNavMeshAfterDelay()
    //{
    //    yield return new WaitForSeconds(1f); // Wait for 0.1 seconds

    //    // Rebuild the NavMesh with the remaining surfaces
    //    RebuildNavMeshAfterDelay();
    //    BuildNavMesh();
    //}

}