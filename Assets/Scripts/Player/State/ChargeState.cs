using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : PlayerState
{
    public override void EnterState(PlayerController context) 
    {
        context.isInvincible = true;
        context.boxColl.enabled = true;
        context.capColl.enabled = false;
    }
    public override void UpdateState(PlayerController context) 
    {
        context.rigidBody.velocity = Vector3.zero;
        if (context.diveCharge < context.maxDiveCharge) context.diveCharge += Time.deltaTime * context.chargeRate;

        Rotate(context);
    }
    public override void LeaveState(PlayerController context) {}

    void Rotate(PlayerController context)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(context.horizontalInput, 0, context.verticalInput), Vector3.up);
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, toRotation, context.rotationSpeed * 2 * Time.deltaTime);
    }
}
