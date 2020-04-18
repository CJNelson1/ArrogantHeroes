using System;
using System.Collections.Generic;

public class Roster 
{
    private Dictionary<string,VikingBoi> roster;

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
        return this.roster.Remove(newViking.ID);
    }

    /// <summary>
    /// The roster of vikings indexed by ID.
    /// </summary>
    /// <returns>Dictionary of vikings indexed by ID</returns>
    public ReadOnlyDictionary<string,VikingBoi> Vikings()
    {
        return new ReadOnlyDictionary<string, VikingBoi>(this.roster);
    }
}