using UnityEngine;

public class RoadObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject mob;
    private GameObject _newMob;
    
    private void Start()
    {
        var startDelay = Random.Range(0f, 2f);
        var delay = Random.Range(3f, 7f);
        InvokeRepeating(nameof(GenerateMob), startDelay, delay);
    }
    
    private void GenerateMob()
    {
        var rotation = Quaternion.Euler(0, 90, 0);
        if (transform.position.x == 10)
        {
            rotation = Quaternion.Euler(0, 270, 0);
        }
        _newMob = Instantiate(mob, transform.position, rotation);
        _newMob.transform.SetParent(transform);
    }
}
