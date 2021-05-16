using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomGenerator<T>
{
    public static T RandomPicker(T[] array)
    {
        var randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];
    }
    
    public static int GenerateNumberWithExclude(IEnumerable<int> list, int excludedNumber)
    {
        var values = list.Where(value => value != excludedNumber).ToArray();
        var randomIndex = Random.Range(0, values.Length);
        return values[randomIndex];
    }
}