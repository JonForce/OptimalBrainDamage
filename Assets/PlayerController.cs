using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    private Gamepad gamepad;

    private void Start()
    {
        
    }

    void Update()
    {
        if (gamepad == null && gamepad.enabled)
            return;
        else
            UpdateControls();
    }

    public void SetGamepad(Gamepad gamepad)
    {
        this.gamepad = gamepad;
    }

    public Gamepad GetGamepad()
    {
        return this.gamepad;
    }

    private void UpdateControls()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(gamepad.leftStick.x.ReadValue(), 0, gamepad.leftStick.y.ReadValue());
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (gamepad.aButton.ReadValue() > 0)
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
