using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private float movingTime, timeToMove;
    [SerializeField] private Transform playerTransform;

    private readonly string VERTICAL = "Vertical";
    private readonly string HORIZONTAL = "Horizontal";
    
    private Vector3 _startPos, _targetPos;
    private bool _isMoving;
    private Animator _anim;
    private static readonly int Move = Animator.StringToHash("Move");
    private float _tInterpolate, _tMove;
    private RaycastHit _hitInfo;
    public bool IsOnWoodLog { get; set; }
    private bool isFalling;

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
            if (Vector3.Distance(_startPos, transform.position) >= 1f)
            {
                _tMove += Time.deltaTime;
                transform.position = _targetPos;
                if (!(_tMove >= timeToMove)) return;
                _isMoving = false;
                _tInterpolate = 0;
                _tMove = 0;
                return;
            }
            
            _tInterpolate += Time.deltaTime / movingTime;
            transform.position = Vector3.Lerp(_startPos, _targetPos, _tInterpolate);
        }
        else if (isFalling)
        {
            transform.position = Vector3.down * (2 * Time.deltaTime);
        }
        else
        {
            var verValue = Input.GetAxisRaw(VERTICAL);
            var horValue = Input.GetAxisRaw(HORIZONTAL);
            if (Math.Abs(verValue) == 1f)
            {
                RotatePlayer(VERTICAL, verValue);
                var position = transform.position;
                _startPos = new Vector3(position.x, 1.5f, position.z);

                var landingRay = new Ray(new Vector3(_startPos.x, 2, _startPos.z), new Vector3(0, 0, verValue));
                if (!Physics.Raycast(landingRay, out _hitInfo, 1f)) 
                {
                    _targetPos = _startPos + new Vector3(0, 0, verValue);
                    _targetPos.x = Mathf.Round(_targetPos.x);
                    _isMoving = true; 
                    _anim.SetTrigger(Move); 
                    if (verValue == 1f) 
                    { 
                        terrainGenerator.GenerateTerrain(false);
                    }
                }
            }
            else if (Math.Abs(horValue) == 1f)
            {
                RotatePlayer(HORIZONTAL, horValue);
                var position = transform.position;
                _startPos = new Vector3(position.x, 1.5f, position.z);

                var landingRay = new Ray(new Vector3(_startPos.x, 2, _startPos.z), new Vector3(horValue, 0, 0));
                if (!Physics.Raycast(landingRay, out _hitInfo, 1f)) 
                {
                    _targetPos = _startPos + new Vector3(horValue, 0, 0);
                    _targetPos.x = Mathf.Round(_targetPos.x);
                    _isMoving = true;
                    _anim.SetTrigger(Move);
                }
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
