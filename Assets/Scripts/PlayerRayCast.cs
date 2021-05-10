using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    public delegate void HorMove(float value);
    public static event HorMove HorizontalMove;

    public delegate void VerMove(float value);
    public static event VerMove VerticalMove;
    
    public delegate void ObjectBelow(RaycastHit hitInfo);
    public static event ObjectBelow ObjectBelowFound;
    
    private RaycastHit _hitInfo;
    private PlayerMovement _playerMovement;
    private void Start()
    {
        PlayerInput.HorizontalInput += HorizontalRayCast;
        PlayerInput.VerticalInput += VerticalRayCast;
        PlayerMovement.CheckGravity += RayCastDown;
        LogController.CheckGravity += RayCastDown;
        _playerMovement = GetComponent<PlayerMovement>();
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
        var ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out _hitInfo, 1f) && ObjectBelowFound != null)
        {
            ObjectBelowFound(_hitInfo);
        }
    }
}
