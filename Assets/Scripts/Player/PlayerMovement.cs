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
        public delegate void BackwardAction();
        public static event BackwardAction OnBackward;
        
        public bool IsOnWoodLog { get; set; }
        public bool isMoving;
        public bool canMoveBackward;
    
        [SerializeField] private float movingTime, timeToMove;

        private Vector3 _startPos, _targetPos;
        private Animator _anim;
        private AudioManager _audioManager;
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Start()
        {
            PlayerRayCast.OnHorizontalMove += HorizontallyMove;
            PlayerRayCast.OnVerticalMove += VerticallyMove;
            _anim = GetComponent<Animator>();
            _audioManager = FindObjectOfType<AudioManager>();
            canMoveBackward = true;
        }

        private void VerticallyMove(float verValue)
        {
            if (!isMoving)
            {
                if (verValue < 0f && !canMoveBackward)
                {
                    if (OnStopMovement != null)
                    {
                        OnStopMovement();
                    }
                    return;
                }
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(0, 0, verValue);
                isMoving = true;
                canMoveBackward = true;
                _anim.SetTrigger(Move);
                _audioManager.Play("Move");
                StartCoroutine(MovePlayer(1f, movingTime));
                if (verValue == 1f && OnForward != null)
                {
                    OnForward();
                }
                if (verValue == -1f && OnBackward != null)
                {
                    OnBackward();
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
                _audioManager.Play("Move");
                StartCoroutine(MovePlayer(1f, movingTime));
            }
        }
        
        public void JumpForward(Vector3 forwardVector, float mTime)
        {
            if (!isMoving)
            {
                _startPos = transform.position;
                _targetPos = _startPos + forwardVector;
                isMoving = true;
                _anim.SetTrigger(Jump);
                _audioManager.Play("Move");
                StartCoroutine(MovePlayer(2f, mTime));
            }
        }
    
        private IEnumerator MovePlayer(float distance, float mTime)
        {
            var tInterpolate = 0f;
            var actualDistance = 0f;
            do
            {
                tInterpolate += Time.deltaTime / mTime;
                transform.position = Vector3.Lerp(_startPos, _targetPos, tInterpolate);
                actualDistance = Vector3.Distance(_targetPos, transform.position);
                yield return null;
            } while (actualDistance <= distance && actualDistance > 0);
            transform.position = _targetPos;
            yield return new WaitForSeconds(timeToMove);
            isMoving = false;
            if (OnStopMovement != null)
            {
                //needs to be called after isMoving otherwise won't fall to water\
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
