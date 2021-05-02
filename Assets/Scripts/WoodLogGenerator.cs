using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogGenerator : MonoBehaviour
{
    [SerializeField] private GameObject woodLog;
    private void Start()
    {
        var startDelay = Random.Range(0f, 2f);
        var delay = Random.Range(3f, 7f);
        InvokeRepeating("GenerateWoodLog", startDelay, delay);
    }
    
    private void GenerateWoodLog()
    {
        var newWoodLog = Instantiate(woodLog, new Vector3(-10, 1, transform.position.z), Quaternion.identity);
        newWoodLog.transform.SetParent(transform);
    }
}
