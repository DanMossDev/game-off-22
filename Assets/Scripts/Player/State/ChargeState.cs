using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : PlayerState
{
    public override void EnterState(PlayerController context) 
    {
        context.animator.SetBool("isCharging", true);
        context.isInvincible = true;
        context.boxColl.enabled = true;
        context.capColl.enabled = false;
    }
    public override void UpdateState(PlayerController context) 
    {
        Rotate(context);
        context.rigidBody.velocity *= 0.5f;
        if (context.diveCharge < context.maxDiveCharge) context.diveCharge += Time.deltaTime * context.chargeRate;

    }
    public override void LeaveState(PlayerController context) 
    {
        context.animator.SetBool("isCharging", false);
    }

    public override void OnDive(PlayerController context, bool isPressed)
    {
        context.ChangeState(context.diveState);
    }

    public override void OnAttack(PlayerController context)
    {}

    public override void OnCollision(PlayerController context, Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<HPManager>().TakeDamage();
        }
    }

    void Rotate(PlayerController context)
    {
        if (context.horizontalInput == 0 && context.verticalInput == 0) return;
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * 2 * Time.deltaTime);
    }
}
