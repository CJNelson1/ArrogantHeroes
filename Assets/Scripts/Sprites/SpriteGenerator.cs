using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    public Sprite[] SpHeads;
    public Sprite[] SpBodies;
    public Sprite[] SpLegs;
    System.Random seed = new System.Random();
    public Sprite GetHead(int i)
    {
        return SpHeads[i];
    }

    public Sprite GetBody(int i)
    {
        return SpBodies[i];
    }

    public Sprite GetLegs(int i)
    {
        return SpLegs[i];
    }
    public int AssignHead()
    {
        return seed.Next(SpHeads.Length);
    }
    public int AssignBody()
    {
        return seed.Next(SpBodies.Length);
    }
    public int AssignLegs()
    {
        return seed.Next(SpLegs.Length);
    }
}
