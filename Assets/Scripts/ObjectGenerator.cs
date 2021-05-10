using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> grassObjectTypes = new List<GameObject>();
    [SerializeField] private GameObject woodLogGenerator;
    [SerializeField] private GameObject cuboid;
    private  List<int> _grassObjectsXPosition = new List<int>();

    private void Awake()
    {
        TerrainController.NewTerrainCreated += GenerateObjects;
        TerrainController.LastTerrainCreated += GenerateCuboid;
    }
    public void GenerateObjects(GameObject newTerrain)
    {
        switch (newTerrain.tag)
        {
            case "Grass": 
                GenerateGrassObjects(newTerrain);
                break;
            case "Road":
                GenerateRoadObjects(newTerrain);
                break;
            case "River":
                GenerateRiverObjects(newTerrain);
                break;
            default:
                throw new Exception("Object Generator: This type of terrain does not exist.");
        }
    }

    private void GenerateGrassObjects(GameObject newTerrain)
    {
        _grassObjectsXPosition.Clear();
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
    
    private void GenerateRoadObjects(GameObject newTerrain)
    {

    }
    
    private void GenerateRiverObjects(GameObject newTerrain)
    {
        var z = newTerrain.transform.position.z;
        var newWoodGenerator = Instantiate(woodLogGenerator, new Vector3(-10, 1, z), Quaternion.identity);
        newWoodGenerator.transform.SetParent(newTerrain.transform);
    }

    public void GenerateCuboid(GameObject terrain)
    {
        Debug.Log("LOL");
        int x;
        if (terrain.CompareTag("Grass"))
        {
            do {
                x = Random.Range(-6, 7);
            } while (_grassObjectsXPosition.Contains(x));
        }
        else
        {
            x = Random.Range(-6, 7);
        }

        var newCuboid = Instantiate(cuboid, new Vector3(x, 2, terrain.transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0)));
        newCuboid.transform.SetParent(terrain.transform);
    }
}
