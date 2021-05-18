using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public delegate void PlDeath();
        public static event PlDeath PlayerDied;
        
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject pauseButton;

        private void OnDestroy()
        {
            gameOverMenu.SetActive(true);
            pauseButton.SetActive(false);
            if (PlayerDied != null)
            {
                PlayerDied();
            }
        }
    }
}