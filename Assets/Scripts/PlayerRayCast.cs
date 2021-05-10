using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public delegate void HorMove(float value);

    public static event HorMove HorizontalMove;

    public delegate void VerMove(float value);

    public static event VerMove VerticalMove;
    
    private RaycastHit _hitInfo;
    private void Start()
    {
        PlayerInput.HorizontalInput += HorizontalRayCast;
        PlayerInput.VerticalInput += VerticalRayCast;
    }

    private void HorizontalRayCast(float horValue)
    {
        var position = transform.position;
        var ray = new Ray(new Vector3(position.x, 2, position.z), new Vector3(horValue, 0, 0));
        if (!Physics.Raycast(ray, out _hitInfo, 1f) && HorizontalMove != null)
        {
            HorizontalMove(horValue);
        }
    }

    private void VerticalRayCast(float verValue)
    {
        var position = transform.position;
        var ray = new Ray(new Vector3(position.x, 2, position.z), new Vector3(0, 0, verValue));
        if (!Physics.Raycast(ray, out _hitInfo, 1f) && VerticalMove != null)
        {
            VerticalMove(verValue);
        }
    }

    private void RayCastDown()
    {
        // var lol = new Ray(_targetPos, Vector3.down);
        // if (Physics.Raycast(lol, out _hitInfo, 1f))
        // {
        //     if (_hitInfo.collider.gameObject.CompareTag("Water") && !IsOnWoodLog)
        //     {
        //         _isFalling = true;
        //     }
        // }
    }
}
