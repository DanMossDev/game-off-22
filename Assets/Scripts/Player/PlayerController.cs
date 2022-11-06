using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Space][Header("Player Movement Variables")]
    [Tooltip("Height the character can jump in units")]
    public float jumpHeight = 3;
    [Tooltip("Force added to the player when diving while moving")]
    public float diveForce = 20;
    [Tooltip("Maximum amount the dive can be charged while stationary")]
    public float maxDiveCharge = 0;
    [Tooltip("Rate at which the dive charges")]
    public float chargeRate = 0;
    [Tooltip("Rate at which the player accelerates")]
    public float acceleration = 10;
    [Tooltip("Ratio by which your speed decreases when not inputting - lower is a greater reduction")][Range(0, 1)]
    public float stoppingDrag = 1;
    [Tooltip("The force added to the player on contact with a hazard")]
    public float hitBounce = 10;
    [Tooltip("The rate at which the character rotates towards their forward movement direction")]
    public float rotationSpeed = 360;
    [Tooltip("Amount of time between pressing a jump and a jump being valid in which the jump will still be executed")]
    public float coyoteTime = 0.1f;


    [Space][Header("Transforms, layers and prefabs")]
    [Tooltip("Transform from which to check if the player is grounded")]
    public Transform feet;
    [Tooltip("Transform from which to check if the player is grounded while diving")]
    public Transform belly;
    [Tooltip("Layers which will be considered as grounded, i.e. things you can jump off")]
    public LayerMask ground;
    [Tooltip("Layers which will be considered as hazards, i.e. things on the stage that hurt you")]
    public LayerMask hazard;
    [Tooltip("Layers which the homing attack will target")]
    public LayerMask homingTargets;


    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public CapsuleCollider capColl;
    [HideInInspector] public BoxCollider boxColl;
    [HideInInspector] public HPManager hitPoints;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public float diveCharge = 0;
    [HideInInspector] public bool hitStunned = false;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isInvincible = false;

    public float? lastGroundedTime;
    public float? jumpPressedTime;


    //States
    [HideInInspector] public PlayerState currentState;
    [HideInInspector] public BaseState baseState = new BaseState();
    [HideInInspector] public ChargeState chargeState = new ChargeState();
    [HideInInspector] public DiveState diveState = new DiveState();
    [HideInInspector] public AttackState attackState = new AttackState();


    public static PlayerController Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        rigidBody = GetComponent<Rigidbody>();
        capColl = GetComponent<CapsuleCollider>();
        boxColl = GetComponent<BoxCollider>();
        hitPoints = GetComponent<HPManager>();
    }
    void Start()
    {
        isInvincible = false;
        currentState = baseState;
    }

    void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(PlayerState state)
    {
        currentState.LeaveState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    void OnMove(InputValue value)
    {
        horizontalInput = value.Get<Vector2>().x;
        verticalInput = value.Get<Vector2>().y;
    }

    void OnJump()
    {
        jumpPressedTime = Time.time;
        if (currentState == diveState && isGrounded) ChangeState(baseState);
    }

    void OnDive(InputValue value)
    {
        if ((currentState != diveState && rigidBody.velocity.magnitude > 5)) ChangeState(diveState);
        else if (value.Get<float>() == 1 && rigidBody.velocity.magnitude <= 5) ChangeState(chargeState);
        else if (value.Get<float>() == 0 && currentState == chargeState) ChangeState(diveState);
    }

    void OnAttack()
    {
        if (isGrounded) return;
        ChangeState(attackState);
    }

    void OnCollisionEnter(Collision other) {
        if ((ground & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) return;
        if ((hazard & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) {
            TakeDamage(other);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !isInvincible) TakeDamage(other);
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && (currentState == attackState || currentState == diveState))
        {
            other.gameObject.GetComponent<HPManager>().TakeDamage();
            ChangeState(baseState);
        }
    }

    public void TakeDamage(Collision other)
    {
        hitPoints.TakeDamage();
        if (currentState != baseState) ChangeState(baseState);
        rigidBody.AddForce((other.contacts[0].normal + Vector3.up) * hitBounce, ForceMode.Impulse);
        hitStunned = true;
        PowerUps.Instance.StopToast();

        if (hitPoints.currentHP > 0) StartCoroutine(EndHitstun());
    }

    IEnumerator EndHitstun()
    {
        while (hitStunned)
        {
            yield return new WaitForSeconds(0.1f);
            if (isGrounded) {
                hitStunned = false;
            }
        }
    }

    public void EndInvincibility()
    {
        StartCoroutine(EndInvincibilityDelay());
    }

    IEnumerator EndInvincibilityDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }
}
