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

    private Rigidbody body;
    private Animator anim;
    private float input = 0.0f;
    private bool isGrounded = true;
    private Transform groundChecker;
    private float horizontalVelocity = 0.0f;
    private float verticalVelocity = 0.0f;
    private bool isJumping = false;
    private float timer = 0.0f;
    private float jumpModifier = 0.0f;


    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundChecker = transform.GetChild(0);
        timer = 0;
    }

    void Update()
    {
        isGrounded = (Physics.CheckSphere(groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore) && verticalVelocity <= 0);
        anim.SetBool("isGrounded", isGrounded);
        input = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && isGrounded && !isJumping)
        {
            //Jumping with constant velocity. 
            //Can't use AddForce, bc sometimes the vertical velocity doesn't reset fast enaugh when Player is grounded and it affects jumping velocity.
            verticalVelocity = JumpHeight / JumpTime * jumpModifier; 
            body.velocity = new Vector3(body.velocity.x, verticalVelocity, 0.0f);  
            isJumping = true;
            anim.Play("Jump");
        }

        if (isJumping)
        {
            //When the timer runs out, "gravity" is applied.
            timer += Time.deltaTime;
            if (timer >= JumpTime * jumpModifier)
            {
                isJumping = false;
                timer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        //jump modifier and animator bool parameter update.
        if (isGrounded)
        {
            jumpModifier = UpdateJumpModifier(horizontalVelocity);

            if (jumpModifier >= 1 + 0.8 * Flow)
            {
                anim.SetBool("isSuperJumping", true);
            }
            else
            {
                anim.SetBool("isSuperJumping", false);
            }
        }

        if (input * horizontalVelocity < 0)
        {
            //When trying to change direction, increase acceleration proportionally to current horizontal velocity value.
            input += Mathf.Abs(body.velocity.x) * ReverseSpeed * input;    
        }
        
        if (input == 0) {

            //When no button is being pressed, the horizontal velocity decreases. 
            //Similar to Physics friction, but it doesnt apply to normal movement.
            input = -1 * Deceleration * body.velocity.x;
        }
        horizontalVelocity = SpeedLimit(body.velocity.x + input * Time.fixedDeltaTime * Acceleration, MaxSpeed);


        if (!isGrounded && !isJumping)
        {
            //Falling. Have to use this over Physics gravity in order to get constant jumping velocity.
            verticalVelocity = SpeedLimit(body.velocity.y - Gravity*Time.fixedDeltaTime, MaxSpeed);
        }
        else
        {
            //Conserve vertical velocity.
            verticalVelocity = SpeedLimit(body.velocity.y, JumpHeight / JumpTime * jumpModifier);
        }

        //Velovity update. More convinient than using AddForce, because of the SpeedLimit.
        body.velocity = new Vector3(horizontalVelocity, verticalVelocity, 0.0f);
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
            return 1 + Flow*(Mathf.Abs(velocity) - MaxSpeed * FlowThreshold) / (MaxSpeed * (1-FlowThreshold));
        }
        return 1;
    }

}
