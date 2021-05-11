using System.Linq;
using Random = System.Random;

public static class NumberGenerator
{
    public static int GenerateTerrainAmountWithProbability(int[] array)
    {
        var random = new Random();
        var randomIndex = random.Next(0, array.Length);
        return array[randomIndex];
    }
    
    public static int GenerateNumberWithExclude(int length, int excludedNumber)
    {
        var random = new Random();
        var values =
            Enumerable.Range(0, length)
                .Where(value => value != excludedNumber).ToArray();
        var randomIndex = random.Next(0, values.Length);
        return values[randomIndex];
    }
    
    
}