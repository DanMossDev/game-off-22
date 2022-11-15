using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostState : PlayerState
{
    float timeEntered;
    public override void EnterState(PlayerController context) 
    {
        timeEntered = Time.time;
    }
    public override void UpdateState(PlayerController context) 
    {
        if (Time.time - timeEntered >= context.boostTime) context.ChangeState(context.baseState);

        context.animator.SetFloat("ySpeed", context.rigidBody.velocity.y);
        context.animator.SetFloat("moveSpeed", Mathf.Log(new Vector3(context.rigidBody.velocity.x, 0, context.rigidBody.velocity.z).magnitude / 10 + 1));
        if (Physics.OverlapSphere(context.feet.position, 0.2f, context.ground).Length != 0) 
        {
            context.isGrounded = true;
            context.lastGroundedTime = Time.time;
            context.canAttack = true;
            if (!context.animator.GetBool("isGrounded")) SFXController.Instance.PlaySFX(context.landSound);
            context.animator.SetBool("isGrounded", true);
        } else {
            context.isGrounded = false;
            context.animator.SetBool("isGrounded", false);
        }

        Rotate(context);
    }
    public override void LeaveState(PlayerController context) {}
    public override void OnAttack(PlayerController context) {}
    public override void OnDive(PlayerController context) {}
    public override void OnCollision(PlayerController context, Collision other) 
    {
        context.ChangeState(context.baseState);
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(context.rigidBody.velocity, Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * Time.deltaTime);
    }
}
