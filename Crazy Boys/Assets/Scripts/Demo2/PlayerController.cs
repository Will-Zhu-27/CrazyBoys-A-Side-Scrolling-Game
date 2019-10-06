using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardWalkSpeed;
    [SerializeField] private float backwardWalkSpeed;
    [SerializeField] private string forwardMoveInputName = "Horizontal";
    private CharacterController characterController;
    private Animator animator;
    private float forwardMoveInput;
    private int forwardMoveId;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        forwardMoveId = Animator.StringToHash("forwardMove");

    }

    // Update is called once per frame
    void Update()
    {
        forwardMoveInput = Input.GetAxis(forwardMoveInputName);
        animator.SetFloat(forwardMoveId, forwardMoveInput);
        CharacterMove();
    }
        private void CharacterMove() {
        float forwardMovement = 0f;
        if (forwardMoveInput > 0.05f) {
            forwardMovement = forwardMoveInput * forwardWalkSpeed;
        } else if (forwardMoveInput < -0.05f) {
            forwardMovement = forwardMoveInput * backwardWalkSpeed;
        }
        Vector3 forwardVect = transform.TransformDirection(Vector3.forward).normalized * forwardMovement;
        characterController.SimpleMove(forwardVect);
    }
}
