using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Space][Header("Player Movement Variables")]
    [Tooltip("Height the character can jump in units")][SerializeField]
    float jumpHeight = 3;
    [Tooltip("Rate at which the player accelerates")][SerializeField]
    float acceleration = 10;
    [Tooltip("Ratio by which your speed decreases when not inputting - lower is a greater reduction")][SerializeField][Range(0, 1)]
    float stoppingDrag = 1;
    [Tooltip("The force added to the player on contact with a hazard")][SerializeField]
    float hitBounce = 10;
    [Tooltip("The rate at which the character rotates towards their forward movement direction")][SerializeField]
    float rotationSpeed = 360;

    [Space][Header("Transforms, layers and prefabs")]
    [Tooltip("Transform from which to check if the player is grounded")][SerializeField]
    Transform feet;
    [Tooltip("Layers which will be considered as grounded, i.e. things you can jump off")][SerializeField]
    LayerMask ground;
    [Tooltip("Layers which will be considered as hazards, i.e. things on the stage that hurt you")][SerializeField]
    LayerMask hazard;


    Rigidbody rigidBody;
    float horizontalInput;
    float verticalInput;
    bool hitStunned = false;
    [HideInInspector] public bool isGrounded = true;


    public static PlayerController Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground);
        if (hitStunned) return;
        if (horizontalInput == 0 && verticalInput == 0) {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x * stoppingDrag, rigidBody.velocity.y, rigidBody.velocity.z * stoppingDrag);
            return;
        }
        Movement();
        Rotate();
    }

    void OnMove(InputValue value)
    {
        horizontalInput = value.Get<Vector2>().x;
        verticalInput = value.Get<Vector2>().y;
    }

    void OnJump()
    {
        if (!isGrounded) return;
        float impulse = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        rigidBody.AddForce(new Vector3(0, impulse, 0), ForceMode.Impulse);
    }

    void Movement()
    {
        if (horizontalInput == 0 || Mathf.Sign(rigidBody.velocity.x) != Mathf.Sign(horizontalInput)) rigidBody.velocity = new Vector3(rigidBody.velocity.x * stoppingDrag, rigidBody.velocity.y, rigidBody.velocity.z);
        if (verticalInput == 0 || Mathf.Sign(rigidBody.velocity.z) != Mathf.Sign(verticalInput)) rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, rigidBody.velocity.z * stoppingDrag);
        
        Vector3 movement = new Vector3(horizontalInput * acceleration * (30 / (Mathf.Abs(rigidBody.velocity.x) + 10)), 0, verticalInput * acceleration * (30 / (Mathf.Abs(rigidBody.velocity.z) + 10)));

        if (!isGrounded) movement *= 0.6f;
        rigidBody.AddForce(movement, ForceMode.Force);
    }

    void Rotate()
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other) {
        if ((hazard & 1 << other.gameObject.layer) != 1 << other.gameObject.layer) return;
        rigidBody.AddForce((other.contacts[0].normal + Vector3.up) * hitBounce, ForceMode.Impulse);
        hitStunned = true;
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
