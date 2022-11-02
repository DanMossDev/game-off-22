using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Height the character can jump in units")][SerializeField]
    float jumpHeight = 3;

    [Tooltip("Rate at which the player accelerates")][SerializeField]
    float acceleration = 10;

    [Tooltip("Drag force applied when no input is given, should be higher than friction to allow for more responsive stopping")][SerializeField]
    float stoppingDrag = 1;

    [Tooltip("Amount of drag while running, higher values give a lower top speed")][SerializeField] 
    float friction = 0.2f;

    [Tooltip("The rate at which the character rotates towards their forward movement direction")][SerializeField]
    float rotationSpeed = 360;

    Rigidbody rigidBody;
    float horizontalInput;
    float verticalInput;
    bool hitStunned = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (horizontalInput == 0 && verticalInput == 0) {
            rigidBody.drag = stoppingDrag;
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
        float impulse = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, impulse, rigidBody.velocity.z);
    }

    void Movement()
    {
        if (horizontalInput == 0) rigidBody.velocity = new Vector3(rigidBody.velocity.x * 0.9f, rigidBody.velocity.y, rigidBody.velocity.z);
        else if (verticalInput == 0) rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, rigidBody.velocity.z * 0.9f);
        
        rigidBody.drag = friction;
        rigidBody.AddForce(horizontalInput * acceleration, 0, verticalInput * acceleration);
    }

    void Rotate()
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
