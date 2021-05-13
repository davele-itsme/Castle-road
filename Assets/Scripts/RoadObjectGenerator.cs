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
        _newMob = Instantiate(mob, transform.position, Quaternion.identity);
        SetMobsSpeed();
        _newMob.transform.SetParent(transform);
    }

    private void SetMobsSpeed()
    {
        var val = Random.Range(0, 2);
        var mobController = _newMob.GetComponent<MobController>();
        var mobControllerRun = val != 0;
        mobController.Run = mobControllerRun;
    }
}
