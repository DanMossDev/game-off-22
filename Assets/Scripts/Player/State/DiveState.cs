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
        if (Time.time - context.lastGroundedTime < 0.25f) Slide(context);
        else Glide(context);
        Rotate(context);
    }
    public override void LeaveState(PlayerController context)
    {
        context.boxColl.enabled = false;
        context.capColl.enabled = true;
        context.animator.SetBool("isDiving", false);
        context.EndInvincibility();
    }

    public override void OnDive(PlayerController context)
    {
        if (Time.time - context.lastGroundedTime < 0.5f) context.ChangeState(context.baseState);
    }
    public override void OnAttack(PlayerController context)
    {}
    public override void OnCollision(PlayerController context, Collision other)
    {
        if (((context.water & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)) context.ChangeState(context.waterState);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) other.gameObject.GetComponent<HPManager>().TakeDamage();
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss")) context.TakeDamage(other);
    }

    void Glide(PlayerController context)
    {
        if (context.horizontalInput == 0 || Mathf.Sign(context.rigidBody.velocity.x) != Mathf.Sign(context.horizontalInput)) context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x * context.stoppingDrag, context.rigidBody.velocity.y, context.rigidBody.velocity.z);
        if (context.verticalInput == 0 || Mathf.Sign(context.rigidBody.velocity.z) != Mathf.Sign(context.verticalInput)) context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x, context.rigidBody.velocity.y, context.rigidBody.velocity.z * context.stoppingDrag);
        
        Vector3 movement = new Vector3(context.horizontalInput * context.acceleration * (30 / (Mathf.Abs(context.rigidBody.velocity.x) + 10)), 0, context.verticalInput * context.acceleration * (30 / (Mathf.Abs(context.rigidBody.velocity.z) + 10)));
        context.rigidBody.AddForce(movement, ForceMode.Force);
    }

    void Slide(PlayerController context)
    {   
        Vector3 movement = new Vector3(context.horizontalInput, 0, context.verticalInput).normalized;
        float ySpeed = context.rigidBody.velocity.y;
        Vector3 rotatedMovement = Vector3.RotateTowards(new Vector3(context.rigidBody.velocity.x, 0, context.rigidBody.velocity.z), movement, context.rotationSpeed * Time.deltaTime, 0);
        context.rigidBody.velocity = new Vector3(rotatedMovement.x, ySpeed, rotatedMovement.z);
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * 2 * Time.deltaTime);
    }
}
