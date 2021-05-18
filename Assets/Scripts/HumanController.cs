using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanController : MonoBehaviour
{
    private float _speed;

    private void Start()
    {
        _speed = Random.Range(0.6f, 1f);

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
            Destroy(other.gameObject);
        }
    }
}
