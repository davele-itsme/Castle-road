using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> grassObjectTypes = new List<GameObject>();
    [SerializeField] private GameObject woodLogGenerator;
    
    public void GenerateObjects(GameObject newTerrain, int type)
    {
        switch (type)
        {
            case (0): 
                GenerateGrassObjects(newTerrain);
                break;
            case (1):
                GenerateRoadObjects(newTerrain);
                break;
            case (2):
                GenerateRiverObjects(newTerrain);
                break;
            default:
                throw new Exception("Object Generator: This type of terrain does not exist.");
        }
    }

    private void GenerateGrassObjects(GameObject newTerrain)
    {
        var z = newTerrain.transform.position.z;
        var numberOfObjects = Random.Range(3, 7);
        var listOfX = new List<int>();
        do
        {
            var randomObj = Random.Range(0, grassObjectTypes.Count);
            int x;
            do {
                x = Random.Range(-7, 8);
            } while (listOfX.Contains(x));
            listOfX.Add(x);
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
}
