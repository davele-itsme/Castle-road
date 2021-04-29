using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    [SerializeField] private int maxSpawn;
    [SerializeField] private Transform level;

    private readonly List<GameObject> _terrains = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(0, 1, 0);
    private bool _generateSameTerrain;
    private int _lastTerrainType = 0;
    private int _terrainCounter = 6;
    
    private void Start()
    {
        for (var i = 0; i < maxSpawn; i++)
        {
            GenerateTerrain();    
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            GenerateTerrain();
        }
    }

    private void GenerateTerrain()
    {
        var type = 0;
        if (_terrainCounter == 0)
        {
            _generateSameTerrain = false;
        }
        else
        {
            _generateSameTerrain = true;
        }
        
        if (_generateSameTerrain)
        {
            type = _lastTerrainType;
        }
        else
        {
            type = GenerateNumberExclude(_lastTerrainType);
            _lastTerrainType = type;
            _terrainCounter = GenerateTerrainGroupNumber();
        }
        
        var newTerrain = Instantiate(terrains[type], _currentPosition, Quaternion.identity);
        _terrains.Add(newTerrain);
        newTerrain.transform.SetParent(level);
        if (_terrains.Count > maxSpawn)
        {
            Destroy(_terrains[0]);
            _terrains.RemoveAt(0);
        }
        _currentPosition.z++;
        _terrainCounter--;
    }

    private static int GenerateTerrainGroupNumber()
    {
        var numbers = new[] { 1, 2, 2, 2, 3, 3, 3, 4, 4, 5};
        var index = Random.Range(0, 10);
        return numbers[index];
    }

    private static int GenerateNumberExclude(int excludedNumber)
    {
        var values = new[] {0, 1, 2};
        values = values.Where(val => val != excludedNumber).ToArray();
        var randomNumber = Random.Range(0, 2);
        return values[randomNumber];
    }
}
