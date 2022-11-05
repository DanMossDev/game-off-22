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
            context.isGrounded = true;
            context.lastGroundedTime = Time.time;
        }
        if (context.horizontalInput == 0 && context.verticalInput == 0) return;
        Movement(context);
        Rotate(context);
    }
    public override void LeaveState(PlayerController context) 
    {
        
        context.isInvincible = false;
    }

    void Movement(PlayerController context)
    {   
        Vector3 movement = new Vector3(context.horizontalInput, 0, context.verticalInput).normalized;

        context.rigidBody.velocity = context.rigidBody.velocity.magnitude * movement;
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * 2 * Time.deltaTime);
    }
}
