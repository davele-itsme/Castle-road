using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public delegate void StopAction();
        public static event StopAction OnStopMovement;
        public delegate void ForwardAction();
        public static event ForwardAction OnForward;
        public delegate void MoveAction(string sound);
        public static event MoveAction OnMove;
        public bool IsOnWoodLog { get; set; }
        public bool isMoving;
    
        [SerializeField] private float movingTime, timeToMove;

        private Vector3 _startPos, _targetPos;
        private Animator _anim;
        private static readonly int Move = Animator.StringToHash("Move");

        private void Start()
        {
            PlayerRayCast.OnHorizontalMove += HorizontallyMove;
            PlayerRayCast.OnVerticalMove += VerticallyMove;
            _anim = GetComponent<Animator>();
        }

        private void VerticallyMove(float verValue)
        {
            if (!isMoving)
            {
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(0, 0, verValue);
                isMoving = true; 
                _anim.SetTrigger(Move);
                if (OnMove != null)
                {
                    OnMove("Move");
                }
                StartCoroutine(MovePlayer());
                if (verValue == 1f && OnForward != null)
                {
                    OnForward();
                }
            }
        }

        private void HorizontallyMove(float horValue)
        {
            if (!isMoving)
            {
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(horValue, 0, 0);
                isMoving = true;
                _anim.SetTrigger(Move);
                if (OnMove != null)
                {
                    OnMove("Move");
                }
                StartCoroutine(MovePlayer());
            }
        }
    
        private IEnumerator MovePlayer()
        {
            var tInterpolate = 0f;
            do
            {
                tInterpolate += Time.deltaTime / movingTime;
                transform.position = Vector3.Lerp(_startPos, _targetPos, tInterpolate);
                yield return null;
            } while (Vector3.Distance(_startPos, transform.position) < 1f);
            transform.position = _targetPos;
            yield return new WaitForSeconds(timeToMove);
            isMoving = false;
            if (OnStopMovement != null)
            {
                //needs to be called after isMoving otherwise won't fall to water
                OnStopMovement();
            }
        }

        private void OnDestroy()
        {
            PlayerRayCast.OnHorizontalMove -= HorizontallyMove;
            PlayerRayCast.OnVerticalMove -= VerticallyMove;
        }
    }
}
