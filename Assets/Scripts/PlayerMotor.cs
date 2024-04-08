using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool isGrounded = false;

    public Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        this.isGrounded = this.controller.isGrounded;
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x * this.speed * Time.deltaTime;
        moveDirection.z = input.y * this.speed * Time.deltaTime;
        this.controller.Move(transform.TransformDirection(moveDirection));
        // gravity handler - every sek: -9.8 m/s along Oy if not on the ground;
        if (this.isGrounded && this.playerVelocity.y < 0)
        {
            this.playerVelocity.y = -2;
        } else {
             this.playerVelocity.y += this.gravity * Time.deltaTime;
        }
        this.controller.Move(this.playerVelocity * Time.deltaTime);
        Debug.Log(this.playerVelocity.y);
        Debug.Log(this.isGrounded);
    }

    public void ProcessJump()
    {
        if (this.isGrounded)
        {
            Debug.Log("ProcessJump is called");
            // delta(v) = h/t + g*t
            this.playerVelocity.y = Mathf.Sqrt(this.jumpHeight * -3f * this.gravity);
        }
    }
}
