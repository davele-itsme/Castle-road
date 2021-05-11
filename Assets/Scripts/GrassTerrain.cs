using System.Collections.Generic;
using UnityEngine;

public class GrassTerrain : MonoBehaviour, ITerrain
{
    [SerializeField] private GameObject grassTerrain;
    [SerializeField] private List<GameObject> grassObjectTypes;

    private List<int> _grassObjectsXPosition;
    private LevelData _levelData;

    private void Start()
    {
        _levelData = LevelData.Instance;
        grassObjectTypes = new List<GameObject>();
        _grassObjectsXPosition = new List<int>();
    }

    public void InstantiateTerrain(Vector3 currentPosition)
    {
        var newTerrain = Instantiate(grassTerrain, currentPosition, Quaternion.identity);
        newTerrain.transform.SetParent(transform);
        _levelData.terrains.Add(newTerrain);
        GenerateGrassObjects(newTerrain);
    }
    
    private void GenerateGrassObjects(GameObject newTerrain)
    {
        var z = newTerrain.transform.position.z;
        var numberOfObjects = Random.Range(2, 6);
        do
        {
            var randomObj = Random.Range(0, grassObjectTypes.Count);
            int x;
            do {
                x = Random.Range(-6, 7);
            } while (_grassObjectsXPosition.Contains(x));
            _grassObjectsXPosition.Add(x);
            var newObject =  Instantiate(grassObjectTypes[randomObj], new Vector3(x, 1.5f, z), Quaternion.identity);
            newObject.transform.SetParent(newTerrain.transform);
            numberOfObjects--;
        } while (numberOfObjects > 0);
    }
}