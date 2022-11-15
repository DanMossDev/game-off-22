using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void EnterState(PlayerController context);
    public abstract void UpdateState(PlayerController context);
    public abstract void LeaveState(PlayerController context);
    public abstract void OnAttack(PlayerController context);
    public abstract void OnDive(PlayerController context);
    public abstract void OnCollision(PlayerController context, Collision other);
}
