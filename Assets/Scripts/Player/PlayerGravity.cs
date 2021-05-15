using UnityEngine;

namespace Player
{
    public class PlayerGravity : MonoBehaviour
    {
        public delegate void FallAction(string sound);
        public static event FallAction OnFall;
        
        private PlayerMovement _playerMovement;
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            PlayerRayCast.OnObjectBelowFound += CheckForWater;
        }

        private void CheckForWater(RaycastHit hitInfo)
        {
            if (hitInfo.collider.gameObject.CompareTag("River") && !_playerMovement.IsOnWoodLog && !_playerMovement.isMoving)
            {
                FallDownToWater();
            }
        }

        private void FallDownToWater()
        {
            if (OnFall != null)
            {
                OnFall("Fall in water");
            }
            transform.position += Vector3.down;
            Destroy(gameObject);
      
        }
    }
}
