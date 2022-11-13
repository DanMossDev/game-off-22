using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : BossState
{
    float timeEntered;
    bool spheresInPlace;
    float lerp;
    public override void EnterState(BossController context) 
    {
        timeEntered = Time.time;
        spheresInPlace = false;
        lerp = 0;
        foreach (GameObject sphere in context.Spheres)
        {
            sphere.GetComponent<BossTarget>().BecomeUntargettable();
        }
        //Make spheres go into a ring above their head and begin an animation

        //TYLER
        context.animator.SetTrigger("charge");

    }
    public override void UpdateState(BossController context) 
    {
        context.LookAtPlayer();

        if (!spheresInPlace)
        {
            if (lerp < 1)
            {
                lerp += Time.deltaTime;
                foreach (GameObject sphere in context.Spheres)
                {
                    sphere.transform.localPosition = Vector3.Lerp(sphere.transform.localPosition, new Vector3(sphere.transform.localPosition.x, 35, sphere.transform.localPosition.z), lerp);
                }
            } else
            {
                foreach (GameObject sphere in context.Spheres)
                {
                    sphere.transform.localPosition = new Vector3(sphere.transform.localPosition.x, 35, sphere.transform.localPosition.z);
                }
                spheresInPlace = true;
            }
        }
        context.SphereHolder.transform.Rotate(0, 720 * Time.deltaTime, 0);
        if (Time.time - timeEntered >= context.chargeTime) context.BeginAttack();
    }
    public override void LeaveState(BossController context) 
    {
        context.SphereHolder.transform.localRotation = Quaternion.identity;
    }
}
