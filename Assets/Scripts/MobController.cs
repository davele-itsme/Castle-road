using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public bool Run { get; set; } 
    private void Start()
    {
        _speed = Random.Range(1.2f, 1.5f);
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
