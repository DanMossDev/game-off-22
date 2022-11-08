using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpin : BossState
{
    float timeEntered;
    public override void EnterState(BossController context) 
    {
        timeEntered = Time.time;
        foreach (GameObject sphere in context.Spheres)
        {
            sphere.GetComponent<BossTarget>().BecomeUntargettable();
        }
    }
    public override void UpdateState(BossController context) 
    {
        if (Time.time - timeEntered > 8) context.ChangeState();
        context.SphereHolder.transform.Rotate(0, 720 * Time.deltaTime, 0);
        context.LookAtPlayer();
        Move(context);
    }
    public override void LeaveState(BossController context) 
    {
        context.SphereHolder.transform.localRotation = Quaternion.identity;
    }

    void Move(BossController context)
    {
        Vector3 direction = new Vector3(context.player.transform.position.x - context.transform.position.x, 0, context.player.transform.position.z - context.transform.position.z);
        context.transform.position += direction * Time.deltaTime * context.rushSpeed;
    }
}
