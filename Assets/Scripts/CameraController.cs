using System.Collections;
using Player;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 _shouldFollow;
    private IEnumerator _followCoroutine;

    private void Start()
    {
        _followCoroutine = FollowPlayer();
        StartCoroutine(_followCoroutine);
        PlayerDeath.PlayerDied += StopFollowing;
    }

    private void StopFollowing()
    {
        StopCoroutine(_followCoroutine);
    }

    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            _shouldFollow = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime);
            transform.position = _shouldFollow;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        PlayerDeath.PlayerDied -= StopFollowing;
    }
}
