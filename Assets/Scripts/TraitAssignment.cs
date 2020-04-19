using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TraitAssignment
{    
    public static int[] currentTraits;

    public static void IncreaseStats(VikingBoi boi, int lastScenarioLevel)
    {
        // Base level up
        boi.stats.Health += (VikingStats.Modest_Health_Raise_Value + boi.seed.Next((-1 * VikingStats.Health_Change_Adjustment), (VikingStats.Health_Change_Adjustment * lastScenarioLevel)));
        boi.stats.Attack += (VikingStats.Modest_Attack_Raise_Value + boi.seed.Next((-1 * VikingStats.Attack_Change_Adjustment), (VikingStats.Attack_Change_Adjustment * lastScenarioLevel)));
        boi.stats.Defense = VikingStats.increasePercentageStat(boi.stats.Defense);              // I'm not going to boost these for the scenario level
        boi.stats.CritChance = VikingStats.increasePercentageStat(boi.stats.CritChance);        // I'm not going to boost these for the scenario level

        // Trait modifiers
        // Bloodthirsty - Raises Ferocity, Attack
        if (boi.stats.CritChance < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] > 0) { boi.stats.CritChance += VikingStats.Stat_Change_Adjustment; }
        boi.stats.Attack += (VikingStats.Modest_Attack_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Bloodthirsty]);
        // Overconfident - Lowers Defense
        if (boi.stats.Defense > 0.15 && boi.stats.Traits[(int)VikingBoi.Traits.Overconfident] > 0) { boi.stats.Defense -= VikingStats.Stat_Change_Adjustment; }
        // Cautious - Raises Defense, Health
        if (boi.stats.Defense < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Cautious] > 0) { boi.stats.Defense += VikingStats.Stat_Change_Adjustment; }
        boi.stats.Health += (VikingStats.Modest_Health_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Cautious]);
        // Coward - Raises Defense, Lowers Ferocity
        if (boi.stats.Defense < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Coward] > 0) { boi.stats.Defense += VikingStats.Stat_Change_Adjustment; }
        if (boi.stats.CritChance > 0.15 && boi.stats.Traits[(int)VikingBoi.Traits.Coward] > 0) { boi.stats.CritChance -= VikingStats.Stat_Change_Adjustment; }
        // Frenzied - Lowers Defense, Health, Raises Ferocity
        if (boi.stats.Defense > 0.15 && boi.stats.Traits[(int)VikingBoi.Traits.Frenzied] > 0) { boi.stats.Defense -= VikingStats.Stat_Change_Adjustment; }
        boi.stats.Health -= (VikingStats.Modest_Health_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Frenzied]);
        if (boi.stats.CritChance < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Frenzied] > 0) { boi.stats.CritChance += VikingStats.Stat_Change_Adjustment; }
        // Suicidal - Raises Attack, Ferocity, Lowers Health
        boi.stats.Attack += (VikingStats.Modest_Attack_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Suicidal]);
        if (boi.stats.CritChance < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Suicidal] > 0) { boi.stats.CritChance += VikingStats.Stat_Change_Adjustment; }
        boi.stats.Health -= (VikingStats.Modest_Health_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Suicidal]);
        // Follower - Raises Attack, Lowers Ferocity
        boi.stats.Attack += (VikingStats.Modest_Attack_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Follower]);
        if (boi.stats.CritChance > 0.15 && boi.stats.Traits[(int)VikingBoi.Traits.Follower] > 0) { boi.stats.CritChance -= VikingStats.Stat_Change_Adjustment; }
        // Christian - Lowers Health
        boi.stats.Health -= (VikingStats.Modest_Health_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Christian]);
        // Party Animal - Raises Health
        boi.stats.Health += (VikingStats.Modest_Health_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.PartyAnimal]);
        // Antagonizer - Lowers Health
        boi.stats.Health -= (VikingStats.Modest_Health_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Antagonizer]);
        // Superstitious - Raises Defense
        if (boi.stats.Defense < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Superstitious] > 0) { boi.stats.Defense += VikingStats.Stat_Change_Adjustment; }
        // Performer - Lowers Defense, Raises Attack, Ferocity
        if (boi.stats.Defense > 0.15 && boi.stats.Traits[(int)VikingBoi.Traits.Performer] > 0) { boi.stats.Defense -= VikingStats.Stat_Change_Adjustment; }
        boi.stats.Attack += (VikingStats.Modest_Attack_Raise_Value * boi.stats.Traits[(int)VikingBoi.Traits.Performer]);
        if (boi.stats.CritChance < 0.85 && boi.stats.Traits[(int)VikingBoi.Traits.Performer] > 0) { boi.stats.CritChance += VikingStats.Stat_Change_Adjustment; }        
    }

    public static void AssignNewTrait(VikingBoi boi)
    {
        int numTraits = Enum.GetValues(typeof(VikingBoi.Actions)).Length;

        boi.stats.Traits[(boi.seed.Next(numTraits))] += 1;
        // Ben says: 
        // DELETE THE ABOVE LINE AND UNCOMMENT/FINISH BELOW IF WE WANT TO DO SOMETHING 
        // SPECIAL AND WE HAVE TIME, I'M JUST GONNA DO RANDOM RIGHT NOW ^^

        // // Pull existing assigned traits
        // currentTraits = boi.stats.Traits;
        
        // double[] allTraits = new double[numTraits];
        // int totalWeight = 0;

        // // Calculate probabilty score of each trait
        // for (int trait = 0; trait < numTraits; trait++) 
        // {
        //     int traitWeight = CalculateProbabilityScore((VikingBoi.Traits)trait);
        //     allTraits[trait] = traitWeight;
        //     if (traitWeight > 0)
        //     {
        //         totalWeight += traitWeight;
        //     }
        // }
        
        // //Create probability curve
        // //Roll for trait
    }

    // private static int CalculateProbabilityScore(VikingBoi.Traits trait)
    // {
    //     int returnScore = 0;

    //     //do different calculations depending on the trait and existing traits
    //     switch (trait)
    //     {
    //         case VikingBoi.Traits.Bloodthirsty:
    //             break;
    //         case VikingBoi.Traits.Greedy:
    //             break;
    //         case VikingBoi.Traits.Haughty:
    //             break;
    //         case VikingBoi.Traits.Overconfident:
    //             break;
    //         case VikingBoi.Traits.Religious:
    //             break;
    //         case VikingBoi.Traits.Drunkard:
    //             break;
    //         case VikingBoi.Traits.Cautious:
    //             break;
    //         case VikingBoi.Traits.Coward:
    //             break;
    //         case VikingBoi.Traits.Frenzied:
    //             break;
    //         case VikingBoi.Traits.Suicidal:
    //             break;
    //         case VikingBoi.Traits.Follower:
    //             break;
    //         case VikingBoi.Traits.Blowhard:
    //             break;
    //         default:
    //             returnScore = 1;
    //             break;
    //     }

    //     //do other stuff if you want

    //     return returnScore;
    // }
}
