using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsMouse : MonoBehaviour
{
    [SerializeField] private Transform eyePoint;
    [SerializeField] private Transform watchPoint;
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform leftShoulderWatchPoint;
    [SerializeField] private float leftShoulderWatchPointFactor;
    [SerializeField] private Vector3 leftShoulderWatchPointRotationOffset = new Vector3(-90f, 0f, 0f);
    [SerializeField] private Transform rightShoulder;
    [SerializeField] private Transform rightShoulderWatchPoint;
    [SerializeField] private float rightShoulderWatchPointFactor;
    [SerializeField] private Vector3 rightShoulderWatchPointRotationOffset = new Vector3(90f, 0f, 0f);
    private Vector3 screenPostion;
    private Vector3 mousePositionOnScreen;
    private Vector3 mousePositionInWorld;

    

    // Update is called once per frame
    void Update()
    {
        mousePositionOnScreen = Input.mousePosition;
        // eye point
        if (eyePoint && watchPoint) {
            screenPostion = Camera.main.WorldToScreenPoint(eyePoint.position);
            mousePositionOnScreen.z = screenPostion.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            watchPoint.position = new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, eyePoint.position.z);
        }
       

        //left hand point
        if (leftShoulder && leftShoulderWatchPoint) {
            screenPostion = Camera.main.WorldToScreenPoint(leftShoulder.position);
            mousePositionOnScreen.z = screenPostion.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            leftShoulderWatchPoint.position = mousePositionInWorld;
            HandleWatchPoint(leftShoulder, leftShoulderWatchPoint, leftShoulderWatchPointFactor, leftShoulderWatchPointRotationOffset);  
        }

        // right hand point
        if (rightShoulder && rightShoulderWatchPoint) {
            screenPostion = Camera.main.WorldToScreenPoint(rightShoulder.position);
            mousePositionOnScreen.z = screenPostion.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            rightShoulderWatchPoint.position = mousePositionInWorld;
            HandleWatchPoint(rightShoulder, rightShoulderWatchPoint, rightShoulderWatchPointFactor, rightShoulderWatchPointRotationOffset);  
        }
    }

    void HandleRightShoulderWatchPoint() {
        Vector3 direction = (rightShoulderWatchPoint.position - rightShoulder.position).normalized;
        rightShoulderWatchPoint.position = rightShoulder.position + direction * rightShoulderWatchPointFactor;
    }

    void HandleWatchPoint(Transform start, Transform end, float factor, Vector3 rotationOffset) {
        Vector3 direction = new Vector3(end.position.x - start.position.x, end.position.y - start.position.y, 0).normalized;
        end.position = start.position + direction * factor;
        end.rotation = Quaternion.LookRotation(end.position - start.position, this.transform.up + this.transform.TransformDirection(rotationOffset));
    }
}
