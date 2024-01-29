using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [Header("Look Controls")]
    [SerializeField] private float _rotationSpeed = 360f;

    [Header("Movement & Jump Controls")]
    [SerializeField] private float _moveSpeed = 8.0f;
    [Space]
    [SerializeField] private GameObject _feet;
    [SerializeField] private float _jumpStrength = 10f;
    [SerializeField] private float _maxJumpHeight = 15f;
    [SerializeField] private float _rayCastDistance = .25f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _isGrounded = true;

    private Vector3 _lsInput;

    private void Update()
    {
        Look();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Gathers the input from the Input system when a input is read for movement and converts it to vector 3 x,z with y of null.
    public void MoveInput(InputAction.CallbackContext onMoveContext)
    {

        Vector2 inputValue = onMoveContext.ReadValue<Vector2>();
        _lsInput = new Vector3(inputValue.x, 0 , inputValue.y);
    }


    // Callback for jump action, runs when event is evoked - jump button is pressed.
    public void JumpInput(InputAction.CallbackContext onJumpContext)
    {
        Jump();
    }

    
    // taking the value of the _lsInput vector2 and converting it to a quaternion, with look rotation rotating around y
    //using inbuilt unity systems to create a lerp "rotate towards" the value of 360 enables quick movement.

    private void Look()
    {
        if (_lsInput == Vector3.zero) return;

        var lookRot = Quaternion.LookRotation(_lsInput, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, _rotationSpeed * Time.deltaTime);

    }


    // uses the kinematic of rigidbody to move it in space, takes the current position, adds +=1 * the value of the input magnitude
    // (0 - 1 float) enabling slower walking, and multiplies it by a movemeent speed, and time.delta time to ensure physics doesn't break.
    private void Move()
    {
        //Vector3 _lsInputCorrected = Vector3.ClampMagnitude(_lsInput, 1f);
        _rb.MovePosition(transform.position + transform.forward * _lsInput.magnitude * _moveSpeed * Time.deltaTime);
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(_feet.transform.position, Vector3.down, _rayCastDistance, _groundLayer))
        {
            Debug.Log("You are Grounded!");
            _isGrounded = true;
            
        }

        else
        {
            Debug.Log("You aren't grounded!");
            _isGrounded = false;
            
        }
    }

    private void Jump()
    {
        

        if (_isGrounded == false)
        {
            return;
        }

        else
        {
            _rb.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
        }
    }


}
