using UnityEngine;

public class RiverTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private GameObject riverTerrain;
    private LevelData _levelData;
    
    private void Start()
    {
        _levelData = LevelData.Instance;
    }
    
    public void InstantiateTerrain(Vector3 currentPosition)
    {
        var newTerrain = Instantiate(riverTerrain, currentPosition, Quaternion.identity);
        newTerrain.transform.SetParent(transform);
        _levelData.terrains.Add(newTerrain);
    }
}