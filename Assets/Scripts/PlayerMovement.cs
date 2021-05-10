using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private float movingTime, timeToMove;
    [SerializeField] private Transform playerTransform;

    private Vector3 _startPos, _targetPos;
    private Animator _anim;
    private bool _isMoving;
    private static readonly int Move = Animator.StringToHash("Move");
    public bool IsOnWoodLog { get; set; }
    private bool _isFalling;

    private void Start()
    {
        PlayerRayCast.HorizontalMove += HorizontallyMove;
        PlayerRayCast.VerticalMove += VerticallyMove;
        _anim = GetComponent<Animator>();
    }

    private void VerticallyMove(float verValue)
    {
        if (!_isMoving)
        {
            _startPos = transform.position;
            _targetPos = _startPos + new Vector3(0, 0, verValue);
            _isMoving = true; 
            _anim.SetTrigger(Move);
            StartCoroutine(MovePlayer());
            if (verValue == 1f) 
            { 
                // terrainGenerator.GenerateTerrain(false);
            }
        }
    }

    private void HorizontallyMove(float horValue)
    {
        if (!_isMoving)
        {
            _startPos = transform.position;
            _targetPos = _startPos + new Vector3(horValue, 0, 0);
            _isMoving = true;
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
        _isMoving = false;
    }
}
