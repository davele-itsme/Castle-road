using System;
using Player;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioManager _audioManager;
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        PlayerGravity.OnFall += PlayWaterSplash;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void PlayWaterSplash(Vector3 position)
    {
        _audioManager.Play("Fall in water");
        transform.position = position;
        _particleSystem.Play();
    }

    private void OnDestroy()
    {
        PlayerGravity.OnFall -= PlayWaterSplash;
    }
}
