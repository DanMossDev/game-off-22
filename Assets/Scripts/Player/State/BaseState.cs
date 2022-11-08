using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : PlayerState
{
    float lastFootstepTime;
    public override void EnterState(PlayerController context) 
    {
        context.capColl.enabled = true;
        context.boxColl.enabled = false;
        lastFootstepTime = Time.time;
    }
    public override void UpdateState(PlayerController context) 
    {
        context.animator.SetFloat("ySpeed", context.rigidBody.velocity.y);
        context.animator.SetFloat("moveSpeed", new Vector3(context.rigidBody.velocity.x, 0, context.rigidBody.velocity.z).magnitude / 10);
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
        if (Time.time - context.lastGroundedTime <= context.coyoteTime && Time.time - context.jumpPressedTime <= context.coyoteTime) Jump(context);
        if (context.hitStunned) return;
        if (context.horizontalInput == 0 && context.verticalInput == 0) {
            context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x * context.stoppingDrag, context.rigidBody.velocity.y, context.rigidBody.velocity.z * context.stoppingDrag);
            return;
        }
        Movement(context);
        context.animator.SetBool("isSkidding", Utils.CompareVector3(new Vector3(context.horizontalInput, 0, context.verticalInput).normalized, context.rigidBody.velocity.normalized * -1, 0.25f));
        Rotate(context);
    }
    public override void LeaveState(PlayerController context) 
    {
        context.animator.SetBool("isSkidding", false);
    }

    public override void OnDive(PlayerController context, bool isPressed)
    {
        if (context.rigidBody.velocity.magnitude > 30 || !context.isGrounded) context.ChangeState(context.diveState);
        else if (context.isGrounded && context.rigidBody.velocity.magnitude <= 30) context.ChangeState(context.chargeState);
    }

    public override void OnAttack(PlayerController context)
    {
        if (!context.isGrounded && context.canAttack) context.ChangeState(context.attackState);
    }

    public override void OnCollision(PlayerController context, Collision other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Boss")) && !context.isInvincible)
        {
            context.TakeDamage(other);
        }
    }

    void Movement(PlayerController context)
    {
        if (context.horizontalInput == 0 || Mathf.Sign(context.rigidBody.velocity.x) != Mathf.Sign(context.horizontalInput)) context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x * context.stoppingDrag, context.rigidBody.velocity.y, context.rigidBody.velocity.z);
        if (context.verticalInput == 0 || Mathf.Sign(context.rigidBody.velocity.z) != Mathf.Sign(context.verticalInput)) context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x, context.rigidBody.velocity.y, context.rigidBody.velocity.z * context.stoppingDrag);
        
        Vector3 movement = new Vector3(context.horizontalInput * context.acceleration * (30 / (Mathf.Abs(context.rigidBody.velocity.x) + 10)), 0, context.verticalInput * context.acceleration * (30 / (Mathf.Abs(context.rigidBody.velocity.z) + 10)));
        context.rigidBody.AddForce(movement, ForceMode.Force);
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * Time.deltaTime);
    }

    void Jump(PlayerController context)
    {
        context.jumpPressedTime = null;
        context.lastGroundedTime = null;
        context.animator.ResetTrigger("Jump");
        context.animator.SetTrigger("Jump");
        SFXController.Instance.PlaySFX(context.jumpSound);
        float impulse = Mathf.Sqrt(context.jumpHeight * -2 * Physics.gravity.y);
        context.rigidBody.AddForce(new Vector3(0, impulse, 0), ForceMode.VelocityChange);
    }

}
