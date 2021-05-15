using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip Clip;
    public string Name;

    [Range(0f, 1f)]
    public float Volume;
    [Range(0.1f, 3f)]
    public float Pitch;

    public bool Loop;

    [HideInInspector] public AudioSource Source;
}
