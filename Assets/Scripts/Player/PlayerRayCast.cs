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

        private bool _canRayCast = true;
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
            if (_canRayCast)
            {
                var position = transform.position;
                _canRayCast = false;
                var ray = new Ray(new Vector3(position.x, 2, position.z), new Vector3(horValue, 0, 0));
                if (!Physics.Raycast(ray, out _hitInfo, 1f) && OnHorizontalMove != null)
                {
                    OnHorizontalMove(horValue);
                }
                else
                {
                    _canRayCast = true;
                }
            }
        }

        private void VerticalRayCast(float verValue)
        {
            if (_canRayCast)
            {
                var position = transform.position;
                _canRayCast = false;
                var ray = new Ray(new Vector3(position.x, 2, position.z), new Vector3(0, 0, verValue));
                if (!Physics.Raycast(ray, out _hitInfo, 1f) && OnVerticalMove != null)
                {
                    OnVerticalMove(verValue);
                }
                else
                {
                    _canRayCast = true;
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

        public bool ObjectInFront(Vector3 forwardVector)
        {
            if (_canRayCast)
            {
                _canRayCast = false;
                var position = transform.position;
                var ray = new Ray(new Vector3(position.x, 2, position.z), forwardVector);
                if (!Physics.Raycast(ray, out _hitInfo, 2f)) return false;
                _canRayCast = true;
            }
            return true;
        }

        private void RayCastEnabled()
        {
            _canRayCast = true;
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
