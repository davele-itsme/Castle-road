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
}
