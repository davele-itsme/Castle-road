using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector]
    public int score;
    
    private TextMeshProUGUI _scoreText;
    public static Score Instance;

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
        
        CuboidPickUp.CuboidPickedUp += UpdateScore;
        _scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        score = 0;
   
    }
    
    private void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        CuboidPickUp.CuboidPickedUp -= UpdateScore;
    }
}
