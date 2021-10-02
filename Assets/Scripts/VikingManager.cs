using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingManager : MonoBehaviour
{
    public List<VikingBoi> VMMaster = new List<VikingBoi>();
    
    public SpriteGenerator spriteGenerator;
    public static VikingManager instance;
    public System.Random seed;
    public bool first;
    void Awake() 
    {
        if(!instance)
        {
            instance = this;
            seed = new System.Random();
            first = true;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void CleanList()
    {   
        if (this.VMMaster.Find(x=>x.Alive == true) == null)
        {
            VikingManager.instance.AddViking();
            VikingManager.instance.AddViking();
            VikingManager.instance.AddViking();
        }
        foreach(VikingBoi v in this.VMMaster)
        {
            v.SelectedForScenario = false;
        }
    }
    public void AddViking()
    {
        VikingBoi boi = gameObject.AddComponent(typeof(VikingBoi)) as VikingBoi;
        AddViking(boi);
    }
    public void AddViking(VikingBoi newBoi)
    {
        VMMaster.Add(newBoi);
    }
    public VikingBoi GetBoi(int index)
    {
        return VMMaster[index];
    }
    public List<VikingBoi> GetAllBois()
    {
        return VMMaster;
    }

}
