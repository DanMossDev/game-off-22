using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeState : PlayerState
{
    Vector3 centerPoint;
    Vector3 currentPos;
    public override void EnterState(PlayerController context) 
    {
        centerPoint = context.pipeCenter.position;
        currentPos = context.transform.position;
        context.rigidBody.isKinematic = true;
        context.animator.SetBool("isGrounded", true);
        context.animator.SetFloat("ySpeed", 0);
    }
    public override void UpdateState(PlayerController context) 
    {
        Vector3 directionUp = centerPoint - context.transform.position;
        context.transform.up = directionUp;
        Vector3 directionForward = context.transform.position - currentPos;
        context.transform.forward = directionForward;
        currentPos = context.transform.position;
        context.transform.localPosition *= 0.95f;
    }
    public override void LeaveState(PlayerController context) 
    {
        context.transform.up = Vector3.up;
        context.transform.parent = null;
        context.rigidBody.isKinematic = false;
    }
    public override void OnAttack(PlayerController context) {}
    public override void OnDive(PlayerController context) {}
    public override void OnCollision(PlayerController context, Collision other) {}
}
