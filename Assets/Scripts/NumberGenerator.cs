using System.Linq;
using UnityEngine;

public static class NumberGenerator
{
    public static int RandomPicker(int[] array)
    {
        var length = array.Length;
        var randomIndex = Random.Range(0, length);
        return array[randomIndex];
    }
    
    public static int GenerateNumberWithExclude(int length, int excludedNumber)
    {
        var values =
            Enumerable.Range(0, length)
                .Where(value => value != excludedNumber).ToArray();
        var randomIndex = Random.Range(0, values.Length);
        return values[randomIndex];
    }
}