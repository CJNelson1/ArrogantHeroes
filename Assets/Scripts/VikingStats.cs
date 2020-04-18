public class VikingStats
{
    private double health;
    private double attack;
    private double defense;

    /// <summary>
    /// Viking Stats Constructor
    /// </summary>
    /// <param name="seed">Random object for stat generation</param>
    public VikingStats(System.Random seed)
    {
        // initialize stats between 1 and 10
        this.health = seed.NextDouble() * 10;
        this.attack = seed.NextDouble() * 10;
        this.defense = seed.NextDouble() * 10;
    }

    public double Health()
    {
        return health;
    }

    public double Attack()
    {
        return attack;
    }

    public double Defense()
    {
        return defense;
    }

}
