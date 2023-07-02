using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private CustomInput input = null;
    private float moveInput = 0.0f;

    public float radius = 5.0f;
    public float moveSpeed = 0.01f;
    public float radian = 0.0f;

    private Transform tf = null;

    private void Awake()
    {
        input = new CustomInput();
        tf = GetComponent<Transform>();
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
        //TODO:Shooting
        Debug.Log("Boom Baam.");
    }

    private void Update()
    {
        float timeAdjMovement = moveSpeed * Time.deltaTime * moveInput;

        //moving along circle, see complex representation r*e^(i*phi)= r*(cos(phi) + i*sin(phi))
        radian = Mathf.Repeat(radian + timeAdjMovement, 2 * Mathf.PI);
        Vector3 newPos = radius * new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0.0f);

        //LookRotation(Z-Axis, Y Axis) Z-Axis should point forward and Y-Axis to target
        Quaternion newRot = Quaternion.LookRotation(Vector3.forward, newPos);

        if (tf != null)
        {
            tf.SetLocalPositionAndRotation(newPos, newRot);
        }
        else { Debug.Log("Can't find Transform."); }
    }

}
