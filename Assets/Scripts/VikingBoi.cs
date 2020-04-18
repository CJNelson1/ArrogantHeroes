using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VikingBoi : MonoBehaviour
{
    public string Name;
    private System.Random seed;
    private VikingStats stats;

    // Start is called before the first frame update
    void Start()
    {
        this.seed = new System.Random();

        // build name from random gen
        this.Name = NameGenerator.GenerateName(seed);

        // roll stats
        this.stats = new VikingStats(seed);
    }

    // Update is called once per frame
    void Update()
    {
        // if they make multiple animates
    }
}
