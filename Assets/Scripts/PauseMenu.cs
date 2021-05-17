using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;
    
    private void Awake()
    {
        PlayerInput.CancelInput += SwitchPause;
    }

    public void SwitchPause()
    {
        if (GameIsPaused)
        {
            OnResume();
        }
        else
        {
            OnPause();
        }
    }

    public void OnResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.SetActive(true);
    }

    public void OnPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseButton.SetActive(false);
    }

    public void OnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1); 
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
}
