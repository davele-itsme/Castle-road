using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanController : MonoBehaviour
{
    private float _speed;
    private Animator _animator;
    private AudioManager _audioManager;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        _speed = Random.Range(0.6f, 1f);
        _audioManager = FindObjectOfType<AudioManager>();
        _animator = GetComponent<Animator>();
        
        if (transform.position.x > 0 && transform.position.x < 14)
        {
            StartCoroutine(GoLeft());
        }
        else
        {
            StartCoroutine(GoRight());
        }
    }

    private IEnumerator GoRight()
    {
        while (transform.position.x <= 10)
        {
            transform.position += Vector3.right * (Time.deltaTime * _speed);
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator GoLeft()
    {
        while (transform.position.x >= -10)
        {
            transform.position += Vector3.left * (Time.deltaTime * _speed);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger(Attack);
            StartCoroutine(AttackPlayer(0.4f, other.gameObject));
        }
    }
    
    private IEnumerator AttackPlayer(float delayTime, GameObject player)
    {
        yield return new WaitForSeconds(delayTime);
        _audioManager.Play("Punch"); 
        Destroy(player);
    }
}
