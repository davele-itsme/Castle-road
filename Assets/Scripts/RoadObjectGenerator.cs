using UnityEngine;

public class RoadObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] humans;

    private GameObject _newMob;
    private Quaternion rotation;
    
    private void Start()
    {
        var startDelay = Random.Range(0f, 5f);
        var delay = Random.Range(12f, 16f);
        
        rotation = Quaternion.Euler(0, 90, 0);
        if (transform.position.x == 10)
        {
            rotation = Quaternion.Euler(0, -90, 0);
        }
        InvokeRepeating(nameof(GenerateMob), startDelay, delay);
    }
    
    private void GenerateMob()
    {
        var randomHuman= RandomGenerator<GameObject>.RandomPicker(humans);
        _newMob = Instantiate(randomHuman, transform.position, rotation);
        _newMob.transform.SetParent(transform);
    }
}
