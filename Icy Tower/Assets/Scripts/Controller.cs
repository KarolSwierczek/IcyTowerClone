using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float MaxSpeed = 20.0f;
    public float Acceleration = 50.0f;
    public float GroundDistance = 0.2f;
    public float JumpHeight = 7;
    public float Deceleration = 0.22f;
    public float SuperJumpHeight = 12.0f;
    public float Gravity = 40.0f;
    public LayerMask Ground;

    private Rigidbody _body;
    private float _input = 0.0f;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private float _horizontalVelocity = 0.0f;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        Physics.gravity = new Vector3(0, -1 * Gravity, 0);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        _input = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && _isGrounded)
        {
            if( Mathf.Abs(_horizontalVelocity) >= 25)
            {
                _body.AddForce(Vector3.up * Mathf.Sqrt(SuperJumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
            else
            {
                _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
            //clean up this mess!!!
        }
    }

    private void FixedUpdate()
    {
        if (_input * _horizontalVelocity < 0)
        {
            //when trying to change direction, reset horizontal velocity
            //_horizontalVelocity = 0;
            _input += Mathf.Abs(_body.velocity.x) * 0.6f * _input;
            //_input *= 5;
        }
        
        if (_input == 0) {

            //when no button is being pressed, the horizontal velocity decreases
            _input = -1 * Deceleration * _body.velocity.x;
        }
        _horizontalVelocity = SpeedLimit(_body.velocity.x + _input * Time.fixedDeltaTime * Acceleration);
        

        _body.velocity = new Vector3(_horizontalVelocity, _body.velocity.y, 0.0f);
    }

    private float SpeedLimit(float a)
    {
        if (Mathf.Abs(a) < MaxSpeed)
        {
            return a;
        }
        else
        {
            return MaxSpeed*Mathf.Sign(a);
        }
    }

}
