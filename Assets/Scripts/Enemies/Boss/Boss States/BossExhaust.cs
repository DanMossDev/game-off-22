using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExhaust : BossState
{
    float timeEntered;
    public override void EnterState(BossController context) 
    {
        // play animation
        context.damagedThisCycle = false;
        timeEntered = Time.time;
    }
    public override void UpdateState(BossController context) 
    {
        if (Time.time - timeEntered >= 10 || context.damagedThisCycle) context.ChangeState();
    }
    public override void LeaveState(BossController context) 
    {
        context.restoreSpheres();
    }
}
