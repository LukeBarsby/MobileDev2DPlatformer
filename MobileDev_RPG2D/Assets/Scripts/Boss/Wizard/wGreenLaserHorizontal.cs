using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wGreenLaserHorizontal : State
{
    public wGreenLaserHorizontal(Wizard wizard) : base(wizard)
    {

    }

    float stateTimer = 6;
    float gTimer;
    int itemToRemove;
    int randNum;

    public override void Start()
    {

    }

    public override void Update()
    {
        GreenLazerHorizontal();
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            End();
        }
    }

    void GreenLazerHorizontal()
    {
        if (gTimer <= 0)
        {
            wizardObj.GreenLaserLocations[itemToRemove].SetActive(false);
            randNum = Random.Range(0, wizardObj.GreenLaserLocations.Length);
            wizardObj.GreenLaserLocations[randNum].SetActive(true);
            itemToRemove = randNum;
            gTimer = 0;
            gTimer = 2;

        }

        gTimer -= Time.deltaTime;
    }

    public override void End()
    {
        for (int i = 0; i < wizardObj.GreenLaserLocations.Length; i++)
        {
            wizardObj.GreenLaserLocations[i].SetActive(false);
        }
        wizardObj.SetState(new wChangeState(wizardObj));
    }
}
