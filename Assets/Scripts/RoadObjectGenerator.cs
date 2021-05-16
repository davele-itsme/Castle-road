using UnityEngine;

public class RoadObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject mob;
    private GameObject _newMob;
    
    private void Start()
    {
        var startDelay = Random.Range(0f, 5f);
        var delay = Random.Range(10f, 14f);
        // InvokeRepeating(nameof(GenerateMob), startDelay, delay);
    }
    
    private void GenerateMob()
    {
        var rotation = Quaternion.Euler(0, 87, 0);
        if (transform.position.x == 10)
        {
            rotation = Quaternion.Euler(0, 267, 0);
        }
        _newMob = Instantiate(mob, transform.position, rotation);
        _newMob.transform.SetParent(transform);
    }
}
