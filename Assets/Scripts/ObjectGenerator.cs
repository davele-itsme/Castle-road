using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectTypes = new List<GameObject>();
    public void GenerateObjects(GameObject newTerrain, int type)
    {
        var z = newTerrain.transform.position.z;
        if (type == 0)
        {
            var numberOfObjects = Random.Range(3, 8);
            var listOfX = new List<int>();
            do
            {
                var randomObj = Random.Range(0, objectTypes.Count);
                int x;
                do {
                    x = Random.Range(-10, 11);
                } while (listOfX.Contains(x));
                listOfX.Add(x);
                var newObject =  Instantiate(objectTypes[randomObj], new Vector3(x, 1.5f, z), Quaternion.identity);
                newObject.transform.SetParent(newTerrain.transform);
                numberOfObjects--;
            } while (numberOfObjects > 0);
        }
     
    }
}
