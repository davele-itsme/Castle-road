using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainTypes = new List<GameObject>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxSpawn;
    [SerializeField] private float spawnDistance;
    [SerializeField] private Transform level;
    
    private Vector3 _currentPosition;
    private readonly List<GameObject> _terrains = new List<GameObject>();
    private ObjectGenerator _objectGenerator;
    private int _lastTerrainType;
    private int _terrainCounter = 8;
    
    private void Start()
    {
        _currentPosition = new Vector3(0, 1, -7);
        PlayerMovement.OnForward += GenerateTerrain;
        _objectGenerator = GetComponent<ObjectGenerator>();
        for (var i = 0; i < maxSpawn; i++)
        {
            GenerateTerrain();    
        }
        
        _objectGenerator.GenerateCuboid(_terrains.Last());
    }

    public void GenerateTerrain()
    {
        if (CheckDistanceToLastTerrain())
        {
            var type = GenerateTerrainType();
            InstantiateTerrain(type);
        }
    }

    private bool CheckDistanceToLastTerrain()
    {
        if (_terrains.Count == 0) return true;
        var distance = _terrains.Last().transform.position.z - playerTransform.position.z;
        return spawnDistance > distance;
    }

    private int GenerateTerrainType()
    {
        if (_terrainCounter == 0)
        {
            var type = NumberGenerator.GenerateNumberWithExclude(_lastTerrainType);
            _lastTerrainType = type;
            _terrainCounter = NumberGenerator.GenerateTerrainGroupNumber();
            return type;
        }
        return _lastTerrainType;
    }

    private void InstantiateTerrain(int type)
    {
        var newTerrain = Instantiate(terrainTypes[type], _currentPosition, Quaternion.identity);
        _terrains.Add(newTerrain);
        newTerrain.transform.SetParent(level);
        DestroyTerrain();
        _currentPosition.z++;
        _objectGenerator.GenerateObjects(newTerrain, type);
    }

    private void DestroyTerrain()
    {
        if (_terrains.Count > maxSpawn)
        {
            Destroy(_terrains[0]);
            _terrains.RemoveAt(0);
        }
        _terrainCounter--;
    }
}
