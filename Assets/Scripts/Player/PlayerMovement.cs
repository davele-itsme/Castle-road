using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public delegate void StayAction();
        public static event StayAction OnStay;
        public delegate void ForwardAction(bool start);
        public static event ForwardAction OnForward;
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
                StartCoroutine(MovePlayer());
                if (verValue == 1f && OnForward != null)
                {
                    OnForward(false);
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
            if (OnStay != null)
            {
                OnStay();
            }
        }

        private void OnDestroy()
        {
            PlayerRayCast.OnHorizontalMove += HorizontallyMove;
            PlayerRayCast.OnVerticalMove += VerticallyMove;
        }
    }
}
