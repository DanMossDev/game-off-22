using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : BossState
{
    float timeEntered;

    public override void EnterState(BossController context) 
    {
        timeEntered = Time.time;
        foreach (GameObject sphere in context.Spheres)
        {
            sphere.GetComponent<BossTarget>().BecomeTargettable();
        }
    }
    public override void UpdateState(BossController context) 
    {
        if (Time.time - timeEntered > 12) context.ChangeState();

        context.LookAtPlayer();
    }
    public override void LeaveState(BossController context) 
    {
    }
}
