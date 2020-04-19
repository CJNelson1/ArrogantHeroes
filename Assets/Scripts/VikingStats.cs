using System;

public class VikingStats
{
    #region Permanent Stats
    public int Health;
    public int Attack;
    public double Defense;              // Decimal between 0-1
    public double CritChance;           // Decimal between 0-1
    #endregion

    #region Temporary Scenario Stats
    public int ScenarioHealth;
    public int ScenarioAttack;
    public double ScenarioDefense;      // Decimal between 0-1
    public double ScenarioCritChance;   // Decimal between 0-1
    public bool ScenarioFled;
    public bool ScenarioTakeDoubleDamageNextRound;
    public bool ScenarioDeathWard;
    public bool ScenarioMustAttack;
    public bool ScenarioHasTaunted;
    public bool ScenarioIsHiding;
    #endregion

    #region Calculation Constants
    public const double Modest_Stat_Raise_Multiplier = 1.2;
    public const double Significant_Stat_Raise_Multiplier = 1.4;
    public const double Modest_Stat_Decrease_Multiplier = 0.8;
    public const double Significant_Stat_Decrease_Multiplier = 0.6;
    public const int Modest_Health_Raise_Value = 25;
    public const int Significant_Health_Raise_Value = 50;
    public const int Modest_Attack_Raise_Value = 3;
    public const int Attack_Change_Adjustment = 2;
    public const int Health_Change_Adjustment = 5;
    public const double Stat_Change_Adjustment = 0.1;
    #endregion

    public int[] Traits;

    public VikingStats(System.Random seed)
    {
        // init stats
        this.Health = seed.Next(80, 100);
        this.Attack = seed.Next(10, 20);
        this.Defense = seed.NextDouble();       // Decimal between 0-1
        this.CritChance = seed.NextDouble();    // Decimal between 0-1

        // init the traits array to all zeroes
        int numTraits = Enum.GetNames(typeof(VikingBoi.Traits)).Length;
        this.Traits = new int[numTraits];

        // give viking a starting trait
        this.Traits[(seed.Next(numTraits))] += 1;
    }

    public static double increasePercentageStat(double statToChange)
    {
        if (statToChange < 0.3) { statToChange = 0.5; }
        else { statToChange = Math.Atan(statToChange * (Math.PI / 2)); }
        return statToChange;
    }

    public static double decreasePercentageStat(double statToChange)
    {
        if (statToChange < 0.3) { statToChange = 0.5; }
        else { statToChange = (statToChange - (Math.Atan(statToChange * (Math.PI / 2)) - statToChange)); }
        return statToChange;
    }
}
