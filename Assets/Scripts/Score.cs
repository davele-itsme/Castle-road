using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private int _score;
    private void Start()
    {
        CuboidPickUp.CuboidPickedUp += UpdateScore;
        _scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        _score = 0;
    }

    private void UpdateScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    private void OnDestroy()
    {
        CuboidPickUp.CuboidPickedUp -= UpdateScore;
    }
}
