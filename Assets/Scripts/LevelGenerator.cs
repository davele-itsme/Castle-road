using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainTypes = new List<GameObject>();
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int maxSpawn;
    [SerializeField] private float spawnDistance;
    [SerializeField] private Transform level;

    private readonly List<GameObject> _terrains = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(0, 1, -3);
    private bool _generateSameTerrain;
    private int _lastTerrainType;
    private int _terrainCounter = 8;
    
    private void Start()
    {
        for (var i = 0; i < maxSpawn; i++)
        {
            GenerateTerrain(true);    
        }
    }

    public void GenerateTerrain(bool start)
    {
        var distance = 0f;
        try
        {
            //If _terrains list is empty, it will throw exception
            distance = _terrains.Last().transform.position.z - playerTransform.position.z;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        if (!(spawnDistance > distance) && !start) return;
        int type;
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
            
        InstantiateTerrain(type);
    }

    private void InstantiateTerrain(int type)
    {
        var newTerrain = Instantiate(terrainTypes[type], _currentPosition, Quaternion.identity);
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
