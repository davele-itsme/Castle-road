using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController2 : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
