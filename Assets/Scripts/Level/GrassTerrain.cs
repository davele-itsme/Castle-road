using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class GrassTerrain : MonoBehaviour, ITerrain
    {
        [SerializeField] private GameObject grassTerrain;
        [SerializeField] private List<GameObject> grassObjectTypes = new List<GameObject>();
        [SerializeField] private List<GameObject> specialGrassObjectTypes = new List<GameObject>();
        [SerializeField] private Transform levelTransform;
        private List<int> _grassObjectsXPosition;
        
        private LevelData _levelData;
        private GameObject _newTerrain;

        private void Awake()
        {
            _levelData = LevelData.Instance;
        }

        public void InstantiateTerrain(Vector3 currentPosition)
        {
            _newTerrain = Instantiate(grassTerrain, currentPosition, Quaternion.identity);
            _newTerrain.transform.SetParent(levelTransform);
            _levelData.terrains.Add(_newTerrain);
            GenerateGrassObjects();
            GenerateSpecialGrassObjects();
        }
    
        private void GenerateGrassObjects()
        {
            _grassObjectsXPosition = new List<int>();
            var numberOfObjects = Random.Range(2, 5);
            do
            {
                var randomObj = Random.Range(0, grassObjectTypes.Count);
                var x = GetUniqueIntFromRange(-6, 7);
                _grassObjectsXPosition.Add(x);
                InstantiateObject(grassObjectTypes[randomObj], x);
                numberOfObjects--;
            } while (numberOfObjects > 0);
        }

        private void GenerateSpecialGrassObjects()
        {
            var x = GetUniqueIntFromRange(-6, 7);
            var possibilityArray = new []{false, false, false, false, false, false, false, false, false, true};
            var possibilityValue = RandomGenerator<bool>.RandomPicker(possibilityArray);
            if (possibilityValue)
            {
                
                specialGrassObjectTypes[]
                // InstantiateObject();
            }
        }

        private int GetUniqueIntFromRange(int first, int last)
        {
            int x;
            do {
                x = Random.Range(first, last);
            } while (_grassObjectsXPosition.Contains(x));
            return x;
        }

        private void InstantiateObject(GameObject gameObj, int x)
        {
            var newObject =  Instantiate(gameObj, new Vector3(x, 1.5f, _newTerrain.transform.position.z), Quaternion.identity);
            newObject.transform.SetParent(_newTerrain.transform);
        }
    }
}