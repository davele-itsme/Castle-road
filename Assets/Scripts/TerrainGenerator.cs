using System;
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
    
    private readonly List<GameObject> _terrains = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(0, 1, -7);
    private ObjectGenerator _objectGenerator;
    private bool _generateSameTerrain;
    private int _lastTerrainType;
    private int _terrainCounter = 8;
    
    private void Start()
    {
        PlayerMovement.OnForward += GenerateTerrain;
        _objectGenerator = GetComponent<ObjectGenerator>();
        for (var i = 0; i < maxSpawn; i++)
        {
            GenerateTerrain(true);    
        }
        
        _objectGenerator.GenerateCuboid(_terrains.Last());
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
            type = NumberGenerator.GenerateNumberWithExclude(_lastTerrainType);
            _lastTerrainType = type;
            _terrainCounter = NumberGenerator.GenerateTerrainGroupNumber();
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
        _objectGenerator.GenerateObjects(newTerrain, type);
    }
}
