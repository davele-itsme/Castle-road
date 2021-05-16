using Player;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        PlayerGravity.OnFall += PlayWaterSplash;
    }

    private void PlayWaterSplash(string action, Vector3 position)
    {
        transform.position = position;
        _particleSystem.Play();
    }
}
