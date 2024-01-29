using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrPlayerController : MonoBehaviour
{
    // GameObject Component
    [SerializeField] Rigidbody _playerRigidBody;

    // Used to float the rigid body and avoid ground collision
    [Header("Floating Body")]
    [SerializeField] float _floatingFactor;
    [SerializeField] float _floatStrength;
    [SerializeField] float _clampedFloatHeight;

    // Used for controller maps to rotate Player Character
    private Vector2 _inputMoveRaw;
    private float _playerRotation;
    private Vector2 _inputRotation;

    // Used in rigidbody physics based movement, velocity, speed etc.
    [Header("Movement Speed Controls")]
    [SerializeField] float _movementSpeed;
    [SerializeField] float _maxSpeed;
    private Vector3 _desiredVelocity;
    
    // Used in a function that returns if there is any input on the left stick
    private bool _isInput;

    // Used in a function that allows the player to jump by adding a positive force on the Y axis to the rigid body.
    [Header("Jump Controls")]
    [SerializeField] float _jumpStrength;
    [SerializeField] float _maxJumpHeight;
    private bool _isGrounded = true;
    private bool _inputJump;

    // Raycast Controls - used to check to see if the chracter is grounded
    [Header("Raycast Controls")]
    [SerializeField] GameObject _rcPos;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float _rayDistance;




    public void OnMove(InputAction.CallbackContext MoveContext)
    {
        // Gathers the move input when you interact with the left stick/keyboard and assigns it to a vector 2 variable with Z being Vertical, and X being Horizontal.
        _inputMoveRaw = MoveContext.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    // Update is called once per frame
    public void Update()

    {
        var rb = _playerRigidBody;

        // Read the input from the movement buttons and convert it to an angle
        checkForInput();

        // check to see if there is movement input and then if there is input then you should calculate degrees
        if (_isInput == true)
        {
            calculateDegrees();
        }

        transform.localRotation = Quaternion.Euler(0, _playerRotation, 0);

        if (rb.velocity.magnitude > _maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, _maxSpeed);
        }

        checkIsGrounded();
    }

    public void FixedUpdate()
    {
        move();

    }

    private void calculateDegrees()
    {
        float _axisRotationRadian;

        _axisRotationRadian = Mathf.Atan2(-_inputMoveRaw.y, _inputMoveRaw.x);
        _playerRotation = _axisRotationRadian * Mathf.Rad2Deg;
    }

    private void move()
    {
        var rb = _playerRigidBody;

        if (!_isInput) { rb.velocity = new Vector3(0, 0, 0); }

        if (_isInput)
        {
            rb.AddRelativeForce(Vector3.forward * _movementSpeed, ForceMode.VelocityChange);

            Debug.Log("Current Speed" + rb.velocity);


        }

    }

    private void Jump()
    {

        var rb = _playerRigidBody;
        Vector3 _jumpForce = new Vector3(0, _jumpStrength, 0);

        if (_isGrounded == true)
        {
            rb.AddForce(_jumpForce, ForceMode.VelocityChange);
        }
    }

    private bool checkForInput()
    {
        if (_inputMoveRaw.x == 0 && _inputMoveRaw.y == 0)
        {
            _isInput = false;
        }

        else _isInput = true;

        return _isInput;
    }

    private bool checkIsGrounded()
    {
        var rcSpherePos = _rcPos.transform.position;


        if (Physics.Raycast(rcSpherePos, Vector3.down, _rayDistance, GroundLayer))
        {
            Debug.Log("YOU ARE GROUNDED!!!");
            Debug.DrawRay(rcSpherePos, Vector3.down, Color.yellow);

            _isGrounded = true;
        }

        else _isGrounded= false;

        return _isGrounded;
        
    }

}
