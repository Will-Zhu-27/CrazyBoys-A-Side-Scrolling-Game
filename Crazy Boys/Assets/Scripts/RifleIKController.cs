using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RifleIKController : MonoBehaviour
{
    private Animator animator;
    public bool ikActive = false;
    [SerializeField] private Transform lookAim = null;
    public bool isHeadWatch = false;
    public bool isLeftHandToward = false;
    public float leftHandRotationWeight = 0f;
    public Transform leftHandAim;
    [SerializeField] private Vector3 leftHandAimOffset = Vector3.zero;
    public bool isRightHandToward = false;
    public float rightHandRotationWeight = 0f;
    public Transform rightHandAim;
    [SerializeField] private Vector3 rightHandAimOffset = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (ikActive)
        {
            if (isHeadWatch)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookAim.position);
            }
            else
            {
                animator.SetLookAtWeight(0);
            }

            if (isLeftHandToward) {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandAim.position + leftHandAimOffset);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,leftHandRotationWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotationWeight);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandAim.rotation);
            } else {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }

            if (isRightHandToward) {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandAim.position + rightHandAimOffset);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandRotationWeight);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandAim.rotation);
            } else {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }


        } else {
            animator.SetLookAtWeight(0);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }
    }    
}
