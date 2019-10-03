using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float walkForwardSpeed;
    public float walkBackwardSpeed;
    public float runSpeed;
    public KeyCode jumpKeyCode;
    public KeyCode runKeyCode;
    public Animator animator;
    public CharacterController characterController;
    private float inputX;
    private float inputZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        animator.SetFloat("inputX", inputX);
        animator.SetFloat("inputZ", inputZ);
        float moveZ;
        if (inputZ > 0) {
            moveZ = inputZ * Time.deltaTime * walkForwardSpeed;
        } else {
             moveZ = inputZ * Time.deltaTime * walkBackwardSpeed;
        }
        if (Input.GetKey(runKeyCode)) {
            animator.SetBool("isRun", true);
        } else {
            animator.SetBool("isRun", false);
        }
        if (Input.GetKey(jumpKeyCode)) {
            animator.SetBool("isJump", true);
        } else {
            animator.SetBool("isJump", false);
        }

        float moveX = inputX * Time.deltaTime * walkForwardSpeed;
        
        Vector3 move = this.transform.forward * moveZ + this.transform.right * moveX;
        characterController.Move(move);

    }
}
