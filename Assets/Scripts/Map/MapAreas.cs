using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapAreas : MonoBehaviour
{
    public VikingList vikingList;
    public int activeArea;
    public int difficulty;
    private System.Random r;
    void Awake() 
    {
        r = new System.Random();
        List<VikingBoi> aliveVikings = new List<VikingBoi>();
        foreach (VikingBoi v in VikingManager.instance.VMMaster)
        {
            if (v.Alive)
            {
                aliveVikings.Add(v);
            }
        }
        if (aliveVikings.Count == 0) //Make it so you can't lose by running out of vikings
        {
            VikingManager.instance.AddViking();
            VikingManager.instance.AddViking();
            VikingManager.instance.AddViking();
        }
        GenerateScenarios();
        VikingManager.instance.CleanList();
    }
    public void GenerateScenarios()
    {
        int index = r.Next(1,9);
        for(int i = 1; i <= 9; i++)
        {
            try
            {
                if (i == index)
                {
                    transform.Find("Map Area " + i.ToString()).gameObject.SetActive(true);
                    vikingList.SetScenarioRoster(i);
                    activeArea = i;
                }
                else
                {
                    transform.Find("Map Area " + i.ToString()).gameObject.SetActive(false);
                }
            }
            catch (Exception e)
            {
                Debug.Log(i.ToString() + " " + e.Message);
            }
        }
    }
    public void UpdateScenarioLevel(float input)
    {
        string[] difficultyText = new string[]{"Baby Viking", "Halfdan", "True Viking", "Ragnarok", "World Eater Snake"};
        difficulty = (int)input;
        GameObject.Find("Difficulty Text").GetComponent<Text>().text = difficultyText[difficulty-1];
        ScenarioController.instance.scenarioLevel = difficulty;
    }
    public void ScenarioStart()
    {
        SceneManager.LoadScene("Scenario");
        MusicController.instance.PlaySFX("Horns/anxhorn");  //I'm an sfx
    }
}
