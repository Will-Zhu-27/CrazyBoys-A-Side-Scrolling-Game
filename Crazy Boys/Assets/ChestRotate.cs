using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRotate : MonoBehaviour
{
    public Transform target;
    public Vector3 chestRotateOffset = Vector3.zero;
    // Start is called before the first frame update


    // Update is called once per frame
    void LateUpdate()
    {
        // this.transform.LookAt(target.position);
        Vector3 tempVector = target.position - this.transform.position;
        tempVector.z = 0;
        Quaternion temp = Quaternion.FromToRotation(tempVector, Vector3.left);
        this.transform.Rotate(-(temp.eulerAngles));
        
    }
}
