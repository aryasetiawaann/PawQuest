using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    // deklarasi reference variables
    PlayerInput playerInput;
    CharacterController heroController;
    Animator animator;

    // Variabel untuk menyimpan nilai player input
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 cameraRelativeMovement;
    bool isMovementPressed;

    // Variabel untuk berlari
    bool isRunPressed;
    float rotationFactorPerFrame = 5.0f;
    float runSpeed = 6.0f;
    float walkSpeed = 3.0f;

    // Variabel untuk melompat
    bool isJumpPressed = false;
    bool isJumping = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1.5f;
    float maxJumpTime = 0.5f;

    // Variabel untuk Menghindar
    bool isDodgePressed;
    bool isDodging = false;
    float dodgeSpeed = 6.0f;
    float dodgeDuration = 0.5f;

    // Variabel untuk Attack
    public bool isAttacking;
    public bool isDead = false;


    // Gravitasi
    float groundedGravity = -0.05f;
    float gravity = -9.8f;


    void Awake()
    {
        playerInput = new PlayerInput();
        heroController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        isAttacking = false;

        playerInput.HeroControls.Move.started += onMovementInput;
        playerInput.HeroControls.Move.canceled += onMovementInput;
        playerInput.HeroControls.Move.performed += onMovementInput;
        playerInput.HeroControls.Run.started += onRun;
        playerInput.HeroControls.Run.canceled += onRun;
        playerInput.HeroControls.Jump.started += onJump;
        playerInput.HeroControls.Jump.canceled += onJump;
        playerInput.HeroControls.Dodge.started += onDodge;
        playerInput.HeroControls.Dodge.canceled += onDodge;

        setupJumpVariable();
    }

    void Update()
    {
        handleRotation();
        handleAnimation();

        if(!isDead){
            if(isRunPressed){
                cameraRelativeMovement = ConvertToCameraSpace(currentRunMovement);
                heroController.Move(cameraRelativeMovement * Time.deltaTime);
            }
            else{
                cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
                heroController.Move(cameraRelativeMovement * Time.deltaTime);

            }

            if(playerInput.HeroControls.Attack.triggered){
                handleAttack();
            }

            handleJumping();
        }else{
            animator.SetBool("isDeath", true);
        }


        handleGravity();
    }

    void setupJumpVariable(){
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void onJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
    }

    void onDodge(InputAction.CallbackContext context){
        isDodgePressed = context.ReadValueAsButton();

         if (isDodgePressed && !isDodging)
        {
            isDodging = true;
            animator.SetBool("isDodging", true);
            StartCoroutine(handleDodging());
        }else if(!isDodgePressed && isDodging){
            isDodging = false;
            animator.SetBool("isDodging", false);
        }
    }

    void onRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }

    void onMovementInput(InputAction.CallbackContext context) {
        currentMovementInput = context.ReadValue<Vector2>();
        // Set jalan
        currentMovement.x = currentMovementInput.x * walkSpeed;
        currentMovement.z = currentMovementInput.y * walkSpeed;

        // Set lari
        currentRunMovement.x = currentMovementInput.x * runSpeed;
        currentRunMovement.z = currentMovementInput.y * runSpeed;

        // Kalo player gak pencet WASD maka dia False dan sebaliknya
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleGravity(){
        if (heroController.isGrounded){
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }else {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
    }

    void handleAttack(){

        if (isAttacking) return;

        isAttacking = true;
        animator.SetInteger("attackCount", UnityEngine.Random.Range(0, 2));
        animator.SetBool("isAttacking", true);
        StartCoroutine(ResetAttackState());
    
    }

    IEnumerator ResetAttackState()
    {
        float attackDuration = 25f / 1.5f; // Durasi dalam frame dibagi kecepatan animasi
        yield return new WaitForSeconds(attackDuration / 60f);
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    void handleJumping(){
        if (!isJumping && heroController.isGrounded && isJumpPressed){
            isJumping = true;
            animator.SetBool("isJumping", true);
            currentMovement.y = initialJumpVelocity * 0.5f;
            currentRunMovement.y = initialJumpVelocity * 0.5f;
        }else if(isJumping && heroController.isGrounded && !isJumpPressed){
            isJumping = false;

            animator.SetBool("isJumping", false);
        }
    }

    IEnumerator handleDodging(){

        float elapsedTime = 0f;
        Vector3 dodgeDirection = transform.forward; 

        while (elapsedTime < dodgeDuration)
        {
            heroController.Move(dodgeDirection * dodgeSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }

    void handleAnimation(){
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        // handle animasi Walking
        if (isMovementPressed && !isWalking){
            animator.SetBool("isWalking", true);
        }
        else if(!isMovementPressed && isWalking){
            animator.SetBool("isWalking", false);
        }

        // handle animasi Running
        if((isMovementPressed && isRunPressed) && !isRunning){
            animator.SetBool("isRunning", true);
        }
        else if((!isMovementPressed || !isRunPressed) && isRunning){
            animator.SetBool("isRunning", false);
        }
    }

    void handleRotation(){
        Vector3 positionToLookAt;
        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed){
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    Vector3 ConvertToCameraSpace(Vector3 vector){
        float currentYValue = vector.y;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZProduct = vector.z * cameraForward;
        Vector3 cameraRightXProduct = vector.x * cameraRight;

        Vector3 vectorRotated = cameraForwardZProduct + cameraRightXProduct;
        vectorRotated.y = currentYValue;
        return vectorRotated;
    }

    void OnEnable()
    {
        playerInput.HeroControls.Enable();
    }

    void OnDisable()
    {
        playerInput.HeroControls.Disable();

    }
}
