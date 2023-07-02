using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public CircularMovement circMov = null;
    public ShootingComponent shootcomp = null;

    private CustomInput input = null;
    private float moveInput = 0.0f;
    private Transform tf = null;

    private void Awake()
    {
        input = new CustomInput();
        tf = GetComponent<Transform>();
        if (circMov == null) { circMov = GetComponent<CircularMovement>(); }
        if (shootcomp == null) { shootcomp = GetComponent<ShootingComponent>();}
    }

    private void OnEnable()
    {
        if (input != null)
        {
            input.Enable();
            //subscribe
            input.Player.Movement.performed += OnMovementPerformed;
            input.Player.Movement.canceled += OnMovementCancelled;
            input.Player.Shoot.performed += OnShootPerformed;
        }
        else
        {
            Debug.Log("Missing CustomInput.");
        }
    }

    private void OnDisable()
    {
        if (input != null)
        {
            input.Disable();
            //unsubscribe
            input.Player.Movement.performed -= OnMovementPerformed;
            input.Player.Movement.canceled -= OnMovementCancelled;
            input.Player.Shoot.performed -= OnShootPerformed;
        }
        else
        {Debug.Log("Missing input script.");}
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<float>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveInput = 0.0f;
    }

    private void OnShootPerformed(InputAction.CallbackContext value)
    {
        if (shootcomp != null)
        { 
            shootcomp.Shoot(tf.position, tf.rotation); 
        }
        else { Debug.Log("Missing Shooting Component."); }
    }

    private void Update()
    {
        if (circMov != null)
        {
            circMov.MoveOnCircle(moveInput, 0.0f);
        }
        else { Debug.Log("Missing MovementComponent."); }
    }

}
