using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VikingBoi : MonoBehaviour
{
    public string vikingName;
    public int head = -1;
    public int body = -1;
    public int legs = -1;
    public System.Random seed;
    public VikingStats stats;
    public string ID;
    public bool SelectedForScenario;
    public bool Alive = true;

    void Awake()
    {        
        this.seed = VikingManager.instance.seed;
        this.ID = Guid.NewGuid().ToString();

        // build name from random gen
        this.vikingName = NameGenerator.GenerateName(seed);

        // roll stats
        this.stats = new VikingStats(seed);
    }

    /// <summary>
    /// Select one action for this viking to take.
    /// </summary>
    /// <param name="inGroup">Whether this viking is in a group (and can therefore perform group actions)</param>
    /// <param name="firstAction">Whether this is the first action of the scenario</param>
    public VikingBoi.Actions SelectAction(bool inGroup, bool firstAction)
    {
        return ActionHelper.SelectAction(this.seed, this.stats, inGroup, firstAction, false);
    }

    public void LevelUpViking(int lastScenarioLevel)
    {
        if (this.Alive && !this.stats.ScenarioFled)
        {
            TraitAssignment.AssignNewTrait(this);       // Assign new trait first since that will influence stats
            TraitAssignment.IncreaseStats(this, lastScenarioLevel);
        }
    }

    public bool Equals(VikingBoi other)
    {
        if (other == null)
        {
            return false;
        }

        return (this.ID == other.ID);
    }

    public enum Actions
    {
        Attack,
        Defend,
        Insult,
        Drink,
        Cower,
        Flee,
        FerociousAttack,
        MockingAttack,
        PlayTheVillain,
        RecklessAttack,
        Plunder,
        DedicateToOdin,
        PrayToTheGods,
        GoBerserk,
        Brag,
        BerserkerRage,      // Group action
        GiveASpeech,        // Group action
        Taunt,              // Group action
        HideInTheBack,      // Group action
        StealAllTheGlory,   // Group action
        GangUp              // Group action
    }

    public enum Traits
    {
        Bloodthirsty,
        Greedy,
        Haughty,
        Overconfident,
        Religious,
        Drunkard,
        Cautious,
        Coward,
        Frenzied,
        Suicidal,
        Follower,
        Blowhard,
        Anarchist,
        Jerk,
        Christian,
        PartyAnimal,
        Antagonizer,
        Contrarian,
        Superstitious,
        Violent,
        Performer
    }
}
