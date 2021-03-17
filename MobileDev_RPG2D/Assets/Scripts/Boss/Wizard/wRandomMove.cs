using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wRandomMove : State
{
    public wRandomMove(Wizard wizard) : base(wizard) { }

    float timer;
    float switchTimer;
    Vector3 goal;

    public override void Start()
    {
        switchTimer = 5;
    }

    public override void Update()
    {
        RandomMove();
    }

    public void RandomMove()
    {
        if (timer <= 0)
        {
            Vector3 randomDir = Random.insideUnitSphere * wizardObj._idleMaxDistnce;
            randomDir += wizardObj.transform.position;

            NavMeshHit hit;
            // -1 = all layers
            if (NavMesh.SamplePosition(randomDir, out hit, wizardObj._idleMaxDistnce, NavMesh.AllAreas))
            {
                goal = hit.position;
            }

            timer = 0;
            timer = 3;
        }
        if (switchTimer <= 0)
        {
            End();
        }
        timer -= Time.deltaTime;
        switchTimer -= Time.deltaTime;
        wizardObj.agent.updateRotation = false;
        wizardObj.agent.destination = goal;
    }

    public override void End()
    {
        wizardObj.SetState(new wChangeState(wizardObj));
    }
}