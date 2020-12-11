using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float FORWARD_ACCELERATION = 10f;
    private const float BACKWARD_ACCELERATION = 10f;
    private const float STRAFE_ACCELERATION = 10.0f;
    private const float MAX_FORWARD_VELOCITY = 4.0f;
    private const float MAX_BACKWARD_VELOCITY = 2.0f;
    private const float MAX_STRAFE_VELOCITY = 3.0f;
    private const float GRAVITY_ACCELERATION = 10.0f;
    private const float MAX_HEAD_TILT_ROTATION = 60.0f;
    private const float MIN_HEAD_TILT_ROTATION = 280.0f;
    private const float ANGULAR_VELOCITY_FACTOR = 2.0f;
    private const float MAX_FALL_VELOCITY = 100.0f;



    private CharacterController _controller;
    private Transform _cameraTransform;
    private Vector3 _acceleration;
    private Vector3 _velocity;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _acceleration = Vector3.zero;
        _velocity = Vector3.zero;

        HideCursor();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeadTilt();
        UpdateRotation();
    }

    void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateVelocity();
        UpdatePosition();

    }

    private void UpdateHeadTilt()
    {
        Vector3 cameraRotation = _cameraTransform.localEulerAngles;

        cameraRotation.x -= Input.GetAxis("Mouse Y") * ANGULAR_VELOCITY_FACTOR;

        if (cameraRotation.x < 180.0f)
            cameraRotation.x = Mathf.Min(cameraRotation.x, MAX_HEAD_TILT_ROTATION);
        else
            cameraRotation.x = Mathf.Max(cameraRotation.x, MIN_HEAD_TILT_ROTATION);

        _cameraTransform.localEulerAngles = cameraRotation;
    }

    private void UpdateRotation()
    {
        float rotation = Input.GetAxis("Mouse X") * ANGULAR_VELOCITY_FACTOR;

        transform.Rotate(0, rotation, 0);
    }

    private void UpdateAcceleration()
    {
        _acceleration.z = Input.GetAxis("Forward");
        _acceleration.z *= (_acceleration.z >= 0) ? FORWARD_ACCELERATION : BACKWARD_ACCELERATION;

        _acceleration.x = Input.GetAxis("Strafe") * STRAFE_ACCELERATION;

        if (_controller.isGrounded)
        {
            _acceleration.y = 0f;
        }
        else
        {
            _acceleration.y = -GRAVITY_ACCELERATION;
        }


    }

    private void UpdateVelocity()
    {
        _velocity += _acceleration * Time.fixedDeltaTime;

        _velocity.z = (_acceleration.z == 0f || _velocity.z * _acceleration.z < 0) ? 0f : Mathf.Clamp(_velocity.z, -MAX_BACKWARD_VELOCITY, MAX_FORWARD_VELOCITY);
        _velocity.x = (_acceleration.x == 0f || _velocity.x * _acceleration.x < 0) ? 0f : Mathf.Clamp(_velocity.x, -MAX_STRAFE_VELOCITY, MAX_STRAFE_VELOCITY);
        _velocity.y = (_acceleration.y == 0f) ? -0.1f : Mathf.Clamp(_velocity.y, -MAX_FALL_VELOCITY, 0);
    }

    private void UpdatePosition()
    {
        Vector3 motion = _velocity * Time.fixedDeltaTime;

        _controller.Move(transform.TransformVector(motion));
    }


    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


}