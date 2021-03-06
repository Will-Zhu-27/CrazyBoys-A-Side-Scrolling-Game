﻿/**
 * Player Move controller to handle all the user input lead to player movement
 * Creator: Yuqiang Zhu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private bool isFaceForward = true;
    [SerializeField] private float forwardSpeed = 2f;
    [SerializeField] private float backwardSpeed = 1f;
    [SerializeField] private float crouchingForwardSpeed = 1.5f;
    [SerializeField] private float crouchingBackwardSpeed = 0.75f;
    // [SerializeField] private float runSpeed = 4f;
    [SerializeField] private string forwardMoveInputName = "Horizontal";
    [SerializeField] private KeyCode crouchKeyCode = KeyCode.S;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0;
    [SerializeField] private KeyCode quickRollKeyCode = KeyCode.Mouse3;
    [SerializeField] private KeyCode spinKeyCode = KeyCode.W;
    [SerializeField] private Transform trackObj;
    [SerializeField] private float trunAroundThreshold = -0.1f;
    [SerializeField] private float turnAroundTime = 0.3f;
    [SerializeField] private float standColliderHeight = 1.81f;
    [SerializeField] private Vector3 standColliderCenter = Vector3.zero;
    private Vector3 standHitBoxCenter;
    private Vector3 standHitBoxSize;
    [SerializeField] private float crouchingColliderHeight = 1.27f;
    [SerializeField] private Vector3 crouchingColliderCenter = Vector3.zero;
    [SerializeField] private Vector3 crouchHitBoxCenter = Vector3.zero;
    [SerializeField] private Vector3 crouchHitBoxSize = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;
    private BoxCollider hitBox;
    private float forwardMoveInput;
    private int forwardMoveId;
    private int isCrouchId;
    private bool isCrouch = false;
    private int isRunId;
    private bool isRun = false;
    private bool isShooting;
    private bool isTurningAround = false;
    private float startRotateTime = 0f;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private Vector3 targetDirection;
    private bool isRoll;
    private int isRollId;
    [SerializeField] private bool isAutoTrunAround = true;
    [SerializeField] private KeyCode jumpKeyCode = KeyCode.Space;
    [SerializeField] private bool isJump = false;
    private int isJumpId;
    [SerializeField] private float jumpSpeed = 8.0f;
    private Vector3 move = Vector3.zero;
    [SerializeField] private float gravity = 20.0f;
    public AudioSource moveAudioSource;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip walkClip;
    private bool isSpin;
    private int isSpinId;
    private bool isCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        hitBox = this.GetComponent<BoxCollider>();
        standHitBoxCenter = hitBox.center;
        standHitBoxSize = hitBox.size;
        CroushOnCollider(this.isCrouch);
        animator = GetComponent<Animator>();
        forwardMoveId = Animator.StringToHash("forwardMove");
        isCrouchId = Animator.StringToHash("isCrouch");
        isRunId = Animator.StringToHash("isRun");
        isRollId = Animator.StringToHash("isRoll");
        isJumpId = Animator.StringToHash("isJump");
        isSpinId = Animator.StringToHash("isSpin");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayerActive) {
            HandleUserInput();
            AutoTurnAround();
        }
        
        CharacterMove();
        // // Physics.Raycast (this.transform.position, Vector3.up, 15.0f)
        // RaycastHit hit;
        // Debug.DrawLine(transform.position, transform.position + Vector3.up * 4, Color.black);
        
        // if(Physics.Raycast(transform.position, Vector3.up, out hit, 4.0f, this.gameObject.layer)){
        //     // if (hit.transform.gameObject.layer == 8) {
        //         print("collide!!!");
        //         print(hit.transform.gameObject.layer);
        //     // }
        // }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
    }

    private void CharacterMove()
    {
        if (GameManager.Instance.isPlayerActive)
        {
            
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName("Kick") && characterController.isGrounded)
            {
                // when player is kicking, stop move
                animator.SetBool(isCrouchId, false);
                move = Vector3.zero;
            }
            else
            {
                float xInput = Input.GetAxis("Horizontal");
                if (characterController.isGrounded) {
                    move = new Vector3(xInput, 0.0f, 0.0f);
                } else {
                    move.x = xInput;
                }
                
                if (isFaceForward)
                {
                    if (xInput > 0.05)
                    {
                        if (isCrouch)
                        {
                            move.x *= crouchingForwardSpeed;
                        }
                        else
                        {
                            move.x *= forwardSpeed;
                        }
                    }
                    else if (xInput < -0.05)
                    {
                        if (isCrouch)
                        {
                            move.x *= crouchingBackwardSpeed;
                        }
                        else
                        {
                            move.x *= backwardSpeed;
                        }
                    }
                    else
                    {
                        move.x = 0.0f;
                    }
                }
                else
                {
                    if (xInput > 0.05)
                    {
                        if (isCrouch)
                        {
                            move.x *= crouchingBackwardSpeed;
                        }
                        else
                        {
                            move.x *= backwardSpeed;
                        }
                    }
                    else if (xInput < -0.05)
                    {
                        if (isCrouch)
                        {
                            move.x *= crouchingForwardSpeed;
                        }
                        else
                        {
                            move.x *= forwardSpeed;
                        }
                    }
                    else
                    {
                        move.x = 0.0f;
                    }
                }
            }

            if (!animatorStateInfo.IsName("Quick Roll") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling") && Input.GetKeyDown(jumpKeyCode) && characterController.isGrounded)
            {
                move.y = jumpSpeed;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Falling")) {
                    StartCoroutine(JumpEvent());
                }
                animator.SetBool(isJumpId, isJump);
            }
        }
        else
        {
            move.x = 0.0f;
        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        move.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(move * Time.deltaTime);
    }

    private void HandleUserInput() {
        forwardMoveInput = Input.GetAxis(forwardMoveInputName);
        if (!isFaceForward) {
            forwardMoveInput = -forwardMoveInput;
        }
        animator.SetFloat(forwardMoveId, forwardMoveInput);

        // judge crouch
        bool canStand = true;
        if (isCrouch) {
            canStand = this.CanStand();
        }
        if (Input.GetKey(crouchKeyCode)) {
            isCrouch = true;
        } else if(canStand) {
            isCrouch = false;
        } else {
            isCrouch = true;
        }
        
        CroushOnCollider(isCrouch);
        animator.SetBool(isCrouchId, isCrouch);



        // judge shooting
        if (Input.GetKeyDown(shootingKeyCode)) {
            isShooting = true;
        } else if (Input.GetKeyUp(shootingKeyCode)) {
            isShooting = false;
        }

        // quick roll
        if (Input.GetKeyDown(quickRollKeyCode)) {
            // print("quick roll");
            isRoll = true;
        } else if (Input.GetKeyUp(quickRollKeyCode)) {
            isRoll = false;
        }
        animator.SetBool(isRollId, isRoll);

        // jump
        // if (!this.isJump && Input.GetKeyDown(jumpKeyCode) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling")) {
        //     StartCoroutine(JumpEvent());
        // }
        // animator.SetBool(isJumpId, isJump);

        // spin
        if (!isCoolDown) {
           if (Input.GetKey(spinKeyCode)) {
                StartCoroutine(spinEvent());
            }
        }
        

        // paly move audio
        if (isCrouch && characterController.isGrounded && (forwardMoveInput >= 0.05 || forwardMoveInput <= -0.05)) {
            if (moveAudioSource.clip != walkClip) {
                moveAudioSource.clip = walkClip;
                moveAudioSource.Play();
            }
        } else if (characterController.isGrounded && forwardMoveInput >= 0.05) {
            if (moveAudioSource.clip != runClip) {
                moveAudioSource.clip = runClip;
                moveAudioSource.Play();
            }
        } else if (characterController.isGrounded && forwardMoveInput <= -0.05) {
            if (moveAudioSource.clip != walkClip) {
                moveAudioSource.clip = walkClip;
                moveAudioSource.Play();
            }
        } else {
            moveAudioSource.clip = null;
        }
    }

    IEnumerator spinEvent() {
        isCoolDown = true;
        isSpin = true;
        animator.SetBool(isSpinId, isSpin);
        while(true) {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Spin")){
                yield return null;
            } else {
                break;
            }
        }
        isSpin = false;
        animator.SetBool(isSpinId, isSpin);
        while(true) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spin") || animator.IsInTransition(0)) {
                yield return null;
            } else {
                break;
            }
        }
        yield return new WaitForSeconds(GameManager.Instance.spinCoolDown);
        isCoolDown = false;
    }

    IEnumerator JumpEvent() {
        this.isJump = true;
        while(characterController.isGrounded) {
            yield return null;
        }
        while(!characterController.isGrounded) {
            yield return null;
        }
        this.isJump = false;
        animator.SetBool(isJumpId, isJump);

    }

    private void AutoTurnAround() {
        if (!isAutoTrunAround) {
            return;
        }
        if (!isTurningAround && this.transform.InverseTransformPoint(trackObj.position).z < trunAroundThreshold && !animator.GetCurrentAnimatorStateInfo(0).IsName("Spin")) {
            // print("turn around event start");
            StartCoroutine(TurnAroundEvent());
        }
    }

    IEnumerator TurnAroundEvent() {
        
        isTurningAround = true;
        startRotateTime = Time.time;
        float l;
        if (isFaceForward)
        {
            startRotation = Quaternion.Euler(0, 90f, 0);
            endRotation = Quaternion.Euler(0, 270f, 0);
            targetDirection = new Vector3(0, 270f, 0);
        }
        else
        {
            startRotation = Quaternion.Euler(0, 270f, 0);
            endRotation = Quaternion.Euler(0, 90f, 0);
            targetDirection = new Vector3(0, 90f, 0);
        }
        do {
            l = Mathf.InverseLerp(startRotateTime, startRotateTime + turnAroundTime, Time.time);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, l);
            yield return null;
        } while(transform.rotation !=  endRotation);
        isTurningAround = false;
        isFaceForward = !isFaceForward;
        // print("turn around event end");
    }

    private void CroushOnCollider(bool isCrouch) {
        if (isCrouch) {
            this.characterController.center = crouchingColliderCenter;
            this.characterController.height = this.crouchingColliderHeight;
            this.hitBox.center = this.crouchHitBoxCenter;
            this.hitBox.size = this.crouchHitBoxSize;
        } else {
            this.characterController.center = standColliderCenter;
            this.characterController.height = this.standColliderHeight;
            this.hitBox.center = this.standHitBoxCenter;
            this.hitBox.size = this.standHitBoxSize;
        }
    }

    private float standHeight = 1.88f;
    public LayerMask detectLayer;
    private bool CanStand() {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), standHeight, detectLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * standHeight, Color.yellow);
            // Debug.Log("Did Hit");
            // Debug.Log(hit.transform.position);
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * standHeight, Color.white);
            // Debug.Log("Did not Hit");
            return true;
        }
    }
}
