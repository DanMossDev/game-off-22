using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Variables
    public float agroRange = 10;
    public float patrolSpeed = 5;
    public float chaseSpeed = 5;

    public Transform[] patrolPoints;
    public EnemyBehaviour behaviour;

    //Cached references
    [HideInInspector] public GameObject target;
    [HideInInspector] public CharacterController charController;

    //State
    [HideInInspector] public EnemyState currentState;
    [HideInInspector] public PatrolState patrolState = new PatrolState();
    [HideInInspector] public AgroState agroState = new AgroState();
    [HideInInspector] public FearState fearState = new FearState();
    [HideInInspector] public DistState distState = new DistState();

    void Start()
    {
        target = PlayerController.Instance.gameObject;
        charController = GetComponent<CharacterController>();
        currentState = patrolState;
    }
    void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(EnemyState state)
    {
        currentState.LeaveState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}

public enum EnemyBehaviour {
    Aggressive,
    Fearful,
    Distant,
    Ignorant
}
