using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioController : MonoBehaviour
{
    public static ScenarioController instance;
    public int scenarioLevel;

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
        scenarioLevel = 1;
    }
    
    public string CreateScenarioLevelText(int input)
    {
        return "";
    }
}
