using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrPlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody _playerRigidBody;


    [Header("Collision Float")]
    [SerializeField] float _floatingFactor;
    [SerializeField] float _floatStrength;
    [SerializeField] float _clampedFloatHeight;

    private Vector2 _inputMoveRaw;
    private float _playerRotation;
    private Vector2 _inputRotation;

    [Header("Movement Speed Controls")]
    [SerializeField] float _movementSpeed;
    [SerializeField] float _maxSpeed;
    private Vector3 _desiredVelocity;
    

    private bool _isInput;

    [Header("Jump Controls")]
    [SerializeField] float _jumpStrength;
    [SerializeField] float _maxJumpHeight;
    private bool _isGrounded = true;
    private bool _inputJump;

    public void OnMove(InputAction.CallbackContext MoveContext)
    {
        _inputMoveRaw = MoveContext.ReadValue<Vector2>();
        
    }

    public void OnJump(InputAction.CallbackContext JumpContext)
    {
        _inputJump = JumpContext.ReadValue<bool>();

    }

    // Start is called before the first frame update
    void Start()
    {

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
    }

    public void FixedUpdate()
    {
        move();

        Jump();



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

        if (_isGrounded)
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

}
