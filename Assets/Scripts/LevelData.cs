using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
       public List<GameObject> terrains;

       public static LevelData Instance { get; private set; }
       
       private void Awake()
       {
              if (Instance != null && Instance != this)
              {
                     Destroy(gameObject);
              } else {
                     Instance = this;
              }
       }
}