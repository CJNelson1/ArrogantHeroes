using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VikingBoi : MonoBehaviour
{
    public readonly string name;
    public readonly System.Random seed;
    private VikingStats stats;
    public readonly string id;

    public VikingBoi()
    {
        this.seed = new System.Random();
        this.id = Guid.NewGuid().ToString();

        // build name from random gen
        this.name = NameGenerator.GenerateName(seed);

        // roll stats
        this.stats = new VikingStats(seed);
    }

    public void SelectAction()
    {
        // init the traits array to all zeroes
        int potentialActions = new int[Enum.GetNames(typeof(VikingBoi.Actions)).Length];

        /* Bloodthirsty */
        if (this.stats.traits[Traits.Bloodthirsty])
        {
            // manipulate numbers
        }

        /* Greedy */
        if (this.stats.Traits[Traits.Greedy])
        {
            // manipulate numbers
        }

        /* Haughty */
        if (this.stats.Traits[Traits.Haughty])
        {
            // manipulate numbers
        }

        // etc.
    }

    public void LevelUpViking()
    {
        // ???
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // every X frames, change animation, move body parts a little, change spites as necessary
    }

    public enum Actions
    {
        Attack,
        Flee
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
        Coward
    }
}
