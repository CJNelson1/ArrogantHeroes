using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    public Sprite[] SpHeads;
    public Sprite[] SpBodies;
    public Sprite[] SpLegs;
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
        return VikingManager.instance.seed.Next(SpHeads.Length);
    }
    public int AssignBody()
    {
        return VikingManager.instance.seed.Next(SpBodies.Length);
    }
    public int AssignLegs()
    {
        return VikingManager.instance.seed.Next(SpLegs.Length);
    }
}
