using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState(EnemyController context);
    public abstract void UpdateState(EnemyController context);
    public abstract void LeaveState(EnemyController context);
}
