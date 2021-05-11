using UnityEngine;

public class RoadTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private GameObject roadTerrain;
    private LevelData _levelData;
    
    private void Awake()
    {
        _levelData = LevelData.Instance;
    }
    
    public void InstantiateTerrain(Vector3 currentPosition)
    {
        var newTerrain = Instantiate(roadTerrain, currentPosition, Quaternion.identity);
        newTerrain.transform.SetParent(transform);
        _levelData.terrains.Add(newTerrain);
    }
}