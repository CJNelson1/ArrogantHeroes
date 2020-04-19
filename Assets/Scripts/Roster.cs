using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Roster : MonoBehaviour 
{
    public Dictionary<string,VikingBoi> roster;

    public Roster() 
    {
        this.roster = new Dictionary<string,VikingBoi>();
    } 

    /// <summary>
    /// Add viking with the given ID to the roster.
    /// </summary>
    /// <param name="newViking">Viking to add to the roster</param>
    public void AddViking(VikingBoi newViking)
    {
        this.roster.Add(newViking.ID, newViking);
    }

    /// <summary>
    /// Remove viking with the given ID from the roster.
    /// </summary>
    /// <param name="vikingID">ID of a viking to remove</param>
    /// <returns>True if a viking was removed, false otherwise</returns>
    public bool RemoveViking(string vikingID)
    {
        return this.roster.Remove(vikingID);
    }

    /// <summary>
    /// The vikings selected for a scenario indexed by ID.
    /// </summary>
    /// <returns>Dictionary of vikings selected for a scenario indexed by ID</returns>
    public Dictionary<string, VikingBoi> SelectedVikings()
    {
        Dictionary<string, VikingBoi> SelectedVikings = new Dictionary<string, VikingBoi>();
        foreach (VikingBoi boi in this.roster.Values)
        {
            if (boi.SelectedForScenario)
            {
                SelectedVikings.Add(boi.ID, boi);
            }
        }
        return SelectedVikings;
    }
}