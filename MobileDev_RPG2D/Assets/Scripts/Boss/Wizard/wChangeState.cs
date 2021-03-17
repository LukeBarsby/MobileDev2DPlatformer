using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wChangeState : State
{
    public wChangeState(Wizard wizard) : base(wizard) { }
    int randomNum;


    public override void Start()
    {
        Random.InitState(System.Environment.TickCount);
        randomNum = Random.Range(0, 4);
    }

    public override void Update()
    {
        if (!wizardObj.returnedToSpawnPos)
        {
            wizardObj.agent.updateRotation = false;
            wizardObj.agent.SetDestination(wizardObj.bossSpawn.position);
        }
        else
        {
            End();
        }

    }

    public override void End()
    {
        switch (randomNum)
        {
            case 0:
                wizardObj.SetState(new wRushAttackState(wizardObj));
                break;
            case 1:
                wizardObj.SetState(new wFireBallState(wizardObj));
                break;
            case 2:
                wizardObj.SetState(new wGreenLaserState(wizardObj));
                break;
            case 3:
                wizardObj.SetState(new wGreenLaserHorizontal(wizardObj));
                break;


            default:
                Debug.LogError("Too much Range");
                break;
        }
    }
}
