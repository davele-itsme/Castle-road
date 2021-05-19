using Player;
using UnityEngine;

namespace Level
{
    public class LevelBoundary : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private int levelBoundaryDistance;
        private LevelData _levelData;
        private PlayerMovement _playerMovement;
        private void Start()
        {
            _levelData = LevelData.Instance;
            _playerMovement = player.GetComponent<PlayerMovement>();
            PlayerMovement.OnBackward += CheckForMapBoundary;
        }

        private void CheckForMapBoundary()
        {
            var distance = player.transform.position.z - _levelData.terrains[0].transform.position.z;
            if (distance < levelBoundaryDistance)
            {
                _playerMovement.canMoveBackward = false;
            }
        }

        private void OnDestroy()
        {
            PlayerMovement.OnBackward -= CheckForMapBoundary;
        }
    }
}