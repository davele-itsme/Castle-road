using UnityEngine;

public class CuboidPickUp : MonoBehaviour
{
    public delegate void Cuboid();
    public static event Cuboid CuboidPickedUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Pickup cuboid");
            if (CuboidPickedUp != null)
            {
                CuboidPickedUp();
            }
            Destroy(gameObject);
        }
    }
}
