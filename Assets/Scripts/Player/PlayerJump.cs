using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private Transform playerModelTransform;
        [SerializeField] private int timer;
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
                var forwardVector = playerModelTransform.TransformDirection (Vector3.back * 2f);
                if (!_playerRayCast.ObjectInFront(forwardVector))
                {
                    JumpForward(forwardVector);
                }
                StartCoroutine(Timer());
                _canJump = false;
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(timer);
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