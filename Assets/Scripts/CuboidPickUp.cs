using UnityEngine;

public class CuboidPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Pickup cuboid");
            Destroy(gameObject);
        }
    }
}
