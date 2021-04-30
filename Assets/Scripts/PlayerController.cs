using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerTransform;

    private readonly string VERTICAL = "Vertical";
    private readonly string HORIZONTAL = "Horizontal";
    
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
            if (Math.Abs(Input.GetAxisRaw(VERTICAL)) == 1f)
            {
                RotatePlayer(VERTICAL, Input.GetAxisRaw(VERTICAL));
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(0, 0, Input.GetAxisRaw(VERTICAL));
                _isMoving = true;
                _anim.SetTrigger(Move);
            }
            else if (Math.Abs(Input.GetAxisRaw(HORIZONTAL)) == 1f)
            {
                RotatePlayer(HORIZONTAL, Input.GetAxisRaw(HORIZONTAL));
                _startPos = transform.position;
                _targetPos = _startPos + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                _isMoving = true;
                _anim.SetTrigger(Move);
            }
        }
    }

    private void RotatePlayer(string direction, float value)
    {
        var directionVector = new Vector3(0, 0, 0);
        if (direction.Equals(HORIZONTAL))
        {
            if (value == 1f)
            {
                directionVector.y = 270;
            }
            else
            {
                directionVector.y = 90;
            }
        }
        else
        {
            if (value == 1f)
            {
                directionVector.y = 180;
            }
        }
        playerTransform.rotation = Quaternion.Euler(directionVector);
     
    }
}
