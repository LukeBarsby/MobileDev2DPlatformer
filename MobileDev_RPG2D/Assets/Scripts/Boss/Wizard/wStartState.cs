using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wStartState : State
{
    public wStartState(Wizard wizard) : base(wizard){}
    public override void Start()
    {
        Debug.Log("StartState");
    }

    public override void Update()
    {
        Debug.Log("StartState Update");
    }

    public override void End()
    {
        
    }
}
