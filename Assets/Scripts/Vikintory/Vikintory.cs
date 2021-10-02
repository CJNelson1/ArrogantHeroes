using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vikintory : MonoBehaviour
{
    public GameObject lilVikPrefab;
    void Start() 
    {
        for (int i = 0; i < VikingManager.instance.VMMaster.Count; i++)
        {
            string findby;
            if (i == 0)
            {
                findby = "Panel";
            }
            else
            {
                findby = string.Format("Panel ([0])", i.ToString());
            }
            // VikingBoi newGuy = VikingManager.instance.VMMaster[i];
            // Vector3 location = new Vector3(i*80, 300, -1);
            // location.z -= .2f;
            // GameObject sprite = Instantiate(lilVikPrefab, location, Quaternion.identity);
            // sprite.id = newGuy.ID;
        }
        
    }
}
