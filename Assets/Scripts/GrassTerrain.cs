using UnityEngine;

public class GrassTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private GameObject grassTerrain;
    private LevelData _levelData;

    private void Start()
    {
        _levelData = LevelData.Instance;
    }

    public void InstantiateTerrain(Vector3 currentPosition)
    {
        var newTerrain = Instantiate(grassTerrain, currentPosition, Quaternion.identity);
        newTerrain.transform.SetParent(transform);
        _levelData.terrains.Add(newTerrain);
    }
}