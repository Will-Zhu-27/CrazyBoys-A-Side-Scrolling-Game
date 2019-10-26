using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (layerIndex == 0 && stateInfo.IsName("Walking Turn 180")) {
           animator.applyRootMotion = true;
           bool isFaceForward = animator.GetBool("isFaceForward");
        //    animator.SetBool("isTurnBack", false);
           animator.SetBool("isFaceForward", !isFaceForward);
       }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (layerIndex == 0 && stateInfo.IsName("Walking Turn 180")) {
           animator.applyRootMotion = false;
           animator.SetBool("isTurnBack", false);
        //    bool isFaceForward = animator.GetBool("isFaceForward");
        //    Transform obj = animator.GetComponent<Transform>();
        //    if (isFaceForward) {
        //        obj.LookAt(obj.position + 10 * Vector3.left);
        //    } else {
        //        obj.LookAt(obj.position + 10 * Vector3.right);
        //    }
       }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
