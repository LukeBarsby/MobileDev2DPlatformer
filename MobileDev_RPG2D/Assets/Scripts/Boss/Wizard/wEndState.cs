using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wEndState : State
{
    public wEndState(Wizard wizard) : base(wizard)
    {

    }
    public override void Start()
    {
        Debug.Log("scream");
        Debug.Log("UI screen to exit manually or return to menu");
        Debug.Log("dead sprite");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void End()
    {
        base.End();
    }
}
