using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroState : EnemyState
{
    public override void EnterState(EnemyController context) 
    {
        context.anim.SetBool("isAggro", true);
    }
    public override void UpdateState(EnemyController context) 
    {
        Rotate(context);
        context.charController.Move(new Vector3(context.target.transform.position.x - context.transform.position.x, 0, context.target.transform.position.z - context.transform.position.z).normalized * context.chaseSpeed * Time.deltaTime);

        if (Vector3.Distance(context.transform.position, context.target.transform.position) >= context.agroRange * 2) context.ChangeState(context.patrolState);
    }
    public override void LeaveState(EnemyController context) {}


    void Rotate(EnemyController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.target.transform.position.x - context.transform.position.x, 0, context.target.transform.position.z - context.transform.position.z), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, 360 * Time.deltaTime);
    }
}
