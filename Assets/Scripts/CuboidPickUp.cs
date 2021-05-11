using UnityEngine;

public class CuboidPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        var levelGeneration = GameObject.FindWithTag("LevelGeneration");
        // var objectGenerator = levelGeneration.GetComponent<>();
        // objectGenerator.GenerateCuboid();
    }
}
