using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scrPlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRb;
    [SerializeField] float _movementSpeed, _maxVelocity;
    [SerializeField] float _pcRotation;
    [SerializeField] Vector2 _vectorMove, _vectorLook;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        Debug.Log("Raw Input" + _vectorMove);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Finds the target velocity by taking the input we are getting and setting it to vectory 2, multiples that by the movement speed
        Vector3 _currentVelocity = _playerRb.velocity;
        Vector3 _targetVelocity = new Vector3(_vectorMove.x, 0, _vectorMove.y);
        _targetVelocity *= _movementSpeed;

        // Align direction of movement to the player direction
        _targetVelocity = transform.TransformDirection(_targetVelocity);

        // Calculate force on RB
        Vector3 _velocityChange = (_targetVelocity - _currentVelocity);

        // Clamp  max velocity
        Vector3.ClampMagnitude(_velocityChange, _maxVelocity);

        // Apply force - forcemode makes there be no acceleration to get to speed.
        _playerRb.AddForce(_velocityChange, ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext movecontext)
    {
        _vectorMove = movecontext.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext lookcontext)
    {
        _vectorLook = lookcontext.ReadValue<Vector2>();
    }

}
