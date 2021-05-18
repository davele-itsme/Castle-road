using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixerSound;
    public AudioMixerGroup audioMixerMusic;
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] greetings;
    public static AudioManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        MapSounds(sounds);
        MapSounds(greetings);
        var music = Array.Find(sounds, sound => sound.Name.Equals("Game menu music"));
        music.Source.outputAudioMixerGroup = audioMixerMusic;
    }

    private void MapSounds(IEnumerable<Sound> groupOfSounds)
    {
        foreach (var sound in groupOfSounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
            sound.Source.outputAudioMixerGroup = audioMixerSound;
        }
    }

    public void Play(string nameOfSound)
    {
        var s = Array.Find(sounds, sound => sound.Name.Equals(nameOfSound));
        if (s == null) return;
        s.Source.Play();
    }

    public void Stop(string nameOfSound)
    {
        var s = Array.Find(sounds, sound => sound.Name.Equals(nameOfSound));
        if (s == null) return;
        s.Source.Stop();
    }

    public void PlayRandomGreeting()
    {
        RandomGenerator<Sound>.RandomPicker(greetings).Source.Play();
    }
}
