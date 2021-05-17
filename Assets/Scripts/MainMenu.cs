using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Game menu music");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StopSound()
    {
        FindObjectOfType<AudioManager>().Stop("Game menu music");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
}
