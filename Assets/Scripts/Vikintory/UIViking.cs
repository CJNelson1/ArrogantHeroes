using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIViking : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Viking viking;
    private Image head;
    private Image body;
    private Image legs;
    private UIViking selectedViking;
    private Tooltip tooltip;

    private void Awake()
    {
        selectedViking = GameObject.Find("SelectedViking").GetComponent<UIViking>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        head = transform.Find("head").GetComponent<Image>();
        body = transform.Find("body").GetComponent<Image>();
        legs = transform.Find("legs").GetComponent<Image>();
        UpdateSlot(null);
    }

    public void UpdateSlot(Viking viking)
    {
        this.viking = viking;
        Image slotBox = GetComponentInParent<Image>();
        Color setColor = new Color(173,173,173,255);
        if(this.viking != null)
        {
            head.color = Color.white;
            head.sprite = VikingManager.instance.spriteGenerator.GetHead(viking.head);
            body.color = Color.white;
            body.sprite = VikingManager.instance.spriteGenerator.GetBody(viking.body);
            legs.color = Color.white;
            legs.sprite = VikingManager.instance.spriteGenerator.GetLegs(viking.legs);
            legs.color = Color.white;
            transform.Find("tootsies").GetComponent<Image>().color = Color.white;
            setColor.a = 30;
        }
        else
        {
            head.color = Color.clear;
            body.color = Color.clear;
            legs.color = Color.clear;
            transform.Find("tootsies").GetComponent<Image>().color = Color.clear;
            setColor.a = 60;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(this.viking != null)
        {
            if(selectedViking.viking != null)
            {
                /*Viking clone = new Viking(selectedViking.viking);
                selectedViking.UpdateSlot(this.viking);
                UpdateSlot(clone);*/
            }
            else
            {
                selectedViking.UpdateSlot(this.viking);
                UpdateSlot(null);
                OnPointerExit(eventData);  //this makes the tooltip disappear properly after grabbing an object
                
            }
        }
        else
        {
            if(selectedViking.viking != null)
            {
                bool missionSelect = false;
                if (transform.parent.name == "Scenario Slot(Clone)") missionSelect = true;
                Debug.Log("Updated viking: " + selectedViking.viking.name + " to vikintory " + selectedViking.viking.masterId + " set to mission " + missionSelect.ToString());
                VikingManager.instance.VMMaster.Find(x=>x.ID == selectedViking.viking.masterId).SelectedForScenario = missionSelect;
                UpdateSlot(selectedViking.viking);
                selectedViking.UpdateSlot(null);
                OnPointerEnter(eventData);  //this allows the tooltip to show up after placing an object
                MusicController.instance.PlaySFX("Slice");                                                          //i'm an sfx
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.viking != null)
        {
            tooltip.GenerateTooltip(this.viking);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
