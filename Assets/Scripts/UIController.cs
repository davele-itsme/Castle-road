using Player;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject scoreText;
    
    private void Start()
    {
        PlayerDeath.PlayerDied += GameOver;
    }

    private void GameOver()
    {
        gameOverMenu.SetActive(true);
        scoreText.SetActive(false);
        pauseButton.SetActive(false);
    }
    
    private void OnDestroy()
    {
        PlayerDeath.PlayerDied -= GameOver;
    }
}
