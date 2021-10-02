using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltipText;
    // Start is called before the first frame update
    void Start()
    {
        tooltipText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void GenerateTooltip(Viking viking)
    {
        string traits = "None";
        int[] traitlist = VikingManager.instance.VMMaster.Find(x=> x.ID == viking.masterId).stats.Traits;
        for(int i = 0; i < traitlist.Length; i++)
        {
            if (traitlist[i] > 0)
            {
                if(traits == "None")
                {
                    traits = ((VikingBoi.Traits)i).ToString();
                }     
                else
                {
                    traits += ", " + ((VikingBoi.Traits)i).ToString();
                }           
            }
        }
        string tooltip = "";
        tooltip += "Name: " + viking.name + "\n";
        tooltip += "Strength: " + viking.strength + "\n";
        tooltip += "Traits: " + traits;// + "\n";
        //add more stats and moves or whatever
        tooltipText.text = tooltip;
        gameObject.SetActive(true);
    }

}
