using UnityEngine;
using Random = UnityEngine.Random;

public class LogController : MonoBehaviour
{
    private float _speed;
    private void Start()
    {
        _speed = Random.Range(1.2f, 1.5f);
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
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform.parent;
            var playerController = other.GetComponent<PlayerController>();
            playerController.IsOnWoodLog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            var playerController = other.GetComponent<PlayerController>();
            playerController.IsOnWoodLog = false;
        }
    }
}
