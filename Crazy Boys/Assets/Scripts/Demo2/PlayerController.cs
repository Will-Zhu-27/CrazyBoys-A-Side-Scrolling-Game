using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardWalkSpeed;
    [SerializeField] private float backwardWalkSpeed;
    [SerializeField] private float crouchingForwardWalkSpeed;
    [SerializeField] private float crouchingBackwardWalkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private string forwardMoveInputName = "Horizontal";
    [SerializeField] private KeyCode crouchKeyCode = KeyCode.S;
    [SerializeField] private KeyCode runKeyCode = KeyCode.LeftShift;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0;
    [SerializeField] private Transform trackObj;
    [SerializeField] private float trunBackThreshold = 0f;
    [SerializeField] private float homingSpeed = 0.125f;
    private CharacterController characterController;
    private Animator animator;
    private float forwardMoveInput;
    private int forwardMoveId;
    private int isCrouchId;
    private bool isCrouch = false;
    private int isRunId;
    private bool isRun = false;
    private int isTurnBackId;
    private bool isTurnBack;
    private bool isFaceForward;
    private int isFaceForwardId;
    private int isShootingId;
    private bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        forwardMoveId = Animator.StringToHash("forwardMove");
        isCrouchId = Animator.StringToHash("isCrouch");
        isRunId = Animator.StringToHash("isRun");
        isTurnBackId = Animator.StringToHash("isTurnBack");
        isFaceForwardId = Animator.StringToHash("isFaceForward");
        isFaceForward = animator.GetBool(isFaceForwardId);
        isShootingId = Animator.StringToHash("isShooting");
    }

    // Update is called once per frame
    void Update()
    {
        isFaceForward = animator.GetBool(isFaceForwardId);
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
        } else if (Input.GetKeyUp(shootingKeyCode)){
            isShooting = false;
        }
        animator.SetBool(isShootingId, isShooting);

        detectTurnBack();
        CharacterMove();
    }

    void FixedUpdate() {
        Quaternion quaternion;
        if (isFaceForward) {
            quaternion = new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);
        } else {
            quaternion = new Quaternion(0.0f, 0.7f, 0.0f, -0.7f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, homingSpeed);
    }

    private void CharacterMove() {
        float forwardMovement = 0f;
        if (forwardMoveInput > 0.05f) {
            if (isRun && !isCrouch) {
                forwardMovement = forwardMoveInput * runSpeed;
            } else if (!isCrouch){
                forwardMovement = forwardMoveInput * forwardWalkSpeed;
            } else {
                forwardMovement = forwardMoveInput * crouchingForwardWalkSpeed;
            }
        } else if (forwardMoveInput < -0.05f) {
            if (!isCrouch) {
                forwardMovement = forwardMoveInput * backwardWalkSpeed;
            } else {
                forwardMovement = forwardMoveInput * crouchingBackwardWalkSpeed;
            }
            
        }
        Vector3 forwardVect = transform.TransformDirection(Vector3.forward).normalized * forwardMovement;
        characterController.SimpleMove(forwardVect);
    }

    private void detectTurnBack() {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Turn 180") && this.transform.InverseTransformPoint(trackObj.position).z < trunBackThreshold) {
            isTurnBack = true;
        } else {
            isTurnBack = false;
        }
        animator.SetBool(isTurnBackId, isTurnBack);
    }
}
