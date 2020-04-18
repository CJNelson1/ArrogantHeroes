public class VikingStats
{
    #region Stats
    public double health;
    public double attack;
    public double defense;
    public double Wisdom;
    public double CritChance;
    #endregion

    #region Traits
    public bool Teamwork;
    #endregion

    public VikingStats(System.Random seed)
    {
        int baseMax = 10;
        
        // init stats
        this.health = seed.NextDouble() * baseMax;
        this.attack = seed.NextDouble() * baseMax;
        this.defense = seed.NextDouble() * baseMax;

        // does a viking start with traits? if so, give them one
    }
}
