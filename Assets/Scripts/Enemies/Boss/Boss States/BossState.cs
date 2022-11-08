using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    public abstract void EnterState(BossController context);
    public abstract void UpdateState(BossController context);
    public abstract void LeaveState(BossController context);
}
