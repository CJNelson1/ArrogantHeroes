using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingList : MonoBehaviour
{
    public List<Viking> vikings = new List<Viking>();
    public List<Viking> missionVikings = new List<Viking>();
    public UIRoster vikintoryRoster;
    public UIRoster scenarioRoster;
    public GameObject firstBox;

    private void Start()
    {
        CreateList();

        //putting music stuff here cause I lack skill
        MusicController.instance.UpdateAudioSources();
        if(VikingManager.instance.first)
        {
            firstBox.SetActive(true);
            VikingManager.instance.first = false;
        }
        UpdateUIVikings();
    }
    public void UpdateUIVikings()
    {
        int i = 0;
        foreach(VikingBoi vb in VikingManager.instance.VMMaster)
        {
            if (vb == null) { continue; }
            if (vb.Alive)
            {
                if(vb.head != -1 && vb.body != -1 && vb.legs != -1)
                {
                    AddViking(new Viking(i, vb.vikingName, vb.stats.Attack, vb.stats.Health, vb.stats.Health, vb.ID, vb.head,vb.body,vb.legs), 0);
                }
                else
                {
                    Viking newBoi = new Viking(i, vb.vikingName, vb.stats.Attack, vb.stats.Health, vb.stats.Health, vb.ID);
                    AddViking(newBoi, 0);
                    vb.head = newBoi.head;
                    vb.body = newBoi.body;
                    vb.legs = newBoi.legs;
                }
                i++;
            }
        }
    }
    public Viking GetViking(int id)
    {
        return vikings.Find(viking => viking.id == id);
    }

    public Viking GetViking(string vikingName)
    {
        return vikings.Find(viking => viking.name == vikingName);
    }

    public void AddViking(Viking viking, int rosterIndex)
    {
        if(rosterIndex == 0)
        {
            vikings.Add(viking);
            vikintoryRoster.AddNewViking(viking);
            Debug.Log("Added viking: " + viking.name + " to vikintory " + viking.masterId);
        }
        else if (rosterIndex == 1)
        {
            missionVikings.Add(viking);
            scenarioRoster.AddNewViking(viking);
            Debug.Log("Added viking: " + viking.name + " to scenario");
        }

    }

    // public void RemoveViking(int id)
    // {
    //     Viking vikingToRemove = GetViking(id);
    //     if (vikingToRemove != null)
    //     {
    //         vikings.Remove(vikingToRemove);
    //         rosterUI.RemoveViking(vikingToRemove);
    //         Debug.Log("Removed Viking: " + vikingToRemove.name);
    //     }
    // }

    void CreateList()
    {
        vikings = new List<Viking>();
        missionVikings = new List<Viking>();
    }
    public void SetScenarioRoster(int i)
    {
        this.scenarioRoster = GameObject.Find("Area " + i.ToString()).GetComponent<UIRoster>();
    }
    public List<VikingBoi> GetScenarioVikings()
    {
        List<VikingBoi> returnList = new List<VikingBoi>();
        foreach(UIViking v in scenarioRoster.uiVikings)
        {
            if (v.viking != null)
            {
                returnList.Add(VikingManager.instance.VMMaster.Find(x=>x.ID == v.viking.masterId));
            }
        }
        return returnList;
    }
}
