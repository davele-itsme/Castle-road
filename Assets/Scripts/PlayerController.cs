using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerTransform;
    private Vector3 _startPos, _targetPos;
    private bool _isMoving;
    private Animator _anim;
    private static readonly int Move = Animator.StringToHash("Move");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
       MovePlayer();
       
    }

    private void MovePlayer()
    {
        if (_isMoving)
        {
            //Snapping, player can react in the last 0.2f
            if (Vector3.Distance(_startPos, transform.position) > 0.8f)
            {
                transform.position = _targetPos;
                _isMoving = false;
                return;
            }
            transform.position += (_targetPos - _startPos) * (moveSpeed * Time.deltaTime);
            
        }
        else
        {
            if (Math.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (Input.GetAxisRaw("Vertical") == 1f)
                {
                    RotatePlayer(new Vector3(0, 180, 0));
                }
                else
                {
                    RotatePlayer(new Vector3(0, 0, 0));
                }
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
                _isMoving = true;
                _anim.SetTrigger(Move);
            }
            else if (Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (Input.GetAxisRaw("Horizontal") == 1f)
                {
                    RotatePlayer(new Vector3(0, 270, 0));
                }
                else
                {
                    RotatePlayer(new Vector3(0, 90, 0));
                }
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                _isMoving = true;
                _anim.SetTrigger(Move);
            }
        }
    }

    private void RotatePlayer(Vector3 direction)
    {
        playerTransform.rotation = Quaternion.Euler(direction);
    }
}
