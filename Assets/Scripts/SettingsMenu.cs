using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
    
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSound(float volume)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
    }

 
}
