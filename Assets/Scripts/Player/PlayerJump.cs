using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        public delegate void JumpAction(float cooldown);
        public static event JumpAction OnJumped;
        
        [SerializeField] private Transform playerModelTransform;
        [SerializeField] private int jumpCooldown;
        [SerializeField] private float movingTime;
        
        private bool _canJump = true;
        private PlayerRayCast _playerRayCast;
        private PlayerMovement _playerMovement;

        private void Start()
        {
            PlayerInput.JumpInputReceived += JumpControl;
            _playerRayCast = GetComponent<PlayerRayCast>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void JumpControl()
        {
            if (_canJump)
            {
                if (OnJumped != null)
                {
                    OnJumped(jumpCooldown);
                }
                _canJump = false;
                var forwardVector = playerModelTransform.TransformDirection (Vector3.back * 2f);
                if (!_playerRayCast.ObjectInFront(forwardVector))
                {
                    JumpForward(forwardVector);
                }
                StartCoroutine(Timer());
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(jumpCooldown);
            _canJump = true;
        }

        private void JumpForward(Vector3 forwardVector)
        {
            _playerMovement.JumpForward(forwardVector, movingTime);
        }

        private void OnDestroy()
        {
            PlayerInput.JumpInputReceived -= JumpControl;
        }
    }
}