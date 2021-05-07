using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LogController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _speed;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("JUMPED");
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("LEFT");
            other.transform.parent = null;
        }
    }
}
