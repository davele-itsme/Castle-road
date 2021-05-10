using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        PlayerRayCast.OnObjectBelowFound += CheckForWater;
    }

    private void CheckForWater(RaycastHit hitInfo)
    {
        if (hitInfo.collider.gameObject.CompareTag("River") && !_playerMovement.IsOnWoodLog && !_playerMovement.isMoving)
        {
            FallDown();
        }
    }

    private void FallDown()
    {
        transform.position += Vector3.down;
        Destroy(gameObject);
    }
}
