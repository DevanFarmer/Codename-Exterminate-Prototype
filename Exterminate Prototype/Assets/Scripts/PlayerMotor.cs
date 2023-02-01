using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shared_Models;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Movement")]
    public float speed;
    private float gravity;
    private float jumpHeight;
    
    [Header("Crouch")]
    public bool lerpCrouch;
    public bool crouching;
    public float crouchTimer;

    [Header("Sprint")]
    public bool toggleSprint;
    public bool sprinting;

    [Header("Settings")]
    public MovementSettings movementSettings;

    private void Awake()
    {
        speed = movementSettings.WalkingSpeed;
        gravity = movementSettings.gravity;
        jumpHeight = movementSettings.jumpHeight;
        toggleSprint = movementSettings.toggleSprint;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        #region Crouching
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
        #endregion

        #region Jumping & Sprinting
        if (!isGrounded)
        {
            speed = movementSettings.inAirSpeed;
        }
        else if (sprinting)
        {
            speed = movementSettings.RunningSpeed;
        }
        else
        {
            speed = movementSettings.WalkingSpeed;
        }
        #endregion
    }

    public void ProcessMove(Vector2 input)
    {
        //MOVEMENT
        var horizontalSpeed = speed * input.x * Time.deltaTime;
        var verticalSpeed = speed * input.y * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0f, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        controller.Move(newMovementSpeed);

        //GRAVITY
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
    }
}
