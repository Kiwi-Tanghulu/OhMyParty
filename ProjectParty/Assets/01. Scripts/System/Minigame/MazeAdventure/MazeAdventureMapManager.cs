using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MazeAdventureMapManager : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void BakeMap()
    {
        navMeshSurface.BuildNavMesh();
    }
}
