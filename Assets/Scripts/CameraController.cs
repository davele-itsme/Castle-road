using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 _shouldFollow;

    private void Update()
    {
        _shouldFollow = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime);
        transform.position = _shouldFollow;
    }
}
