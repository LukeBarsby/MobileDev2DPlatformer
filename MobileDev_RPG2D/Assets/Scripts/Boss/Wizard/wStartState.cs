using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wStartState : State
{
    public wStartState(Wizard wizard) : base(wizard){}
    public override void Start()
    {
        wizardObj.SetState(new wChangeState(wizardObj));
    }

    public override void Update()
    {
    }

    public override void End()
    {
        
    }
}
