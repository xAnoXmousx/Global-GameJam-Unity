using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrPlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody _playerRigidBody;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _maxSpeed;

    private Vector2 _inputMoveRaw;
    private float _playerRotation;
    private bool _isInput;

    public void OnMove(InputAction.CallbackContext MoveContext)
    {
        _inputMoveRaw = MoveContext.ReadValue<Vector2>();
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        // Read the input from the movement buttons and convert it to an angle
        checkForInput();

        // check to see if there is movement input and then if there is input then you should calculate degrees
        if (_isInput == true)
        {
            calculateDegrees();
        }

        transform.localRotation = Quaternion.Euler(0, _playerRotation, 0);


    }

    private void calculateDegrees()
    {
        float _axisRotationRadian;

        _axisRotationRadian = Mathf.Atan2(-_inputMoveRaw.y, _inputMoveRaw.x);
        _playerRotation = _axisRotationRadian * Mathf.Rad2Deg;
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

    private void FixedUpdate()
    {
        +
    }

}
