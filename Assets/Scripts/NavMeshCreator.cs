using Level;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCreator : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;
    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
        TerrainController.OnCreated += UpdateNavMesh;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }

    private void OnDestroy()
    {
        TerrainController.OnCreated -= UpdateNavMesh;
    }
}
