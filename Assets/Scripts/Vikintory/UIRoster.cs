using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoster : MonoBehaviour
{
    public List<UIViking> uiVikings = new List<UIViking>();
    public int rosterSize;
    public bool missionSelect;
    public GameObject slotPrefab;
    public Transform rosterSlot;

    private void Awake()
    {
        for(int i = 0; i < rosterSize; i++)
        {
            GameObject inst = Instantiate(slotPrefab);
            inst.transform.SetParent(rosterSlot);
            uiVikings.Add(inst.GetComponentInChildren<UIViking>());
        }
    }

    public void UpdateSlot(int slot, Viking viking)
    {
        uiVikings[slot].UpdateSlot(viking);
    }

    public void AddNewViking(Viking viking)
    {
        UpdateSlot(uiVikings.FindIndex(i => i.viking == null), viking);
    }

    public void RemoveViking(Viking viking)
    {
        UpdateSlot(uiVikings.FindIndex(i => i.viking == viking), null);
    }
}
