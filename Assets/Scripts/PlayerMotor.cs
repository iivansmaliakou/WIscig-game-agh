using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private Vector2 input;
    public float angleSpeed = 400.0f;
    private float currentAngledVelocity;
    public float speed = 15;
    public float jumpSpeedAccelerationRatio = 2;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    public bool isGrounded = false;

    public float currentRelativeAngleOy = 0;

    public Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        coll = GetComponent<Collider>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.isGrounded = this.CheckGround();
    }
    public bool CheckGround()
    {
        float _distanceToTheGround = this.coll.bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
    public void ProcessMove(Vector2 input)
    {
        this.input = input;
        // animation handler;
        if (input.y == 0){
            this.animator.SetBool("isMoving", false);
        } else {
            this.animator.SetBool("isMoving", true);
        }
        if (this.isGrounded && this.playerVelocity.y <= 0)
        {
            this.playerVelocity.y = -2;
            this.animator.SetBool("isJumping", false);
        } else {
             this.playerVelocity.y += this.gravity * Time.deltaTime;
        }
        this.controller.Move(this.playerVelocity * Time.deltaTime);

        float currentMoveSpeed = speed;
        if (this.animator.GetBool("isMoving") && this.animator.GetBool("isJumping")){
            currentMoveSpeed *= this.jumpSpeedAccelerationRatio;
        }
        Vector3 moveDirection = Vector3.zero;
        moveDirection.z = input.y * currentMoveSpeed * Time.deltaTime;
        this.controller.Move(this.transform.TransformDirection(moveDirection));
        
        // rotation;
        if (input.x != 0){
            transform.Rotate(0, input.x * this.angleSpeed * Time.deltaTime, 0);
        }
    }

    public void ProcessJump()
    {
        if (this.isGrounded)
        {
            this.animator.SetBool("isJumping", true);
            this.playerVelocity.y = Mathf.Sqrt(this.jumpHeight * -1 * this.gravity);
        }
    }
}
