using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;

public class BoximonController : MonoBehaviour
{
    public delegate void FollowPlayer(string action);
    public static event FollowPlayer OnFollowPlayer;
    
    private GameObject _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _followPlayer;
    private bool _madeSound;
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement.OnStopMovement += CheckForDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _followPlayer = true;
            _animator.SetBool(Walk, true);
            if (OnFollowPlayer != null && !_madeSound)
            {
                OnFollowPlayer("Boximon");
                _madeSound = true;
            }
            StartCoroutine(FollorPlayer());
        }
    }

    private IEnumerator FollorPlayer()
    {
        do
        {
            _agent.SetDestination(_player.transform.position);
            CheckForDistance();
            yield return null;
        } while (_followPlayer);
    }
    
    private void CheckForDistance()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 1f)
        {
            _followPlayer = false;
            _animator.SetTrigger(Attack);
            StartCoroutine(AttackPlayer(0.2f));
        }
    }

    private IEnumerator AttackPlayer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(_player);
    }
}
