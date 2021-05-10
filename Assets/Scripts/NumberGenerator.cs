using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class NumberGenerator
{
    public static int GenerateTerrainAmountWithProbability(int[] array)
    {
        var random = new Random();
        var index = random.Next(0, array.Length);
        return array[index];
    }
    
    public static int GenerateNumberWithExclude(List<GameObject> terrainTypes, int excludedNumber)
    {
        var random = new Random();
        var values = new List<int>();
        for(var i = 0; i < terrainTypes.Count; i++)
        {
            if (i != excludedNumber)
            {
                values.Add(i);
            }
        }
        var randomNumber = random.Next(0, terrainTypes.Count - 1);
        return values[randomNumber];
    }
    
    
}