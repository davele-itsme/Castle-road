using UnityEngine;

public class WoodLogGenerator : MonoBehaviour
{
    [SerializeField] private GameObject woodLog;
    private GameObject _newWoodLog;
    
    private void Start()
    {
        var startDelay = Random.Range(0f, 2f);
        var delay = Random.Range(3f, 7f);
        InvokeRepeating(nameof(GenerateWoodLog), startDelay, delay);
    }
    
    private void GenerateWoodLog()
    {
        _newWoodLog = Instantiate(woodLog, transform.position, Quaternion.identity);
        _newWoodLog.transform.SetParent(transform);
    }
}
