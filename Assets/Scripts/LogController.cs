using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class LogController : MonoBehaviour
{
    public delegate void ExitWoodLog();
    public static event ExitWoodLog OnExit;
    
    private float _speed;
    private Animator _animator;
    private static readonly int OnPlayerInteraction = Animator.StringToHash("OnPlayerInteraction");
    private AudioManager _audioManager;

    private void Start()
    {
        _speed = Random.Range(1.2f, 1.5f);
        _animator = GetComponent<Animator>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        transform.parent.Translate(Vector3.right * (Time.deltaTime * _speed));
        if (transform.position.x >= 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.IsOnWoodLog = true;
        _animator.SetTrigger(OnPlayerInteraction);
        _audioManager.Play("Wood");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var playerMovement = other.GetComponent<PlayerMovement>();
        playerMovement.IsOnWoodLog = false;
        if (OnExit != null)
        {
            OnExit();
        }
    }
}
