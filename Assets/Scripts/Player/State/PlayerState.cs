using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void EnterState(PlayerController context);
    public abstract void UpdateState(PlayerController context);
    public abstract void LeaveState(PlayerController context);
}
