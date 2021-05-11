using System.Linq;
using Player;
using UnityEngine;

//Game manager
public class TerrainController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxSpawn;
    [SerializeField] private float spawnDistance;
    
    private LevelData _levelData;
    private Vector3 _currentPosition;
    private int _lastTerrainType;
    private int _terrainCounter = 8;
    private ITerrain[] _terrainTypes;

    private void Start()
    {
        _levelData = LevelData.Instance;
        _currentPosition = new Vector3(0, 1, -7);
        _terrainTypes = gameObject.GetComponents<ITerrain>();
        PlayerMovement.OnForward += ControlTerrain;
        do
        {
            ControlTerrain();
        } while (CheckDistanceToLastTerrain());
    }

    private void ControlTerrain()
    {
        if (CheckDistanceToLastTerrain())
        {
            GenerateTerrainType();
            for (var i = _terrainCounter; i > 0; i--)
            {
                InstantiateTerrain();
                _currentPosition.z++;
                _terrainCounter--;
            }
        }
    
        if (CheckDistanceToBeginning())
        {
            DestroyTerrain();
        }
    }
    
    private bool CheckDistanceToLastTerrain()
    {
        if (_levelData.terrains.Count == 0) return true;
        var distance = _levelData.terrains.Last().transform.position.z - playerTransform.position.z;
        return spawnDistance > distance;
    }
    
    private bool CheckDistanceToBeginning()
    {
        if (_levelData.terrains.Count < maxSpawn) return false;
        var distance = playerTransform.position.z - _levelData.terrains[0].transform.position.z;
        return maxSpawn - spawnDistance < distance;
    }
    
    private void GenerateTerrainType()
    {
        _lastTerrainType = NumberGenerator.GenerateNumberWithExclude(_terrainTypes.Length, _lastTerrainType);
        var numbers = new[] { 1, 2, 2, 2, 3, 3, 3, 4, 4, 5}; 
        _terrainCounter = NumberGenerator.GenerateTerrainAmountWithProbability(numbers);
    }
    
    private void InstantiateTerrain()
    {
        _terrainTypes[_lastTerrainType].InstantiateTerrain(_currentPosition);
    }
    
    private void DestroyTerrain()
    {
        if (_levelData.terrains.Count > maxSpawn)
        {
            Destroy(_levelData.terrains[0]);
            _levelData.terrains.RemoveAt(0);
        }
    }
}
