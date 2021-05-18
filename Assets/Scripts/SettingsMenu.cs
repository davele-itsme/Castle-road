using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    public void OnEnable()
    {
        var soundVolume = PlayerPrefs.GetFloat("SoundVolume");
        var musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        if (soundVolume != 0)
        {
            SetSound(soundVolume);
        }

        if (musicVolume != 0)
        {
            SetMusic(musicVolume);
        }
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
    
    public void SetMusic(float volume)
    {
        var fixedVolume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MusicVolume", fixedVolume);
        PlayerPrefs.SetFloat("MusicVolume", fixedVolume);
    }

    public void SetSound(float volume)
    {
        var fixedVolume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SoundVolume", fixedVolume);
        PlayerPrefs.SetFloat("SoundVolume", fixedVolume);
    }
}
