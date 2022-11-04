using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveState : PlayerState
{
    public override void EnterState(PlayerController context) 
    {
        context.boxColl.enabled = true;
        context.capColl.enabled = false;

        Vector3 diveBurst;
        if (context.horizontalInput == 0 && context.verticalInput == 0) diveBurst = context.transform.forward;
        else diveBurst = new Vector3(context.horizontalInput, 0, context.verticalInput).normalized;

        context.rigidBody.AddForce(diveBurst * context.diveForce * 40 / (Mathf.Abs(context.rigidBody.velocity.magnitude) + 10), ForceMode.Impulse);
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
    public override void LeaveState(PlayerController context) {}

    void Movement(PlayerController context)
    {   
        Vector3 movement = new Vector3(context.horizontalInput * context.acceleration, 0, context.verticalInput * context.acceleration);

        context.rigidBody.AddForce(movement / 4, ForceMode.Force);
    }

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed / 4 * Time.deltaTime);
    }
}
