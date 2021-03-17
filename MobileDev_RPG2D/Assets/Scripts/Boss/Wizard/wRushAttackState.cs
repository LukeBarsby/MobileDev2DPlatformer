using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wRushAttackState : State
{
    public wRushAttackState(Wizard wizard) : base(wizard)
    {

    }
    float normalSpeed;
    float rushSpeed;
    float normalAccelearation;
    float rushAccelearation;
    public override void Start()
    {
        //System.Array.Reverse(wizardObj.rushPoints);
        wizardObj.transform.gameObject.tag = "BossRush";
        wizardObj.rushPointCounter = 0;

        normalSpeed = wizardObj.agent.speed;
        rushSpeed = wizardObj.rushSpeed;

        normalAccelearation = wizardObj.agent.acceleration;
        rushAccelearation = wizardObj.rushAcceleration;

        wizardObj.agent.speed = rushSpeed;
        wizardObj.agent.acceleration = rushAccelearation;
        wizardObj.hitCollider.isTrigger = true;
    }

    public override void Update()
    {
        RushAttackSate();
    }

    void RushAttackSate()
    {
        for (int i = 0; i < wizardObj.rushPoints.Length; i++)
        {
            if (wizardObj.rushPointCounter == i)
            {
                wizardObj.agent.destination = wizardObj.rushPoints[i].transform.position;
            }
            if (wizardObj.rushPointCounter == wizardObj.rushPoints.Length)
            {
                End();
            }
        }

    }

    public override void End()
    {
        wizardObj.hitCollider.isTrigger = false;
        wizardObj.agent.speed = normalSpeed;
        wizardObj.agent.acceleration = normalAccelearation;
        wizardObj.agent.velocity = Vector3.zero;

        wizardObj.transform.gameObject.tag = "Enemy";
        wizardObj.hitCollider.enabled = true;
        wizardObj.SetState(new wRandomMove(wizardObj));
    }
}
