using UnityEngine;

public class RiverTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private GameObject riverTerrain;
    [SerializeField] private GameObject woodLogGenerator;
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
        GenerateRiverObjects(newTerrain);
    }
    
    private void GenerateRiverObjects(GameObject newTerrain)
    {
        var z = newTerrain.transform.position.z;
        var newWoodGenerator = Instantiate(woodLogGenerator, new Vector3(-10, 1, z), Quaternion.identity);
        newWoodGenerator.transform.SetParent(newTerrain.transform);
    }
    
}