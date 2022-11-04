using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Space][Header("Player Movement Variables")]
    [Tooltip("Height the character can jump in units")]
    public float jumpHeight = 3;
    [Tooltip("Force added to the player when diving")]
    public float diveForce = 20;
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


    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public CapsuleCollider capColl;
    [HideInInspector] public BoxCollider boxColl;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public bool hitStunned = false;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isDiving = false;

    public float? lastGroundedTime;
    public float? jumpPressedTime;


    //States
    [HideInInspector] public PlayerState currentState;
    [HideInInspector] public BaseState baseState = new BaseState();
    [HideInInspector] public DiveState diveState = new DiveState();


    public static PlayerController Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capColl = GetComponent<CapsuleCollider>();
        boxColl = GetComponent<BoxCollider>();

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

    void OnDive()
    {
        if (currentState != diveState) ChangeState(diveState);
    }

    void OnCollisionEnter(Collision other) {
        if ((hazard & 1 << other.gameObject.layer) != 1 << other.gameObject.layer) return;
        if (currentState != baseState) ChangeState(baseState);
        rigidBody.AddForce((other.contacts[0].normal + Vector3.up) * hitBounce, ForceMode.Impulse);
        hitStunned = true;
        PowerUps.Instance.StopToast();
        StartCoroutine(EndHitstun());
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
}
