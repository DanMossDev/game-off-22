using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveState : PlayerState
{
    public override void EnterState(PlayerController context) 
    {
        context.isInvincible = true;
        context.boxColl.enabled = true;
        context.capColl.enabled = false;
        SFXController.Instance.PlaySFX(context.diveSound);
        context.animator.SetTrigger("Dive");
        context.animator.SetBool("isDiving", true);

        Vector3 diveBurst;
        if (context.horizontalInput == 0 && context.verticalInput == 0) diveBurst = context.transform.forward;
        else diveBurst = new Vector3(context.horizontalInput, 0, context.verticalInput).normalized;
        float force;
        if (context.diveCharge == 0) force = context.diveForce * 40 / (Mathf.Abs(context.rigidBody.velocity.magnitude) + 10);
        else {
            force = context.diveCharge;
            context.diveCharge = 0;
        }
        context.rigidBody.AddForce(diveBurst * force, ForceMode.Impulse);
    }
    public override void UpdateState(PlayerController context) 
    {
        if (Physics.OverlapSphere(context.belly.position, 0.2f, context.ground).Length != 0) 
        {
            context.animator.SetBool("isGrounded", true);
            context.isGrounded = true;
            context.lastGroundedTime = Time.time;
        }
        else
        {
            context.animator.SetBool("isGrounded", true);
            context.isGrounded = false;
        }
        if (context.horizontalInput == 0 && context.verticalInput == 0) return;
        Movement(context);
        Rotate(context);
    }
    public override void LeaveState(PlayerController context)
    {
        context.animator.SetBool("isDiving", false);
        context.EndInvincibility();
    }

    public override void OnDive(PlayerController context, bool isPressed)
    {}
    public override void OnAttack(PlayerController context)
    {}
    public override void OnCollision(PlayerController context, Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<HPManager>().TakeDamage();
            context.rigidBody.AddForce((other.contacts[0].normal + Vector3.up) * context.hitBounce, ForceMode.VelocityChange);
            context.ChangeState(context.baseState);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss")) context.TakeDamage(other);
    }

    void Movement(PlayerController context)
    {   
        Vector3 movement = new Vector3(context.horizontalInput, 0, context.verticalInput).normalized;
        if (Utils.CompareVector3(movement, context.rigidBody.velocity.normalized, 0.2f)) return;
        context.rigidBody.AddForce(movement * context.acceleration / 4, ForceMode.Force);
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * 2 * Time.deltaTime);
    }
}
