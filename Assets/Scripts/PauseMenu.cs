using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject scoreText;
    private AudioManager _audioManager;
    
    private void Awake()
    {
        PlayerInput.CancelInput += SwitchPause;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void SwitchPause()
    {
        PlaySound();
        if (gameIsPaused)
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
        gameIsPaused = false;
        pauseButton.SetActive(true);
        scoreText.SetActive(true);
    }

    public void OnPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseButton.SetActive(false);
        scoreText.SetActive(false);
    }

    public void OnHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1); 
    }
    
    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void PlaySound()
    {
        _audioManager.Play("Select");
    }

    public void Accept()
    {
        if (gameIsPaused)
        {
            pauseMenuUI.SetActive(true);
        }
        else
        {
            gameOverMenu.SetActive(true);
        }
    }
}
