public class VikingStats
{
    #region Stats
    public double Health;
    public double Attack;
    public double Defense;
    public double Wisdom;
    public double CritChance;
    #endregion

    public int[] Traits;

    public VikingStats(System.Random seed)
    {
        int baseMax = 10;
        
        // init stats
        this.Health = seed.NextDouble() * baseMax;
        this.Attack = seed.NextDouble() * baseMax;
        this.Defense = seed.NextDouble() * baseMax;

        // init the traits array to all zeroes
        int numTraits = Enum.GetNames(typeof(VikingBoi.Traits)).Length;
        this.traits = new int[numTraits];

        // does a viking start with traits? if so, give them one
        this.traits[(seed.Next(numTraits))] += 1;
    }

}
