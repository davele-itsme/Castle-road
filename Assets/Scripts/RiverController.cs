using System;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerController>().IsOnWoodLog)
            {
                Debug.Log("FALL DOWN");
            }
        }
    }
}
