using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelData : MonoBehaviour
    {
        public List<GameObject> terrains = new List<GameObject>();

        private static LevelData instance;
        public static LevelData Instance
        {
            get { return instance; }
        }
       
        private void Awake()
        {
            instance = this;
        }
    }
}