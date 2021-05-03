
using System;
using System.Linq;

public static class NumberGenerator
{
    public static int GenerateTerrainGroupNumber()
    {
        var random = new Random();
        var numbers = new[] { 1, 2, 2, 2, 3, 3, 3, 4, 4, 5};
        var index = random.Next(0, 10);
        return numbers[index];
    }
    
    public static int GenerateNumberWithExclude(int excludedNumber)
    {
        var random = new Random();
        var values = new[] {0, 1, 2};
        values = values.Where(val => val != excludedNumber).ToArray();
        var randomNumber = random.Next(0, 2);
        return values[randomNumber];
    }
    
    
}