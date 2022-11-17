using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterState : PlayerState
{
    public override void EnterState(PlayerController context) {}
    public override void UpdateState(PlayerController context) 
    {
        context.animator.SetFloat("ySpeed", context.rigidBody.velocity.y);
        context.animator.SetFloat("moveSpeed", Mathf.Log(new Vector3(context.rigidBody.velocity.x, 0, context.rigidBody.velocity.z).magnitude / 10 + 1));

        if (Physics.OverlapSphere(context.feet.position, 0.25f, context.water).Length != 0) 
        {
            if (context.rigidBody.velocity.magnitude < 30) context.Drown();
            context.isGrounded = true;
            context.lastGroundedTime = Time.time;
            context.animator.SetBool("isGrounded", true);
        } else {
            context.isGrounded = false;
            context.animator.SetBool("isGrounded", false);
        }

        Movement(context);
        Rotate(context);
    }
    public override void LeaveState(PlayerController context) {}
    public override void OnAttack(PlayerController context) {}
    public override void OnDive(PlayerController context) 
    {
        context.ChangeState(context.waterDiveState);
    }
    public override void OnCollision(PlayerController context, Collision other)
    {
        if ((context.ground & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) context.ChangeState(context.baseState);
        if ((other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Boss")) && !context.isInvincible)
        {
            context.rigidBody.AddForce((other.contacts[0].normal + Vector3.up) * context.hitBounce, ForceMode.Impulse);
        }
    }

    void Movement(PlayerController context)
    {
        if (context.horizontalInput == 0 || Mathf.Sign(context.rigidBody.velocity.x) != Mathf.Sign(context.horizontalInput)) context.rigidBody.velocity = new Vector3(context.rigidBody.velocity.x * context.stoppingDrag, context.rigidBody.velocity.y, context.rigidBody.velocity.z);
        
        Vector3 movement = new Vector3(context.horizontalInput * context.acceleration * (30 / (Mathf.Abs(context.rigidBody.velocity.x) + 10)), 0, context.verticalInput * context.acceleration * (30 / (Mathf.Abs(context.rigidBody.velocity.z) + 10)));
        context.rigidBody.AddForce(movement, ForceMode.Force);
    }

    void Rotate(PlayerController context)
    {
        if (context.horizontalInput == 0 && context.verticalInput == 0) return;
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * Time.deltaTime);
    }
}
