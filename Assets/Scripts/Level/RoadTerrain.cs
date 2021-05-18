using System.Collections;
using UnityEngine;

namespace Level
{
    public class RoadTerrain : MonoBehaviour, ITerrain
    {
        [SerializeField] private GameObject roadTerrain;
        [SerializeField] private GameObject roadObjectGenerator;
        [SerializeField] private Transform levelTransform;
        private AudioManager _audioManager;
        private LevelData _levelData;
    
        private void Awake()
        {
            _levelData = LevelData.Instance;
            _audioManager = FindObjectOfType<AudioManager>();
            StartCoroutine(PlaySound());
        }

        public void InstantiateTerrain(Vector3 currentPosition)
        {
            var newTerrain = Instantiate(roadTerrain, currentPosition, Quaternion.identity);
            newTerrain.transform.SetParent(levelTransform);
            _levelData.terrains.Add(newTerrain);
            GenerateRoadObjects(newTerrain);
        }

        private void GenerateRoadObjects(GameObject newTerrain)
        {
            var z = newTerrain.transform.position.z;
            var side = new[] {-10, 10};
            var position = RandomGenerator<int>.RandomPicker(side);
            var newRoadObjectGenerator = Instantiate(roadObjectGenerator, new Vector3(position, 1.35f, z), Quaternion.identity);
            newRoadObjectGenerator.transform.SetParent(newTerrain.transform);
        }

        private IEnumerator PlaySound()
        {
            while (true)
            {
                var time = Random.Range(10f, 20f);
                yield return new WaitForSeconds(time);
                _audioManager.PlayRandomGreeting();
            }
        }
    }
}