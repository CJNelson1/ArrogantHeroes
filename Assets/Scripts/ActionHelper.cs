using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionHelper
{  
    #region Weight Constants
    private const int Very_High_Weight = 1000;      // Big Chungus
    private const int High_Weight = 250;
    private const int Medium_Weight = 100;
    private const int Low_Weight = 50;
    private const int Very_Low_Weight = 20;
    private const int No_Weight = 10;
    #endregion

    #region Multiplier Constants
    private const double Regular_Higher_Multiplier = 100;
    private const double Significant_Higher_Multiplier = 500;
    private const double Regular_Lower_Multiplier = 0.9;
    private const double Significant_Lower_Multiplier = 0.5;
    #endregion

    public static VikingBoi.Actions SelectAction(System.Random seed, VikingStats stats, bool inGroup, bool firstAction, bool debug)
    {
        // init the potential actions array to all zeroes
        double[] potentialActions = new double[Enum.GetNames(typeof(VikingBoi.Actions)).Length];

        #region Initial Weighting
        // Attack - Very Heavily Weighted
        potentialActions[(int) VikingBoi.Actions.Attack] = Very_High_Weight;

        // Defend - Heavily Weighted
        potentialActions[(int) VikingBoi.Actions.Defend] = High_Weight;

        // Insult - Medium Weighted
        potentialActions[(int) VikingBoi.Actions.Insult] = Medium_Weight;

        // Drink - Low Weighted
        potentialActions[(int) VikingBoi.Actions.Drink] = Low_Weight;

        // Cower - Very low weighted
        potentialActions[(int) VikingBoi.Actions.Cower] = Very_Low_Weight;

        // Flee - Very low weighted
        potentialActions[(int) VikingBoi.Actions.Flee] = Very_Low_Weight;

        // Ferocious Attack - Weight based on CritChance (Furocity)
        potentialActions[(int) VikingBoi.Actions.FerociousAttack] = stats.CritChance;

        // Mocking Attack - No weight
        potentialActions[(int) VikingBoi.Actions.MockingAttack] = No_Weight;

        // Play the Villain - No weight
        potentialActions[(int) VikingBoi.Actions.PlayTheVillain] = No_Weight;

        // Reckless Attack - No weight
        potentialActions[(int) VikingBoi.Actions.RecklessAttack] = No_Weight;

        // Plunder - Very low weight
        potentialActions[(int) VikingBoi.Actions.Plunder] = Very_Low_Weight;

        // Dedicate to Odin - Very low weight
        potentialActions[(int) VikingBoi.Actions.DedicateToOdin] = Very_Low_Weight;

        // Pray to the Gods - No weight
        potentialActions[(int) VikingBoi.Actions.PrayToTheGods] = No_Weight;

        // Go berserk - No weight
        potentialActions[(int) VikingBoi.Actions.GoBerserk] = No_Weight;

        // Brag - No weight
        potentialActions[(int) VikingBoi.Actions.Brag] = No_Weight;

        /* Group Actions */
        if (inGroup)
        {
            // Berserker Rage - Very low weight
            potentialActions[(int) VikingBoi.Actions.BerserkerRage] = Very_Low_Weight;

            // Give a Speech -Very low weight
            potentialActions[(int) VikingBoi.Actions.GiveASpeech] = Very_Low_Weight;

            // Taunt - Very low weight
            potentialActions[(int) VikingBoi.Actions.Taunt] = Very_Low_Weight;

            // Hide in the Back - Very low weight
            potentialActions[(int) VikingBoi.Actions.HideInTheBack] = Very_Low_Weight;

            // Steal the Glory - Very low weight
            potentialActions[(int) VikingBoi.Actions.StealAllTheGlory] = Very_Low_Weight;

            // Gang up - Low weight
            potentialActions[(int) VikingBoi.Actions.GangUp] = Low_Weight;
        }
        #endregion

        #region Multipliers
        /*
        Bloodthirsty
        Raises: Reckless Attack, Go Berserk, Play The Villain
        Lowers: Defend, Cower, Hide in the Back 
        */
        potentialActions[(int) VikingBoi.Actions.RecklessAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GoBerserk] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.PlayTheVillain] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Defend] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Cower] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.HideInTheBack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] * Regular_Lower_Multiplier));

        /*
        Greedy
        Raises: Plunder (significantly)
        Lowers: none
        */
        potentialActions[(int) VikingBoi.Actions.Plunder] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Greedy] * Significant_Higher_Multiplier));

        /*
        Haughty
        Raises: Brag, Insult, Give a Speech
        Lowers: Dedicate to Odin, Pray to the Gods
        */
        potentialActions[(int) VikingBoi.Actions.Brag] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Haughty] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Insult] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Haughty] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GiveASpeech] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Haughty] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.DedicateToOdin] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Haughty] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.PrayToTheGods] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Haughty] * Regular_Lower_Multiplier));

        /*
        Overconfident
        Raises: Mocking Attack, Brag, Taunt
        Lowers: Cower, Flee, Defend
        */
        potentialActions[(int) VikingBoi.Actions.MockingAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Overconfident] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Brag] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Overconfident] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Taunt] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Overconfident] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Cower] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Overconfident] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Flee] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Overconfident] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Defend] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Overconfident] * Regular_Lower_Multiplier));

        /*
        Religious
        Raises: Dedicate to Odin, Pray to the Gods
        Lowers: Flee
        */
        potentialActions[(int) VikingBoi.Actions.DedicateToOdin] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Religious] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.PrayToTheGods] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Religious] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Flee] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Religious] * Regular_Lower_Multiplier));

        /*
        Drunkard
        Raises: Drink, Brag, Insult
        Lowers: none
        */
        potentialActions[(int) VikingBoi.Actions.Drink] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Drunkard] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Brag] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Drunkard] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Insult] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Drunkard] * Regular_Higher_Multiplier));

        /*
        Cautious
        Raises: Defend, Cower, Flee
        Lowers: Reckless Attack, Go Berserk, Mocking Attack
        */
        potentialActions[(int) VikingBoi.Actions.Defend] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Cautious] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Cower] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Cautious] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Flee] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Cautious] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.RecklessAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Cautious] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GoBerserk] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Cautious] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.MockingAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Cautious] * Regular_Lower_Multiplier));

        /*
        Coward
        Raises: Cower, Flee, Hide in the Back
        Lowers: none
        */
        potentialActions[(int) VikingBoi.Actions.Cower] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Coward] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Flee] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Coward] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.HideInTheBack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Coward] * Regular_Higher_Multiplier));

        /*
        Frenzied
        Raises: Reckless Attack, Go Berserk, Attack, Mocking Attack
        Lowers: none
        */
        potentialActions[(int) VikingBoi.Actions.RecklessAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Frenzied] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GoBerserk] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Frenzied] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Attack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Frenzied] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.MockingAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Frenzied] * Regular_Higher_Multiplier));

        /*
        Suicidal
        Raises: none
        Lowers: CANNOT Defend, Cower, Flee
        */
        if (stats.Traits[(int)VikingBoi.Traits.Suicidal] > 0)
        {
            potentialActions[(int) VikingBoi.Actions.Defend] = 0;
            potentialActions[(int) VikingBoi.Actions.Cower] = 0;
            potentialActions[(int) VikingBoi.Actions.Flee] = 0;
        }

        /*
        Follower
        Raises: First action is always Dedicate to Odin
        Lowers: none
        */
        if (firstAction) { return VikingBoi.Actions.DedicateToOdin; }

        /*
        Blowhard
        Raises: First Action is always Brag
        Lowers: none
        */
        if (firstAction) { return VikingBoi.Actions.Brag; }

        /*
        Anarchist
        Raises: none
        Lowers: none
        */


        /*
        Jerk
        Raises: Play the Villain, Plunder, Steal The Glory
        Lowers: none
        */
        potentialActions[(int) VikingBoi.Actions.PlayTheVillain] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Jerk] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.Plunder] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Jerk] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.StealAllTheGlory] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Jerk] * Regular_Higher_Multiplier));

        /*
        Christian
        Raises: none
        Lowers: CANNOT Dedicate to Odin, Pray to the Gods, Drink
        */
        if (stats.Traits[(int)VikingBoi.Traits.Christian] > 0)
        {
            potentialActions[(int) VikingBoi.Actions.DedicateToOdin] = 0;
            potentialActions[(int) VikingBoi.Actions.PrayToTheGods] = 0;
            potentialActions[(int) VikingBoi.Actions.Drink] = 0;
        }

        /*
        Party Animal
        Raises: Drink, Give a Speech, Reckless Attack
        Lowers: 
        */
        potentialActions[(int) VikingBoi.Actions.Drink] *= (1 + (stats.Traits[(int)VikingBoi.Traits.PartyAnimal] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GiveASpeech] *= (1 + (stats.Traits[(int)VikingBoi.Traits.PartyAnimal] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.RecklessAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.PartyAnimal] * Regular_Higher_Multiplier));

        /*
        Antagonizer
        Raises: First action is always Mocking Attack, Raises Taunt
        Lowers: Give a Speech, Gang Up
        */
        if (firstAction) { return VikingBoi.Actions.MockingAttack; }
        potentialActions[(int) VikingBoi.Actions.Taunt] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Antagonizer] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GiveASpeech] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Antagonizer] * Regular_Lower_Multiplier));
        potentialActions[(int) VikingBoi.Actions.GangUp] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Antagonizer] * Regular_Lower_Multiplier));

        /*
        Contrarian
        Raises: (When on a team) Only takes Selfish actions (Berserker Rage, Hide in the Back, Steal the Glory)
        Lowers: none
        */
        if (stats.Traits[(int)VikingBoi.Traits.Contrarian] > 0)
        {
            potentialActions[(int) VikingBoi.Actions.BerserkerRage] *= 1000000000;
            potentialActions[(int) VikingBoi.Actions.HideInTheBack] *= 1000000000;
            potentialActions[(int) VikingBoi.Actions.StealAllTheGlory] *= 1000000000;
        }

        /*
        Superstitious
        Raises: Cower, Pray to the Gods
        Lowers: none
        */
        potentialActions[(int) VikingBoi.Actions.Cower] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Superstitious] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.PrayToTheGods] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Superstitious] * Regular_Higher_Multiplier));

        /*
        Violent
        Raises: First Action is always Go Berserk, Raises Berserker Rage
        Lowers: Mocking Attack
        */
        if (firstAction) { return VikingBoi.Actions.GoBerserk; }
        potentialActions[(int) VikingBoi.Actions.BerserkerRage] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Violent] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.MockingAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Violent] * Regular_Lower_Multiplier));

        /*
        Performer
        Raises: Play The Villain, Mocking Attack, Dedicate to Odin
        Lowers: CANNOT Go Berserk, Hide in the Back
        */
        potentialActions[(int) VikingBoi.Actions.PlayTheVillain] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Performer] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.MockingAttack] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Performer] * Regular_Higher_Multiplier));
        potentialActions[(int) VikingBoi.Actions.DedicateToOdin] *= (1 + (stats.Traits[(int)VikingBoi.Traits.Performer] * Regular_Higher_Multiplier));
        if (stats.Traits[(int)VikingBoi.Traits.Performer] > 0)
        {
            potentialActions[(int) VikingBoi.Actions.GoBerserk] = 0;
            potentialActions[(int) VikingBoi.Actions.HideInTheBack] = 0;
        }
        #endregion

        // Account for always attack
        if (stats.ScenarioMustAttack)
        {
            potentialActions[(int) VikingBoi.Actions.Defend] = 0;
            potentialActions[(int) VikingBoi.Actions.Insult] = 0;
            potentialActions[(int) VikingBoi.Actions.Drink] = 0;
            potentialActions[(int) VikingBoi.Actions.Cower] = 0;
            potentialActions[(int) VikingBoi.Actions.Flee] = 0;
            potentialActions[(int) VikingBoi.Actions.PlayTheVillain] = 0;
            potentialActions[(int) VikingBoi.Actions.Plunder] = 0;
            potentialActions[(int) VikingBoi.Actions.DedicateToOdin] = 0;
            potentialActions[(int) VikingBoi.Actions.PrayToTheGods] = 0;
            potentialActions[(int) VikingBoi.Actions.Brag] = 0;
            potentialActions[(int) VikingBoi.Actions.GiveASpeech] = 0;
            potentialActions[(int) VikingBoi.Actions.Taunt] = 0;
            potentialActions[(int) VikingBoi.Actions.HideInTheBack] = 0;
        }

        // Accumulate the total weights for the possible actions
        double denominator = 0;
        foreach (double actionProbability in potentialActions)
        {
            denominator += actionProbability;
        }

        // Pick a random value somewhere in there
        double result = seed.NextDouble() * denominator;

        // Debug :|
        if (debug)
        {
            Debug.Log("Random: " + result.ToString());
            for (int i = 0; i < potentialActions.Length; i++)
            {
                Debug.Log(((VikingBoi.Actions)i).ToString() + ": " + potentialActions[i].ToString());
            }
        }

        // Find what action that corresponds to
        double numerator = 0;
        for (int i = 0; i < potentialActions.Length; i++)
        {
            numerator += potentialActions[i];
            if (numerator >= result)
            {
                return (VikingBoi.Actions)i;
            }
        }

        // If for some reason, we didn't find an action because Ben is a shitty programmer, just attack I suppose.
        return VikingBoi.Actions.Attack;
    }

    public static void TestSelectAction()
    {
        System.Random seed = new System.Random();
        VikingStats stats = new VikingStats(seed);
        bool inGroup = false;
        bool firstAction = false;
        stats.CritChance = 1;
        stats.Traits[(int)VikingBoi.Traits.Bloodthirsty] = 0;
        stats.Traits[(int)VikingBoi.Traits.Greedy] = 0;
        stats.Traits[(int)VikingBoi.Traits.Haughty] = 0;
        stats.Traits[(int)VikingBoi.Traits.Overconfident] = 0;
        stats.Traits[(int)VikingBoi.Traits.Religious] = 0;
        stats.Traits[(int)VikingBoi.Traits.Drunkard] = 0;
        stats.Traits[(int)VikingBoi.Traits.Cautious] = 0;
        stats.Traits[(int)VikingBoi.Traits.Coward] = 0;
        stats.Traits[(int)VikingBoi.Traits.Frenzied] = 0;
        stats.Traits[(int)VikingBoi.Traits.Suicidal] = 0;
        stats.Traits[(int)VikingBoi.Traits.Follower] = 0;
        stats.Traits[(int)VikingBoi.Traits.Blowhard] = 0;
        stats.Traits[(int)VikingBoi.Traits.Anarchist] = 0;
        stats.Traits[(int)VikingBoi.Traits.Jerk] = 0;
        stats.Traits[(int)VikingBoi.Traits.Christian] = 0;
        stats.Traits[(int)VikingBoi.Traits.PartyAnimal] = 0;
        stats.Traits[(int)VikingBoi.Traits.Antagonizer] = 0;
        stats.Traits[(int)VikingBoi.Traits.Contrarian] = 0;
        stats.Traits[(int)VikingBoi.Traits.Superstitious] = 0;
        stats.Traits[(int)VikingBoi.Traits.Violent] = 0;
        stats.Traits[(int)VikingBoi.Traits.Performer] = 0;
        
        Debug.Log("Selected Action: " + SelectAction(seed, stats, inGroup, firstAction, false).ToString());
    }
}