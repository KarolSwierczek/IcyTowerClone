using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float MaxSpeed = 30.0f;
    public float Acceleration = 40.0f;
    public float Deceleration = 0.12f;
    public float ReverseSpeed = 0.6f;
    public float JumpTime = 3.0f;
    public float JumpHeight = 5.0f;
    //Flow is how much horizontal velocity affects jumping
    public float Flow = 3.0f;
    public float FlowThreshold = 0.7f;
    public float GroundDistance = 0.2f;
    public float Gravity = 75.0f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Collider _collider;
    private float _input = 0.0f;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private float _horizontalVelocity = 0.0f;
    private float _verticalVelocity = 0.0f;
    private bool _isJumping = false;
    private float _timer = 0.0f;
    private float _jumpModifier = 0.0f;


    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _groundChecker = transform.GetChild(0);
        _timer = 0;
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        _input = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && _isGrounded && !_isJumping)
        {
            //Jumping with constant velocity. 
            //Can't use AddForce, bc sometimes the vertical velocity doesn't reset fast enaugh when Player is grounded and it affects jumping velocity.
            _verticalVelocity = JumpHeight / JumpTime * (1 + _jumpModifier); 
            _body.velocity = new Vector3(_body.velocity.x, _verticalVelocity, 0.0f);  
            _isJumping = true;
        }

        if (_isJumping)
        {
            //When the timer runs out, "gravity" is applied.
            _timer += Time.deltaTime;
            if (_timer >= JumpTime * (1 + 0.5f * _jumpModifier))
            {
                _isJumping = false;
                _timer = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        //jump modifier update.
        _jumpModifier = UpdateJumpModifier(_horizontalVelocity);
        _collider.material.bounciness = 0.6f + 0.4f*_jumpModifier / Flow;

        if (_input * _horizontalVelocity < 0)
        {
            //When trying to change direction, increase acceleration proportionally to current horizontal velocity value.
            _input += Mathf.Abs(_body.velocity.x) * ReverseSpeed * _input;    
        }
        
        if (_input == 0) {

            //When no button is being pressed, the horizontal velocity decreases. 
            //Similar to Physics friction, but it doesnt apply to normal movement.
            _input = -1 * Deceleration * _body.velocity.x;
        }
        _horizontalVelocity = SpeedLimit(_body.velocity.x + _input * Time.fixedDeltaTime * Acceleration, MaxSpeed);


        if (!_isGrounded && !_isJumping)
        {
            //Falling. Have to use this over Physics gravity in order to get constant jumping velocity.
            _verticalVelocity = SpeedLimit(_body.velocity.y - Gravity*Time.fixedDeltaTime, MaxSpeed);
        }
        else
        {
            //Conserve vertical velocity.
            _verticalVelocity = SpeedLimit(_body.velocity.y, JumpHeight / JumpTime);
        }

        //Velovity update. More convinient than using AddForce, because of the SpeedLimit.
        _body.velocity = new Vector3(_horizontalVelocity, _verticalVelocity, 0.0f);
    }

    
    //Returns speed or limit with speeds' direction.
    private float SpeedLimit(float speed, float limit)
    {
        if (Mathf.Abs(speed) < limit)
        {
            return speed;
        }
        else
        {
            return limit*Mathf.Sign(speed);
        }
    }

    //How much current velocity goes over a threshold.
    private float UpdateJumpModifier(float velocity)
    {
        if (Mathf.Abs(velocity) > FlowThreshold * MaxSpeed)
        {
            return Flow*(Mathf.Abs(velocity) - MaxSpeed * FlowThreshold) / (MaxSpeed * FlowThreshold);
        }
        return 0;
    }

}
