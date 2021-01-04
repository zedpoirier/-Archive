using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopController : MonoBehaviour
{
    public int currentPhase = 0;

    public Nail nail1;
    public Nail nail2;
    public Nail nail3;
    public Nail nail4;


    void Update()
    {
        if (currentPhase == 0) { }  // do nothing, waiting phase
        else if (currentPhase == 1) // Hammer the Nails
        {
            if (nail1.hammeredFully &&
                nail2.hammeredFully &&
                nail3.hammeredFully &&
                nail4.hammeredFully)
            {
                currentPhase = 2;
            }
        }
        else if (currentPhase == 2)
        {
            //GameStageManager.instance.NextStage();
        }
    }
}
