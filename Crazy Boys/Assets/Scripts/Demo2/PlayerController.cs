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
    private CharacterController characterController;
    private Animator animator;
    private float forwardMoveInput;
    private int forwardMoveId;
    private int isCrouchId;
    private bool isCrouch = false;
    private int isRunId;
    private bool isRun = false;
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
        forwardMoveInput = Input.GetAxis(forwardMoveInputName);
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

        CharacterMove();
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
}
