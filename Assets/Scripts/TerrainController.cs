using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Game manager
public class TerrainController : MonoBehaviour
{
    public delegate void NewTerrain(GameObject terrain);
    public static event NewTerrain NewTerrainCreated;
    
    public delegate void LastTerrain(GameObject terrain);
    public static event LastTerrain LastTerrainCreated;

    [SerializeField] private List<GameObject> terrainTypes = new List<GameObject>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxSpawn;
    [SerializeField] private float spawnDistance;
    [SerializeField] private Transform level;
    
    private Vector3 _currentPosition;
    private readonly List<GameObject> _terrains = new List<GameObject>();
    private int _lastTerrainType;
    private int _terrainCounter = 8;

    private void Start()
    {
        _currentPosition = new Vector3(0, 1, -7);
        PlayerMovement.OnForward += GenerateTerrain;
        for (var i = 0; i < maxSpawn; i++)
        {
            GenerateTerrain();    
        }
    }

    public void GenerateTerrain()
    {
        if (CheckDistanceToLastTerrain())
        {
            var type = GenerateTerrainType();
            InstantiateTerrain(type);
            DestroyTerrain();
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
            var type = NumberGenerator.GenerateNumberWithExclude(terrainTypes, _lastTerrainType);
            _lastTerrainType = type;
            var numbers = new[] { 1, 2, 2, 2, 3, 3, 3, 4, 4, 5};
            _terrainCounter = NumberGenerator.GenerateTerrainAmountWithProbability(numbers);
            return type;
        }
        return _lastTerrainType;
    }

    private void InstantiateTerrain(int type)
    {
        var newTerrain = Instantiate(terrainTypes[type], _currentPosition, Quaternion.identity);
        _terrains.Add(newTerrain);
        newTerrain.transform.SetParent(level);
        _currentPosition.z++;
        if (NewTerrainCreated != null)
        {
            NewTerrainCreated(newTerrain);
        }
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
