using UnityEngine;
using Random = UnityEngine.Random;

public class LogController : MonoBehaviour
{
    private float _speed;
    private Animator _animator;
    private static readonly int OnPlayerInteraction = Animator.StringToHash("OnPlayerInteraction");

    private void Start()
    {
        _speed = Random.Range(1.2f, 1.5f);
        _animator = GetComponent<Animator>();
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
        var playerController = other.GetComponent<PlayerController>();
        playerController.IsOnWoodLog = true;
        _animator.SetTrigger(OnPlayerInteraction);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var playerController = other.GetComponent<PlayerController>();
        playerController.IsOnWoodLog = false;
        playerController.IsStaying();
    }
}
