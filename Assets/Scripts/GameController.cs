using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //testActionSelection(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void testActionSelection(int howManyTimes)
    {
        for (int i = 0; i < howManyTimes; i++)
        {
            ActionHelper.TestSelectAction();
        }
    }
}
