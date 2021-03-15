using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wRushAttackState : State
{
    public wRushAttackState(Wizard wizard) : base(wizard)
    {

    }

    public override void Start()
    {
        //System.Array.Reverse(wizardObj.rushPoints);
        wizardObj.transform.gameObject.tag = "BossRush";
        wizardObj.rushPointCounter = 0;
    }

    public override void Update()
    {
        RushAttackSate();
    }

    void RushAttackSate()
    {
        wizardObj.hitCollider.enabled = false;
        for (int i = 0; i < wizardObj.rushPoints.Length; i++)
        {
            if (wizardObj.rushPointCounter == i)
            {
                wizardObj.transform.position = Vector2.MoveTowards(wizardObj.transform.position, wizardObj.rushPoints[i].transform.position, wizardObj.rushSpeed * Time.deltaTime);
            }
            if (wizardObj.rushPointCounter == wizardObj.rushPoints.Length)
            {
                End();
            }
        }

    }

    public override void End()
    {
        wizardObj.transform.gameObject.tag = "Enemy";
        wizardObj.hitCollider.enabled = true;
        wizardObj.SetState(new wRandomMove(wizardObj));
    }
}
