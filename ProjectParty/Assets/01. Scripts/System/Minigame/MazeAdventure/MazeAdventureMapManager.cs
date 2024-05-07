using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MazeAdventureMapManager : MonoBehaviour
{
    public static MazeAdventureMapManager Instance { get; private set; }
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void BakeMap()
    {
        navMeshSurface.BuildNavMesh();
    }
}
