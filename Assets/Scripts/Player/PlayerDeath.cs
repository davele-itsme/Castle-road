using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject pauseButton;
        private void OnDestroy()
        {
            gameOverMenu.SetActive(true);
            pauseButton.SetActive(false);
        }
    }
}