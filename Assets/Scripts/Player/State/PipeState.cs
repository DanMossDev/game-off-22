using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeState : PlayerState
{
    public override void EnterState(PlayerController context) 
    {
        context.rigidBody.isKinematic = true;
        context.animator.SetBool("isGrounded", true);
        context.animator.SetFloat("ySpeed", 0);
        context.animator.SetFloat("moveSpeed", 5);
    }
    public override void UpdateState(PlayerController context) 
    {
        context.transform.localPosition *= 0.95f;
    }
    public override void LeaveState(PlayerController context) 
    {
        context.transform.parent = null;
        context.rigidBody.isKinematic = false;
    }
    public override void OnAttack(PlayerController context) {}
    public override void OnDive(PlayerController context) {}
    public override void OnCollision(PlayerController context, Collision other) {}
}
