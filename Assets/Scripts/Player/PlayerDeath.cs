using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public delegate void PlDeath();
        public static event PlDeath PlayerDied;
        
        private void OnDestroy()
        {
            if (PlayerDied != null)
            {
                PlayerDied();
            }
        }
    }
}