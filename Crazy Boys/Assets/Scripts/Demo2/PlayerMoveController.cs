/**
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
    [SerializeField] private KeyCode runKeyCode = KeyCode.LeftShift;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0;
    [SerializeField] private KeyCode quickRollKeyCode = KeyCode.Mouse3;
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
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();
        AutoTurnAround();
        CharacterMove();
    }
    
    private void CharacterMove() {
        if (characterController.isGrounded) {
            // We are grounded, so recalculate
            // move direction directly from axes
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName("Kick"))
            {
                // when player is kicking, stop move
                animator.SetBool(isCrouchId, false);
                move = Vector3.zero;
            }
            else
            {
                float xInput = Input.GetAxis("Horizontal");
                move = new Vector3(xInput, 0.0f, 0.0f);
                if (isFaceForward)
                {
                    if (xInput > 0.05)
                    {
                        if (isCrouch) {
                            move *= crouchingForwardSpeed;
                        } else {
                            move *= forwardSpeed;
                        }
                    }
                    else if (xInput < -0.05)
                    {
                        if (isCrouch) {
                            move *= crouchingBackwardSpeed;
                        } else {
                            move *= backwardSpeed;
                        }
                    }
                    else
                    {
                        move = Vector3.zero;
                    }
                }
                else
                {
                    if (xInput > 0.05)
                    {
                        if (isCrouch) {
                            move *= crouchingBackwardSpeed;
                        } else {
                            move *= backwardSpeed;
                        }
                    }
                    else if (xInput < -0.05)
                    {
                        if (isCrouch) {
                            move *= crouchingForwardSpeed;
                        } else {
                            move *= forwardSpeed;
                        }
                    }
                    else
                    {
                        move = Vector3.zero;
                    }
                }
            }
            
            if (!animatorStateInfo.IsName("Quick Roll") && Input.GetKeyDown(jumpKeyCode))
            {
                move.y = jumpSpeed;
            }
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
        if (Input.GetKeyDown(crouchKeyCode)) {
            isCrouch = true;

        } else if (Input.GetKeyUp(crouchKeyCode)) {
            isCrouch = false;
        }
        CroushOnCollider(isCrouch);
        animator.SetBool(isCrouchId, isCrouch);

        // judge run
        if (Input.GetKeyDown(runKeyCode)) {
            isRun = true;
        } else if (Input.GetKeyUp(runKeyCode)) {
            isRun = false;
        }
        animator.SetBool(isRunId, isRun);

        // judge shooting
        if (Input.GetKeyDown(shootingKeyCode)) {
            isShooting = true;
        } else if (Input.GetKeyUp(shootingKeyCode)) {
            isShooting = false;
        }

        // quick roll
        if (Input.GetKeyDown(quickRollKeyCode)) {
            print("quick roll");
            isRoll = true;
        } else if (Input.GetKeyUp(quickRollKeyCode)) {
            isRoll = false;
        }
        animator.SetBool(isRollId, isRoll);

        // jump
        if (!this.isJump && !this.isCrouch && Input.GetKey(jumpKeyCode) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling")) {
            StartCoroutine(JumpEvent());
        }
        animator.SetBool(isJumpId, isJump);
    }

    IEnumerator JumpEvent() {
        this.isJump = true;
        while(!characterController.isGrounded) {
            yield return null;
        }
        this.isJump = false;
    }

    private void AutoTurnAround() {
        if (!isAutoTrunAround) {
            return;
        }
        if (!isTurningAround && this.transform.InverseTransformPoint(trackObj.position).z < trunAroundThreshold) {
            print("turn around event start");
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
        print("turn around event end");
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

}
