using System.Collections;
using System.Collections.Generic;
using System;

public static class SpriteGenerator
{
    private static string Head(System.Random seed)
    {
        int index = seed.Next(firstNames.Length);
        return firstNames[index];
    }

    private static string Body(System.Random seed)
    { 
        int index = seed.Next(lastNamePartOnes.Length);
        return lastNamePartOnes[index];
    }
    
    private static string Legs(System.Random seed)
    { 
        int index = seed.Next(lastNamePartTwos.Length);
        return lastNamePartTwos[index];
    }

    private static string[] firstNames = 
    {
        "head1"
    };

    private static string[] lastNamePartOnes = 
    {
        "Mc"
    };

    private static string[] lastNamePartTwos = 
    {
        "Large"
    };
}
