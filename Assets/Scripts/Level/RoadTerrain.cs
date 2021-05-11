using UnityEngine;

namespace Level
{
    public class RoadTerrain : MonoBehaviour, ITerrain
    {
        [SerializeField] private GameObject roadTerrain;
        [SerializeField] private Transform levelTransform;

        private int count;
        private LevelData _levelData;
    
        private void Awake()
        {
            _levelData = LevelData.Instance;
        }
    
        public void InstantiateTerrain(Vector3 currentPosition)
        {
            var newTerrain = Instantiate(roadTerrain, currentPosition, Quaternion.identity);
            newTerrain.transform.SetParent(levelTransform);
            _levelData.terrains.Add(newTerrain);
        }
    }
}