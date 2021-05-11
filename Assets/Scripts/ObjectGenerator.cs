using UnityEngine;
using Random = UnityEngine.Random;

public class Cuboid : MonoBehaviour
{
    public void GenerateCuboid(GameObject terrain)
    {
        Debug.Log("LOL");
        int x;
        if (terrain.CompareTag("Grass"))
        {
            // do {
            //     x = Random.Range(-6, 7);
            // } while (_grassObjectsXPosition.Contains(x));
        }
        else
        {
            x = Random.Range(-6, 7);
        }

        // var newCuboid = Instantiate(cuboid, new Vector3(x, 2, terrain.transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0)));
        // newCuboid.transform.SetParent(terrain.transform);
    }
}
