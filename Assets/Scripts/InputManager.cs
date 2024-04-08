using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;

    // Start is called before the first frame update
    void Awake()
    {
        this.playerInput = new PlayerInput();
        this.onFoot = playerInput.OnFoot;
        this.motor = GetComponent<PlayerMotor>();
        this.onFoot.Jump.performed += ctx => this.motor.ProcessJump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        this.onFoot.Enable();
    }

    private void OnDisable()
    {
        this.onFoot.Disable();
    }
}
