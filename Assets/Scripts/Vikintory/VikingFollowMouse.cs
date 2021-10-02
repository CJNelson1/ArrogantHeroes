using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingFollowMouse : MonoBehaviour
{
    
    void LateUpdate()
    {
        float offset = 0;
        if(GameObject.Find("Tooltip") != null)
        {
            if (GameObject.Find("Tooltip").activeSelf)
            {
                offset = GameObject.Find("Tooltip").GetComponent<RectTransform>().rect.height * 2.5f;
            }
        }
        transform.position = Input.mousePosition - new Vector3(0,offset,0);
    }
}
