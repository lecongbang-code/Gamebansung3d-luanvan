using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    float accelerration = 2.0f;
    float deceleration = 2.0f;
    float maximumWalkVelocity = 1.0f;
    float maximumRunVelocity = 2.0f;
    float groundDistance = 0.4f;

    int VelocityZHash;
    int VelocityXHash;

    float speed;
    float speedWalk;
    float speedRun;
    float speedAiming;
    float gravity = -9.81f;
    float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;
    float timeWalk = 0f;

    public LayerMask groundMask;

    public Transform groundCheck;

    public PlayerController playerController;

    PlayerInput playerInput;
    CharacterController controller;
    PlayerAiming playerAiming;
    PlayerAnimation playerAnimation;
    
    public AudioManager audioManager;
    public AudioMoveMent audioMoveMent;

    void Start() 
    {
        playerInput = GetComponentInParent<PlayerInput>();
        controller = GetComponentInParent<CharacterController>();
        playerAiming = GetComponentInParent<PlayerAiming>();
        playerAnimation = GetComponentInParent<PlayerAnimation>();

        VelocityZHash = Animator.StringToHash("Vertical");
        VelocityXHash = Animator.StringToHash("Horizontal");

        speedRun = playerController.speedMove;
        speedWalk = speedRun - speedRun * 0.3f;
        speedAiming = speedRun - speedRun * 0.7f;


    }
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool runPressed = Input.GetKey(KeyCode.RightShift);
        bool run = Input.GetKeyDown(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        changeVelocity(forwardPressed, backPressed, rightPressed, leftPressed, runPressed, currentMaxVelocity);
        
        lockOrResetVelocity(forwardPressed, backPressed, rightPressed, leftPressed, runPressed, currentMaxVelocity);

        playerAnimation.animator.SetFloat(VelocityZHash, velocityZ);
        
        playerAnimation.animator.SetFloat(VelocityXHash, velocityX);

        CheckAiming();

        Jump();

        Run(run);

        SelectAudioMove();
    }

    void Run(bool run)
    {
        if(run && velocityZ > 0f && playerInput.timeShoot > 1.7f)
        {
            if(!playerController.run)
            {
                playerController.run = true;
                playerController.aiming = false;
                playerAiming.OnUnScoped();
                playerController.reload = false;
                playerAnimation.animator.SetBool("Reload", false);
            }
            else
            {
                playerController.run = false;
                audioManager.Play("OffScope");
            }
        }
        if(velocityZ <= 0f)
        {
            playerController.run = false;
        }
        
        playerAnimation.animator.SetBool("Run", playerController.run);
    }

    void CheckAiming()
    {
        if(playerController.aiming)
        {
            speed = speedAiming;
            gravity = -9.81f * 2f;
            jumpHeight = 2f;
        }
        else if(playerController.run)
        {
            speed = speedRun;
            gravity = -9.81f * 2f;
            jumpHeight = 3f;
        }
        else
        {
            speed = speedWalk;
            gravity = -9.81f * 2f;
            jumpHeight = 2.8f;
        }
    }

    public void SelectAudioMove()
    {
        timeWalk += Time.deltaTime;

        if(playerController.run)
        {
            if(Input.GetKey(KeyCode.W) && timeWalk > 0.3f && isGrounded)
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.S) && timeWalk > 0.4f && isGrounded && !Input.GetKey(KeyCode.W))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.A) && timeWalk > 0.4f && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.D) && timeWalk > 0.4f && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }
        }
        else if(!playerController.run && !playerController.aiming)
        {
            if(Input.GetKey(KeyCode.W) && timeWalk > 0.45f && isGrounded)
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.S) && timeWalk > 0.55f && isGrounded && !Input.GetKey(KeyCode.W))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.A) && timeWalk > 0.55f && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.D) && timeWalk > 0.55f && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.W) && timeWalk > 0.8f && isGrounded)
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.S) && timeWalk > 0.85f && isGrounded && !Input.GetKey(KeyCode.W))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.A) && timeWalk > 0.85f && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }

            if(Input.GetKey(KeyCode.D) && timeWalk > 0.85f && isGrounded && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                audioMoveMent.Step();
                timeWalk = 0f;
            }
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector3 move = transform.right * velocityX + transform.forward * velocityZ;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            playerAnimation.animator.SetBool("Jump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            audioManager.Play("Jump");
        }
        else
        {
           playerAnimation.animator.SetBool("Jump", false); 
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void changeVelocity(bool forwardPressed, bool backPressed, bool rightPressed, bool leftPressed, bool runPressed, float currentMaxVelocity)
    {
        if(forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * accelerration;
        }

        if(backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * accelerration;
        }

        if(rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * accelerration;
        }

        if(leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * accelerration;
        }

        if(!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if(!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        if(!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if(!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool backPressed, bool rightPressed, bool leftPressed, bool runPressed, float currentMaxVelocity)
    {
        if(!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        if(!rightPressed && !leftPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        if(forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if(forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;

            if(velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        if(backPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if(backPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;

            if(velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        else if(backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }

        if(leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if(leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;

            if(velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if(leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        if(rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if(rightPressed && velocityX > deceleration)
        {
            velocityX -= Time.deltaTime *deceleration;
            
            if(velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05))
            {
                velocityX =currentMaxVelocity;
            }
        }
        else if(rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }
}
