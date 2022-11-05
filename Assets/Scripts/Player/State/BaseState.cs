using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : PlayerState
{
    public override void EnterState(PlayerController context) 
    {
        context.capColl.enabled = true;
        context.boxColl.enabled = false;
    }
    public override void UpdateState(PlayerController context) 
    {
        if (Physics.OverlapSphere(context.feet.position, 0.2f, context.ground).Length != 0) 
        {
            context.isGrounded = true;
            context.lastGroundedTime = Time.time;
        } else context.isGrounded = false;
        if (Time.time - context.lastGroundedTime <= context.coyoteTime && Time.time - context.jumpPressedTime <= context.coyoteTime) Jump(context);
        if (context.hitStunned) return;
        if (context.horizontalInput == 0 && context.verticalInput == 0) {
            context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x * context.stoppingDrag, context.rigidBody.velocity.y, context.rigidBody.velocity.z * context.stoppingDrag);
            return;
        }
        Movement(context);
        Rotate(context);
    }
    public override void LeaveState(PlayerController context) 
    {}

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
        float impulse = Mathf.Sqrt(context.jumpHeight * -2 * Physics.gravity.y);
        context.rigidBody.AddForce(new Vector3(0, impulse, 0), ForceMode.Impulse);
    }
    
}