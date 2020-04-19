using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingManager : MonoBehaviour
{
    public List<VikingBoi> VMMaster = new List<VikingBoi>();
    
    public SpriteGenerator spriteGenerator;
    public static VikingManager instance;
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
    
    void Update() 
    {
        if(Input.GetButtonDown("Jump"))
        {
            VikingBoi boi = gameObject.AddComponent(typeof(VikingBoi)) as VikingBoi;
            AddViking(boi);
        }
        if(Input.GetButtonDown("Horizontal"))
        {

        }
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
