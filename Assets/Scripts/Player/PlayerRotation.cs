using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        private Vector3 _directionVector;
        private void Start()
        {
            PlayerInput.HorizontalInput += RotateHorizontally;
            PlayerInput.VerticalInput += RotateVertically;
        }

        private void RotateHorizontally(float value)
        {
            if (value == 1f)
            {
                _directionVector.y = 270;
            }
            else
            {
                _directionVector.y = 90;
            }
        
            RotatePlayer();
        }

        private void RotateVertically(float value)
        {
            if (value == 1f)
            {
                _directionVector.y = 180;
            }
            else
            {
                _directionVector.y = 0;
            }

            RotatePlayer();
        }
        private void RotatePlayer()
        {
            playerTransform.rotation = Quaternion.Euler(_directionVector);
        }

        private void OnDestroy()
        {
            PlayerInput.HorizontalInput -= RotateHorizontally;
            PlayerInput.VerticalInput -= RotateVertically;
        }
    }
}
