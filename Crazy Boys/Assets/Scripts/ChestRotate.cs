using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRotate : MonoBehaviour
{
    public Transform target;
    public Vector3 chestRotateOffset = Vector3.zero;
    void LateUpdate()
    {
        Vector3 tempVector = target.position - this.transform.position;
        tempVector.z = 0;
        Quaternion temp = Quaternion.FromToRotation(tempVector, Vector3.left);
        this.transform.Rotate(-(temp.eulerAngles));
        
    }

    // void LateUpdate() {
    //     Quaternion temp = Quaternion.LookRotation(Vector3.forward, this.transform.position - target.position);
    //     // Vector3 tempVector = temp.eulerAngles;
    //     // tempVector.z = 0;
    //     // this.transform.rotation = temp;
    //     this.transform.localRotation = temp;
    // }
}
