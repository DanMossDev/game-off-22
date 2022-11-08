using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRelease : BossState
{
    Vector3 target;
    float lerp;
    public override void EnterState(BossController context) 
    {
        //Set an animation
        target = context.player.transform.position;
        lerp = 0;
    }
    public override void UpdateState(BossController context) 
    {
        lerp += Time.deltaTime * 2;
        for (int i = 0; i < context.Spheres.Length; i++)
        {
            context.Spheres[i].transform.position = Vector3.Lerp(context.head.transform.position, target, (lerp - (i * 0.105f)));
        }

        if (lerp >= 1) context.BecomeExhausted();
    }
    public override void LeaveState(BossController context)
    {
        foreach (GameObject sphere in context.Spheres)
        {
            sphere.GetComponent<BossTarget>().BecomeTargettable();
        }
    }
}
