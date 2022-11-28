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
        context.charController.Move((context.target.transform.position - context.transform.position).normalized * context.chaseSpeed * Time.deltaTime);

        if (Vector3.Distance(context.transform.position, context.target.transform.position) >= context.agroRange * 2) context.ChangeState(context.patrolState);
    }
    public override void LeaveState(EnemyController context) {}
}
