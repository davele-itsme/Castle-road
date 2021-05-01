using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float tValue;
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, tValue);
    }
}
