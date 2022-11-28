using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    int nextPosition = 0;
    public override void EnterState(EnemyController context) 
    {
        
        context.anim.SetBool("isAggro", false);
        if (context.patrolPoints.Length > 1)
        {
            nextPosition = 0;
        }

    }
    public override void UpdateState(EnemyController context) 
    {
        if (Vector3.Distance(context.transform.position, context.target.transform.position) <= context.agroRange)
        {
            switch (context.behaviour)
            {
                case EnemyBehaviour.Aggressive:
                    context.ChangeState(context.agroState);
                    return;
                case EnemyBehaviour.Fearful:
                    context.ChangeState(context.fearState);
                    return;
                case EnemyBehaviour.Distant:
                    context.ChangeState(context.distState);
                    return;
                default:
                    break;
            }
        }
        if (context.patrolPoints.Length <= 1) return;

        if (Vector3.Distance(context.transform.position, context.patrolPoints[nextPosition].position) < 0.1f)
        {
            nextPosition = nextPosition == context.patrolPoints.Length - 1 ? 0 : nextPosition + 1;
        }
        context.charController.Move((context.patrolPoints[nextPosition].position - context.transform.position).normalized * context.patrolSpeed * Time.deltaTime);
    }
    public override void LeaveState(EnemyController context) 
    {

    }
}
