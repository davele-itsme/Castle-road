using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;

public class BoximonController : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _followPlayer;
    private bool _madeSound;
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private AudioManager _audioManager;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioManager = FindObjectOfType<AudioManager>();
        _audioManager.Play("Boximon sleep");
        PlayerMovement.OnStopMovement += CheckForDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _followPlayer = true;
            _animator.SetBool(Walk, true);
            if (!_madeSound)
            {
                _audioManager.Play("Boximon grunt");
                _audioManager.Stop("Boximon sleep");
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
            yield return new WaitForSeconds(0.5f);
        } while (_followPlayer);
    }
    
    private void CheckForDistance()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 1f)
            {
                _followPlayer = false;
                _animator.SetTrigger(Attack);
                _audioManager.Play("Boximon punch");
                StartCoroutine(AttackPlayer(0.2f));
            }
    }

    private IEnumerator AttackPlayer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(_player);
    }

    private void OnDestroy()
    {
        _audioManager.Stop("Boximon sleep");
        PlayerMovement.OnStopMovement -= CheckForDistance;
    }
}
