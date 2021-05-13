using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelData : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> terrains = new List<GameObject>();

        public static LevelData Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}