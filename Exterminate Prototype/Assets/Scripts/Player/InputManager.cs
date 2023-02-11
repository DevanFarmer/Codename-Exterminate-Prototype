using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerInput.WeaponActions weapon;

    private PlayerMotor motor;
    private PlayerLook look;
    private Weapon playerWeapon;

    public bool firing;

    void Awake()
    {
        playerInput = new PlayerInput();

        #region OnFoot Inputs
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();

        #endregion

        #region Weapon Inputs
        weapon = playerInput.Weapon;
        playerWeapon = GetComponent<Weapon>();

        //Activates firing variable to shoot and deactivates to stop shooting
        weapon.PrimaryFire.performed += ctx => ActivateFiring();
        weapon.PrimaryFire.canceled += ctx => DeactivateFiring();

        //weapon.SecondaryFire.performed += ctx +> playerWeapon.AltFire();
        weapon.Reload.performed += ctx => playerWeapon.ReloadWeapon();

        #endregion

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        //tell the playermotor to move using the value given from movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        //check if sprinting it toggled
        if (motor.toggleSprint)
        {
            onFoot.Sprint.canceled += ctx => motor.Sprint();
        }
    }

    #region ToggleFiring
    void ActivateFiring()
    {
        firing = true;
    }

    public void DeactivateFiring()
    {
        firing = false;
    }
    #endregion

    #region OnEnable/OnDisable 
    private void OnEnable()
    {
        onFoot.Enable();
        weapon.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
        weapon.Disable();
    }
    #endregion
}
