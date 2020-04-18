using System.Collections;
using System.Collections.Generic;
using System;

public static class NameGenerator
{
    public static string GenerateName(System.Random seed)
    {
        return FirstName(seed) + " " + LastNamePartOne(seed) + LastNamePartTwo(seed) + " " + Modifier(seed);
    }

    private static string FirstName(System.Random seed)
    {
        int index = seed.Next(firstNames.Length);
        return firstNames[index];
    }

    private static string LastNamePartOne(System.Random seed)
    { 
        if (seed.Next(10) > 7)
        {
            int index = seed.Next(lastNamePartOnes.Length);
            return lastNamePartOnes[index];
        }
        else
        {
            return null;
        }
    }
    
    private static string LastNamePartTwo(System.Random seed)
    { 
        int index = seed.Next(lastNamePartTwos.Length);
        return lastNamePartTwos[index];
    }
    
    private static string Modifier(System.Random seed)
    {
        int index = seed.Next(modifiers.Length);
        return modifiers[index];
    }

    private static string[] firstNames = 
    {
        "Huge"
    };

    private static string[] lastNamePartOnes = 
    {
        "Mc"
    };

    private static string[] lastNamePartTwos = 
    {
        "Large"
    };

    private static string[] modifiers = 
    {
        "the Best"
    };
}
