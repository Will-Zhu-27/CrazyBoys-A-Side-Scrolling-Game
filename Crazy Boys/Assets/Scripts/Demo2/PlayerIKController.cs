using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerIKController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool ikActive = false;
    public Transform lookObj = null;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                // if(rightHandObj != null) {
                //     animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                //     animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                //     animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                //     animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                // }        
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                // animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                // animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                animator.SetLookAtWeight(0);
            }
        }
    }

    public void setIKActive(bool status) {
        ikActive = status;
    }    
}
