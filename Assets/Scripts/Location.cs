using System;
using UnityEngine;

public class Location : MonoBehaviour  
{
    public bool hasScenario;
    public int scenarioLevel;
    public Scenario scenario;

    void Start() 
    {
        
    }

    void Update() 
    {
        
    }

    public void GibScenario(int dayNumber)
    {
        System.Random seed = new System.Random();
        scenarioLevel = seed.Next(0, (1 + dayNumber));
        if (scenarioLevel == 0)
        {
            hasScenario = false;
        }
        else 
        {
            hasScenario = true;
            this.scenario = new Scenario();
        }
    }

}
