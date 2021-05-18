using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private GameObject playerModel;
        [SerializeField] private int timer;
        private bool _canJump;

        private void Start()
        {
            
        }

        private void Jump()
        {
            if (_canJump)
            {
                StartCoroutine(Timer());
                _canJump = false;
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(timer);
            _canJump = true;
        }
    }
}