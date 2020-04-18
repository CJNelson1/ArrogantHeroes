using System;
using System.Collections.Generic;

public class Map 
{
    private Dictionary<string,Location> map;

    public Map() 
    {
        this.map = new Dictionary<string,Location>();
    } 

    /// <summary>
    /// The list of locations indexed by ID.
    /// </summary>
    /// <returns>Dictionary of locations indexed by ID</returns>
    public ReadOnlyDictionary<string,Location> Vikings()
    {
        return new ReadOnlyDictionary<string,Location>(this.roster);
    }
}