using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speed;

    private readonly string VERTICAL = "Vertical";
    private readonly string HORIZONTAL = "Horizontal";
    
    private Vector3 _startPos, _targetPos;
    private bool _isMoving;
    private static readonly int Move = Animator.StringToHash("Move");
    private float _tInterpolate, _tMove;
    private RaycastHit _hitInfo;
    public bool IsOnWoodLog { get; set; }
    private bool isFalling;
    private Rigidbody _rigidbody;
    private Vector3 movement;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        movement.z = Input.GetAxisRaw(VERTICAL);
        movement.x = Input.GetAxisRaw(HORIZONTAL);
        if (movement.z != 0)
        { 
            transform.rotation = Quaternion.LookRotation (new Vector3(0, _rigidbody.position.y, movement.z));
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * (Time.deltaTime * speed));
    }

    private void MovePlayer()
    {
        // if (Math.Abs(verValue) == 1f)
            // {
            //     RotatePlayer(VERTICAL, verValue);
            //     _startPos = transform.position;
            //
            //     var landingRay = new Ray(new Vector3(_startPos.x, 2, _startPos.z), new Vector3(0, 0, verValue));
            //     if (!Physics.Raycast(landingRay, out _hitInfo, 1f)) 
            //     {
            //         _targetPos = _startPos + new Vector3(0, 0, verValue);
            //         _targetPos.x = Mathf.Round(_targetPos.x);
            //         _isMoving = true; 
            //         _anim.SetTrigger(Move); 
            //         if (verValue == 1f) 
            //         { 
            //             terrainGenerator.GenerateTerrain(false);
            //         }
            //     }
            // }
            // else if (Math.Abs(horValue) == 1f)
            // {
            //     RotatePlayer(HORIZONTAL, horValue);
            //
            //     var landingRay = new Ray(new Vector3(_startPos.x, 2, _startPos.z), new Vector3(horValue, 0, 0));
            //     if (!Physics.Raycast(landingRay, out _hitInfo, 1f)) 
            //     {
            //         _targetPos = _startPos + new Vector3(horValue, 0, 0);
            //         _targetPos.x = Mathf.Round(_targetPos.x);
            //         _isMoving = true;
            //     }
            // }
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
     
    }

    private void lol()
    {
        var lol = new Ray(_targetPos, Vector3.down);
        if (Physics.Raycast(lol, out _hitInfo, 1f))
        {
            if (_hitInfo.collider.gameObject.CompareTag("Water") && !IsOnWoodLog)
            {
                isFalling = true;
            }
        }
    }
}
