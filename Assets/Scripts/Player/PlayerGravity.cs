using System;
using UnityEngine;

namespace Player
{
    public class PlayerGravity : MonoBehaviour
    {
        public delegate void FallAction(Vector3 position);
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
                OnFall(transform.position);
            }
            transform.position += Vector3.down;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            PlayerRayCast.OnObjectBelowFound -= CheckForWater;
        }
    }
}
