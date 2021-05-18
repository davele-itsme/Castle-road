using UnityEngine;

namespace Player
{
    public class PlayerRayCast : MonoBehaviour
    {
        public delegate void HorizontalAction(float value);
        public static event HorizontalAction OnHorizontalMove;

        public delegate void VerticalAction(float value);
        public static event VerticalAction OnVerticalMove;
    
        public delegate void ObjectBelow(RaycastHit hitInfo);
        public static event ObjectBelow OnObjectBelowFound;

        private bool _rayCastDone;
        private RaycastHit _hitInfo;
        private void Start()
        {
            PlayerInput.HorizontalInput += HorizontalRayCast;
            PlayerInput.VerticalInput += VerticalRayCast;
            PlayerMovement.OnStopMovement += RayCastDown;
            PlayerMovement.OnStopMovement += RayCastEnabled;
            LogController.OnExit += RayCastDown;
        }

        private void HorizontalRayCast(float horValue)
        {
            if (!_rayCastDone)
            {
                var position = transform.position;
                _rayCastDone = true;
                var ray = new Ray(new Vector3(position.x, 2, position.z), new Vector3(horValue, 0, 0));
                if (!Physics.Raycast(ray, out _hitInfo, 1f) && OnHorizontalMove != null)
                {
                    OnHorizontalMove(horValue);
                }
                else
                {
                    _rayCastDone = false;
                }
            }
        }

        private void VerticalRayCast(float verValue)
        {
            if (!_rayCastDone)
            {
                var position = transform.position;
                _rayCastDone = true;
                var ray = new Ray(new Vector3(position.x, 2, position.z), new Vector3(0, 0, verValue));
                if (!Physics.Raycast(ray, out _hitInfo, 1f) && OnVerticalMove != null)
                {
                    OnVerticalMove(verValue);
                }
                else
                {
                    _rayCastDone = false;
                }
            }
        }

        private void RayCastDown()
        {
            var ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out _hitInfo, 1f) && OnObjectBelowFound != null)
            {
                OnObjectBelowFound(_hitInfo);
            }
        }

        private void RayCastEnabled()
        {
            _rayCastDone = false;
        }

        private void OnDestroy()
        {
            PlayerInput.HorizontalInput -= HorizontalRayCast;
            PlayerInput.VerticalInput -= VerticalRayCast;
            PlayerMovement.OnStopMovement -= RayCastDown;
            PlayerMovement.OnStopMovement -= RayCastEnabled;
            LogController.OnExit -= RayCastDown;
        }
    }
}
