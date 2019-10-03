using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Animator animator;
    public Vector3 offset;
    private Transform rightShoulder;
    void Start() {
        rightShoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vector3 posToScreen = Camera.main.WorldToScreenPoint(rightShoulder.position);
        // Vector3 vect = Input.mousePosition - posToScreen + new Vector3(0, 0, rightShoulder.position.z);
        // rightShoulder.LookAt(vect);
        // rightShoulder.rotation = Quaternion.LookRotation(vect, rightShoulder.up) * Quaternion.Euler(new Vector3(0, 90, 0));
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rightShoulder.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rightShoulder.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        if (rotationZ < -90 || rotationZ > 90)
        {
 
 
 
            if(this.transform.eulerAngles.y == 0)
            {
 
 
                rightShoulder.localRotation = Quaternion.Euler(180, 0, -rotationZ);
 
 
            } else if (this.transform.eulerAngles.y == 180) {
 
 
                 rightShoulder.localRotation = Quaternion.Euler(180, 180, -rotationZ);
 
 
            }
 
        }
    }
}
