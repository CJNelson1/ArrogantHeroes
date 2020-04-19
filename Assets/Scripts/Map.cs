using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Dictionary<string, Location> map;
    public int dayNumber = 0;
    public Location SelectedLocation;

    public static Map instance;

    public void AdvanceDay()
    {
        // level up the bois from last mission
        foreach (VikingBoi boi in VikingManager.instance.VMMaster) 
        {
           if (boi.SelectedForScenario)
           {
               boi.LevelUpViking(SelectedLocation.scenarioLevel);
           }
        }

        // assign new scenarios to map locations?
        foreach (Location location in map.Values)
        {
            location.GibScenario(dayNumber);
        }

        // get more bois?

        dayNumber++;
    }

    void Awake() 
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        this.map = new Dictionary<string, Location>();
        AdvanceDay();
    }

    void Update() 
    {
        
    }
}