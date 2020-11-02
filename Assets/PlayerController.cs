using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private enum ANIMATION_STATE { RUNNING, IDLE, JUMPING };

    public CharacterController controller;

    public bool isAlive = true; // defaults alive
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public GameObject runAnimation;
    public GameObject jumpAnimation;
    public GameObject idleAnimation;

    private Vector3 animationSpawnOffset = new Vector3(0, -1.1f, 0);
    private Vector3 moveDirection = Vector3.zero;
    private Gamepad gamepad;

    private ANIMATION_STATE animationState;
    private GameObject animation;

    private void Start()
    {
        Use(idleAnimation, ANIMATION_STATE.IDLE);
    }

    void Update()
    {
        if (gamepad == null || !gamepad.enabled)
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
            {
                moveDirection.y = jumpSpeed;
                if (animationState != ANIMATION_STATE.JUMPING)
                    Use(jumpAnimation, ANIMATION_STATE.JUMPING);
            } else
            {
                if (moveDirection.magnitude > 0)
                {
                    if (animationState != ANIMATION_STATE.RUNNING)
                        Use(runAnimation, ANIMATION_STATE.RUNNING);
                } else
                {
                    if (animationState != ANIMATION_STATE.IDLE)
                        Use(idleAnimation, ANIMATION_STATE.IDLE);
                }
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // Make the animation face the correct direction.
        if (Math.Abs(moveDirection.x) > 0 || Math.Abs(moveDirection.z) > 0)
        {
            Quaternion quat = this.transform.rotation;
            Vector3 direction = new Vector3(this.moveDirection.x, 0, this.moveDirection.z);
            quat.SetLookRotation(direction);
            animation.transform.rotation = quat;
        }
    }

    public void kill() {
        //player "alive or dead"
        //disable player object
        isAlive = false;
        gameObject.SetActive(false);
        ////********** death animations
    }

    private void Use(GameObject newAnimation, ANIMATION_STATE newState)
    {
        GameObject.Destroy(animation, 0);
        Vector3 pos = this.transform.position;
        pos.x = pos.x + animationSpawnOffset.x;
        pos.y = pos.y + animationSpawnOffset.y;
        pos.z = pos.z + animationSpawnOffset.z;
        Quaternion rotation;
        if (animation == null) rotation = this.transform.rotation;
        else rotation = animation.transform.rotation;
        animation = Instantiate(newAnimation, pos, rotation);
        animation.transform.parent = this.transform;
        animationState = newState;
    }
}
