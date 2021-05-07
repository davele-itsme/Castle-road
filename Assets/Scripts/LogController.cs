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
        transform.Translate(Vector3.right * (Time.deltaTime * _speed));
        if (transform.position.x >= 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("LOL");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("LOL2");
        }
    }
}
