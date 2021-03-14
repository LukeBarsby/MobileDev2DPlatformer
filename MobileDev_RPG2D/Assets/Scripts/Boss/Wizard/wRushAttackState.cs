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
        Debug.Log("wRushAttackState");
    }

    public override void Update()
    {
        Debug.Log("wRushAttackState Update");
    }

    public override void End()
    {
        base.End();
    }
}
