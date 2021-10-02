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
        //Harmonize names
        string partOne = LastNamePartOne(seed);
        string partTwo = LastNamePartTwo(seed);
        string harmonizedName;
        if (partOne == null || partOne == "Mc")
        {
            partTwo = char.ToUpper(partTwo[0]) + partTwo.Substring(1);
        }
        harmonizedName = partOne + partTwo;
        return FirstName(seed) + " " + harmonizedName + " " + Modifier(seed);
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
        if (seed.Next(10) > 3)
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
        "Huge", "Leif", "Sindre", "Toki", "Sten", "Svend", "Troels", "Torsten", "Ulf", "Magnus", "Olaf", "Finni", "Vemund",
        "Bjarni", "Ofeigr", "Kolbeinn", "Ragnar", "Bjorn", "Harald", "Sweyn", "Eddval", "Kotkel", "Arne", "Birger", "Bjørn",
        "Bo", "Erik", "Frode", "Gorm", "Halfdan", "Knud", "Kåre", "Njal", "Roar", "Rune", "Sune", "Skarde", "Toke", "Ulf",
        "Ødger", "Åge", "Godefroy", "Osmond", "Hastain", "Burnouf", "Estur", "Ivar", "Lodbrok", "Liv", "Bodil", "Gertrude",
        "Helga", "Inga", "Randi", "Revna", "Tora", "Smelgard", "Pointy", "The", "Brosef", "Schtee", "Brodin", "C Jager", "BenHild",
        "Ethgar", "Drewski", "Zachfried", "Franfnir", "Samgurd", "Jarlcob", "Jo", "Thorin", "Snorri", "Big", "Big Willy", "Hodor"
    };

    private static string[] lastNamePartOnes = 
    {
        "Mc", "Strokes", "Big", "Thunder", "War", "Leather", "Hairy", "Coal", "Long", "Butter", "Iron", "Blood", "Grey", "Ox",
        "Creek", "Spooky", "Wry", "Young", "Pale", "Chubby", "Wood", "Night", "Woad", "Dirt", "Law", "Death", "Meat", "Leather",
        "Strong", "Wild", "Chain", "Splint", "Fire", "Ice", "Green", "Blue", "Black", "White", "Wet", "Great", "Battle", "Light",
        "Heavy", "Cross", "Long", "Short", "Tall", "Bronze", "Gold", "Silver", "Copper", "Tin", "Royal", "Frost", "Ghost", "True",
        "Burning", "Silent", "Cloud", "Storm", "Bear", "Wind", "Blessed", "Owl", "Sir", "Eagle", "Evil", "Holy", "Dark", "Tiny",
        "Schtee", "Jo", "Mead", "Twiddle", "Sticky", "Wise", "Lady", "Fastly", "Stabby", "Zoop", "Rag"
    };

    private static string[] lastNamePartTwos = 
    {
        "large", "hard", "son", "beard", "tooth", "cheek", "bird", "king", "truth", "town", "church", "shirt", "bow", "hand",
        "neck", "breeches", "brow", "penis", "side", "axe", "cloak", "foot", "nose", "ear", "Heart", "hall", "breath", "Dude",
        "ass", "eye", "arm", "head", "legs", "scale", "club", "hammer", "sword", "mace", "bow", "strike", "arrow", "strider",
        "skin", "fox", "shtee", "jo", "waft", "fratheim", "guzzler", "quarterDan", "longboi", "thumbs", "weed", "toes", "littleCooks",
        "artly", "boi", "spice"
    };

    private static string[] modifiers = 
    {
        "the Best", "the Fuckboi", "the Red", "the Worst", "the Average", "the Generous", "the Deepminded", "the Witch-Breaker", 
        "the Boneless", "the Stout", "the Excellent", "the Honorable", "the Pumped", "the Woebegone", "the Auspicious", "the Glorious",
        "the Unbiased", "the Quizzical", "the Violent", "the Unwieldy", "the Normal", "the Descriptive", "the Faithful", "the Tense",
        "the Barelegged", "the Condemned", "the Bored", "the Swift", "the Trashy", "the Massive", "the Energetic", "the Deserted", 
        "the Astonishing", "the Greasy", "the Last", "the Brash", "the Sincere", "the Quirky", "the Delicious", "the Certain",
        "the Acceptable", "the Embarrassed", "the Scandalous", "the Tremendous", "the Abundant", "the Safe", "the Mature", "the Jittery",
        "the Wrathful", "the Sticky", "the Horrible", "the Eminent", "the Parsimonious", "the Amniscient", "the Skillful", "the Guiltless",
        "the Schtee", "and a Half", "the Improbable", "the Boi", "the Gentleman", "the Cheater", "the Survivor", "the Boy", "Jr.", "Sr."
    };
}
