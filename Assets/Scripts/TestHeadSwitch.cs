using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeadSwitch : MonoBehaviour
{

    public Sprite headOne;
    public Sprite headTwo;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.Find("Head").GetComponent<SpriteRenderer>().sprite = headOne;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            this.transform.Find("Head").GetComponent<SpriteRenderer>().sprite = headTwo;
        }
    }
}
