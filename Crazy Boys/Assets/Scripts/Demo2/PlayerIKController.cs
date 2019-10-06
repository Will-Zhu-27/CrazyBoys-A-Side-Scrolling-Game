using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerIKController : MonoBehaviour
{
    private Animator animator;
    public bool ikActive = false;
    [SerializeField] private Transform lookObj = null;
    public bool isHeadWatch;
    
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
        } else {
            animator.SetLookAtWeight(0);
        }
    }    
}
