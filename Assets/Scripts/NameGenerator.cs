using System.Collections;
using System.Collections.Generic;
using System;

public static class NameGenerator
{
    /// <summary>
    /// Generates a random viking name.
    /// </summary>
    /// <param name="seed">Random object for name generation</param>
    /// <returns>A random viking name</returns>
    public static string GenerateName(System.Random seed)
    {
        return FirstName(seed) + " " + LastNamePartOne(seed) + LastNamePartTwo(seed) + " " + Modifier(seed);
    }

    /// <summary>
    /// Generates a random viking first name.
    /// </summary>
    /// <param name="seed">Random object for name generation</param>
    /// <returns>A random viking fist name</returns>
    private static string FirstName(System.Random seed)
    {
        int index = seed.Next(firstNames.Length);
        return firstNames[index];
    }

    /// <summary>
    /// Generates a random viking last name (part 1).
    /// </summary>
    /// <param name="seed">Random object for name generation</param>
    /// <returns>A random viking last name (part 1)</returns>
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

    /// <summary>
    /// Generates a random viking last name (part 2).
    /// </summary>
    /// <param name="seed">Random object for name generation</param>
    /// <returns>A random viking last name (part 2)</returns>
    private static string LastNamePartTwo(System.Random seed)
    { 
        int index = seed.Next(lastNamePartTwos.Length);
        return lastNamePartTwos[index];
    }

    /// <summary>
    /// Generates a random viking name modifier.
    /// </summary>
    /// <param name="seed">Random object for name generation</param>
    /// <returns>A random viking name modifier</returns>
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
