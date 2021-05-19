using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

//Game manager
namespace Level
{
    public class TerrainController : MonoBehaviour
    {
        public delegate void TerrainCreation();
        public static event TerrainCreation OnCreated;
        
        [SerializeField] private Transform playerTransform;
        [SerializeField] private int maxSpawn;
        [SerializeField] private float spawnDistance;
    
        private LevelData _levelData;
        private Vector3 _currentPosition;
        private int _lastTerrainType;
        private int _terrainCounter = 11;
        private ITerrain[] _terrainTypes;
        private IEnumerable<int> _possibleTerrainTypes;

        private void Start()
        {
            _levelData = LevelData.Instance;
            _currentPosition = new Vector3(0, 1, -10);
            _terrainTypes = gameObject.GetComponents<ITerrain>();
            _possibleTerrainTypes = Enumerable.Range(0, _terrainTypes.Length);
            PlayerMovement.OnForward += ControlTerrain;
            StartTerrain();
        }

        //Instantiate grass terrain as safe zone and then other terrains
        private void StartTerrain()
        {
            for (var i = 0; i < _terrainCounter; i++)
            {
                InstantiateTerrain();
            }
            do
            {
                ControlTerrain();
            } while (CheckDistanceToLastTerrain());
        }

        private void ControlTerrain()
        {
            if (CheckDistanceToLastTerrain())
            {
                GenerateTerrainType();
                for (var i = _terrainCounter; i > 0; i--)
                {
                    InstantiateTerrain();
                }

                if (OnCreated != null)
                {
                    OnCreated();
                }
            }
    
            if (CheckDistanceToBeginning())
            {
                DestroyTerrain();
            }
        }
    
        private bool CheckDistanceToLastTerrain()
        {
            if (_levelData.terrains.Count == 0) return true;
            var distance = _levelData.terrains.Last().transform.position.z - playerTransform.position.z;
            return spawnDistance > distance;
        }
    
        private bool CheckDistanceToBeginning()
        {
            if (_levelData.terrains.Count < maxSpawn) return false;
            var distance = playerTransform.position.z - _levelData.terrains[0].transform.position.z;
            return maxSpawn - spawnDistance < distance;
        }
    
        private void GenerateTerrainType()
        {
            _lastTerrainType = RandomGenerator<int>.GenerateNumberWithExclude(_possibleTerrainTypes, _lastTerrainType);
            var numbers = new[] { 1, 2, 2, 2, 3, 3, 3, 4, 4, 5}; 
            _terrainCounter = RandomGenerator<int>.RandomPicker(numbers);
        }
    
        private void InstantiateTerrain()
        {
            _terrainTypes[_lastTerrainType].InstantiateTerrain(_currentPosition);
            _currentPosition.z++;
        }
    
        private void DestroyTerrain()
        {
            if (_levelData.terrains.Count > maxSpawn)
            {
                Destroy(_levelData.terrains[0]);
                _levelData.terrains.RemoveAt(0);
            }
        }

        private void OnDestroy()
        {
            PlayerMovement.OnForward -= ControlTerrain;
        }
    }
}
