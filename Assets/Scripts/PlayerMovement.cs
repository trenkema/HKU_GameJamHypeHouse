using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded { private set; get; }

    public float x { private set; get; }
    public float z { private set; get; }

    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //Vector3 move = transform.right * x + transform.forward * z;

        //controller.Move(move * speed * Time.deltaTime);

        //velocity.y += gravity * Time.deltaTime;

        //controller.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;

        var inputVector = new Vector3(x, 0, z);

        Vector3 move = transform.right * x + transform.forward * z;

        var inputMag = (inputVector.magnitude > 1) ? move = move.normalized * speed : move = move * speed;

        //move = move * speed;

        move.y = velocity.y;

        controller.Move(move * Time.deltaTime);
    }

    //public void Move()
    //{
    //    Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

    //    moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * moveSpeed, ref smoothMoveVelocity, smoothTime);
    //}
}
