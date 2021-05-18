
using System.Collections;
using Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 _shouldFollow;
    private bool _follow = true;

    private void Start()
    {
        StartCoroutine(FollowPlayer());
        PlayerDeath.PlayerDied += StopFollowing;
    }

    private void StopFollowing()
    {
        _follow = false;
    }

    private IEnumerator FollowPlayer()
    {
        do
        {
            _shouldFollow = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime);
            transform.position = _shouldFollow;
            yield return null;
        } while (_follow);
    }
}
