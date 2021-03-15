using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wGreenLaserState : State
{
    public wGreenLaserState(Wizard wizard) : base(wizard)
    {

    }

    Quaternion q;
    float laserTime = 6;

    public override void Start()
    {
        
        wizardObj.GreenLaserSpin.SetActive(true);
    }

    public override void Update()
    {
        GreenLazerStateSpin();
        laserTime -= Time.deltaTime;
        if (laserTime <= 0)
        {
            End();
        }
    }

    void GreenLazerStateSpin()
    {
        Vector3 direction = wizardObj.transform.position - PlayerController.Instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        q = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        wizardObj.GreenLaserSpin.transform.rotation = Quaternion.Lerp(wizardObj.GreenLaserSpin.transform.rotation, q, 2.5f * Time.deltaTime);

    }

    public override void End()
    {
        wizardObj.GreenLaserSpin.SetActive(false);
        wizardObj.SetState(new wRandomMove(wizardObj));
    }
}
