/**
 * Player controller to handle all the user input
 * Creator: Yuqiang Zhu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isFaceForward = true;
    [SerializeField] private float forwardWalkSpeed;
    [SerializeField] private float backwardWalkSpeed;
    [SerializeField] private float crouchingForwardWalkSpeed;
    [SerializeField] private float crouchingBackwardWalkSpeed;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private string forwardMoveInputName = "Horizontal";
    [SerializeField] private KeyCode crouchKeyCode = KeyCode.S;
    [SerializeField] private KeyCode runKeyCode = KeyCode.LeftShift;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0;
    [SerializeField] private Transform trackObj;
    [SerializeField] private float trunAroundThreshold = 0f;
    [SerializeField] private float turnAroundSpeed = 0.01f;
    [SerializeField] private float turnAroundTime = 0.3f;
    private CharacterController characterController;
    private Animator animator;
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

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        forwardMoveId = Animator.StringToHash("forwardMove");
        isCrouchId = Animator.StringToHash("isCrouch");
        isRunId = Animator.StringToHash("isRun");
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();
        AutoTurnAround();
        CharacterMove();
    }

    private void CharacterMove() {
        if (isTurningAround) {
            return;
        }
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
    }

    private void AutoTurnAround() {
        if (!isTurningAround && this.transform.InverseTransformPoint(trackObj.position).z < trunAroundThreshold) {
            isTurningAround = true;
            startRotateTime = Time.time;
        }
        if (isTurningAround) {
            TurnAround();
        }
    }

    private void TurnAround()
    {
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
        float l = Mathf.InverseLerp(startRotateTime, startRotateTime + turnAroundTime, Time.time);
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, l);
        if (this.transform.rotation.eulerAngles.Equals(targetDirection)) {
            isTurningAround = false;
            isFaceForward = !isFaceForward;
        }
    }
}
