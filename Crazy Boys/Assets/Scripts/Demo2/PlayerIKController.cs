using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerIKController : MonoBehaviour
{
    private Animator animator;
    public bool ikActive = false;
    [SerializeField] private Transform lookObj = null;
    public bool isHeadWatch = true;
    public bool isLeftHandToward = true;
    public bool isRightHandToward = true;
    public Transform rightHandObj;
    
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
                animator.SetLookAtPosition(lookObj.position);
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
            if (isRightHandToward) {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, lookObj.position);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);              
            } else {

            }

        } else {
            animator.SetLookAtWeight(0);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
        }
    }    
}
