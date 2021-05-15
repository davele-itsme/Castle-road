using System.Collections;
using UnityEngine;

public class MobController : MonoBehaviour
{
    private float _speed;

    private void Start()
    {
        _speed = Random.Range(1f, 1.2f);

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
        do
        {
            transform.position += Vector3.right * (Time.deltaTime * _speed);
            yield return null;
        } while (transform.position.x <= 10);
        Destroy(gameObject);
    }

    private IEnumerator GoLeft()
    {
        do
        {
            transform.position += Vector3.left * (Time.deltaTime * _speed);
            yield return null;
        } while (transform.position.x >= -10);
        Destroy(gameObject);
    }
    
    
}
