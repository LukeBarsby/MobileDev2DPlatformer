using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class wEndState : State
{
    public wEndState(Wizard wizard) : base(wizard)
    {

    }
    public override void Start()
    {
        AnalyticsResult result = Analytics.CustomEvent("Level 1 beat");
        Debug.Log("Result = " + result);

        wizardObj.ls.EnableExit();
        PlayerController.Instance.LeaveLevel();
        wizardObj.gameObject.SetActive(false);
    }

    public override void Update()
    {
        
    }

    public override void End()
    {
        base.End();
    }
}
