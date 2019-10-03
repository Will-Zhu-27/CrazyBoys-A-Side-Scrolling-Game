using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private string forwardInputName;
    [SerializeField] private string rightInputName;
    public float walkForwardSpeed;
    public float walkBackwardSpeed;
    public float runSpeed;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float runJumpMultiplier;
    [SerializeField] private KeyCode jumpKeyCode;
    [SerializeField] private KeyCode runKeyCode;
    public Animator animator;
    public CharacterController characterController;
    private float forwardInput;
    private float rightInput;
    private bool isWalk = false;
    private bool isJump = false;
    private bool isRun = false;

    void Start() {
        // Debug.Log(transform.TransformDirection(Vector3.forward).normalized);
    }
    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis(forwardInputName);
        rightInput = Input.GetAxis(rightInputName);

        animator.SetFloat("forwardInput", forwardInput);
        animator.SetFloat("rightInput", rightInput);

        if (forwardInput >= -0.1 && forwardInput <= 0.1) {
            isWalk = false;  
        } else {
            isWalk = true;
        }
        animator.SetBool("isWalk", isWalk);

        if (Input.GetKey(runKeyCode)) {
            isRun = true;
        } else {
            isRun = false;
        }
        animator.SetBool("isRun", isRun);

        if (!isJump && Input.GetKey(jumpKeyCode)) {
            Debug.Log("jump event!");
            isJump = true;
            StartCoroutine(JumpEvent());
        }
        animator.SetBool("isJump", isJump);

        CharacterMove();
    }

    private void CharacterMove() {
        float forwardMovement = 0f, rightMovement = 0f;
        if (forwardInput > 0.1) {
            forwardMovement = forwardInput * walkForwardSpeed;
        } else if (forwardInput < -0.1) {
            forwardMovement = forwardInput * walkBackwardSpeed;
        }
        rightMovement = rightInput * walkForwardSpeed;
        Vector3 forwardVect = transform.TransformDirection(Vector3.forward).normalized * forwardMovement;
        Vector3 rightVect = transform.TransformDirection(Vector3.right).normalized * rightMovement;
        characterController.SimpleMove(forwardVect + rightVect);
    }

    private IEnumerator JumpEvent() {
        characterController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            if (isRun) {
                characterController.Move(Vector3.up * jumpForce * runJumpMultiplier * Time.deltaTime); 
            } else {
               characterController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime); 
            }
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!characterController.isGrounded && characterController.collisionFlags != CollisionFlags.Above);

        characterController.slopeLimit = 45.0f;
        isJump = false;
    }
}
