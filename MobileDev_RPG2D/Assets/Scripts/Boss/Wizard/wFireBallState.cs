using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wFireBallState : State
{
    public wFireBallState(Wizard wizard) : base(wizard)
    {

    }

    List<GameObject> firebllQueue = new List<GameObject>();
    bool fireballsSpawned = false;
    public override void Start()
    {
        wizardObj.StartCoroutine(SpawnFireBalls());
    }

    public override void Update()
    {
        FireBallState();
    }
    void FireBallState()
    {
        if (fireballsSpawned)
        {
            wizardObj.StartCoroutine(ShootFireBalls());
        }
    }

    IEnumerator SpawnFireBalls()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < wizardObj.fireBallLocations.Length; i++)
        {
            GameObject firebll = ObjectPooler.Instance.SpawnFromPool("Fireball", wizardObj.fireBallLocations[i].transform.position, Quaternion.identity);
            firebllQueue.Add(firebll);
            yield return new WaitForSeconds(.5f);
        }
        fireballsSpawned = true;
    }
    IEnumerator ShootFireBalls()
    {
        for (int i = 0; i < firebllQueue.Count; i++)
        {
            firebllQueue[i].GetComponent<ProjectileController>().ShootFireBall();
            if (i == firebllQueue.Count - 1)
            {
                End();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public override void End()
    {
        wizardObj.SetState(new wRandomMove(wizardObj));
    }
}
