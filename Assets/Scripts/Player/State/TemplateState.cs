using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateState : PlayerState
{
    public override void EnterState(PlayerController context) {}
    public override void UpdateState(PlayerController context) {}
    public override void LeaveState(PlayerController context) {}
    public override void OnAttack(PlayerController context) {}
    public override void OnDive(PlayerController context) {}
    public override void OnCollision(PlayerController context, Collision other) {}
}
