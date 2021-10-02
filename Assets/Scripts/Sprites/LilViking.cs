using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilViking : MonoBehaviour
{
    public int head;
    public int body;
    public int legs;
    public string id;
    public SpriteGenerator spriteGenerator;

    // Start is called before the first frame update
    void Start()
    {
        // this.head = spriteGenerator.AssignHead();
        // this.body = spriteGenerator.AssignBody();
        // this.legs = spriteGenerator.AssignLegs();
        // this.transform.Find("head").GetComponent<SpriteRenderer>().sprite = spriteGenerator.GetHead(this.head);
        // this.transform.Find("body").GetComponent<SpriteRenderer>().sprite = spriteGenerator.GetBody(this.body);
        // this.transform.Find("legs").GetComponent<SpriteRenderer>().sprite = spriteGenerator.GetLegs(this.legs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
