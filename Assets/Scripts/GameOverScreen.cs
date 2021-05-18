using Player;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private Score _score;
    
    private void Start()
    {
        PlayerDeath.PlayerDied += GameOver;
        _score = Score.Instance;
    }

    private void GameOver()
    {
        var bestScore = PlayerPrefs.GetInt("BestScore");
        if (_score.score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", _score.score);
        }
        
        gameOverMenu.SetActive(true);
        bestScoreText.text = "Best score " + PlayerPrefs.GetInt("BestScore");
        pauseButton.SetActive(false);
    }
    
    private void OnDestroy()
    {
        PlayerDeath.PlayerDied -= GameOver;
    }
}
