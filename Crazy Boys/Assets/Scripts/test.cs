using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private float startRotateTime;
    private float rotateTime;
    private Quaternion startRotation;
    private Quaternion endRotation;
    public bool isStart = false;

    void Start() {
        startRotateTime = 0f;
        rotateTime = 5f;
        startRotation = Quaternion.Euler(0,0,0);
        endRotation = Quaternion.Euler(0,270,0);
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            isStart = true;
        }
        if (isStart) {
            RotationEvent();
        }
        
    }

    private void RotationEvent() {
        float l = Mathf.InverseLerp(startRotateTime, startRotateTime + rotateTime, Time.time);
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, l);
        if (this.transform.eulerAngles == new Vector3(0, 270, 0)) {
            isStart = false;
        }
    }

}
