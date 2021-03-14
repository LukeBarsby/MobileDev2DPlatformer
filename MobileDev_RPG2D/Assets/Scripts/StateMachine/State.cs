using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Wizard wizardObj;

    public State(Wizard wizard)
    {
        wizardObj = wizard;
    }
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }
    public virtual void End()
    {

    }
}
