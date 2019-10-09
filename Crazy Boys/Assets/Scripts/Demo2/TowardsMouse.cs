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
    [SerializeField] private Transform rightShoulder;
    [SerializeField] private Transform rightShoulderWatchPoint;
    [SerializeField] private float rightShoulderWatchPointFactor;
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
            watchPoint.position = mousePositionInWorld;
        }
       

        //left hand point
        if (leftShoulder && leftShoulderWatchPoint) {
            screenPostion = Camera.main.WorldToScreenPoint(leftShoulder.position);
            mousePositionOnScreen.z = screenPostion.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            leftShoulderWatchPoint.position = mousePositionInWorld;
            // HandleRightShoulderWatchPoint();
            HandleWatchPoint(leftShoulder, leftShoulderWatchPoint, leftShoulderWatchPointFactor);  
        }

        // right hand point
        if (rightShoulder && rightShoulderWatchPoint) {
            screenPostion = Camera.main.WorldToScreenPoint(rightShoulder.position);
            mousePositionOnScreen.z = screenPostion.z;
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            rightShoulderWatchPoint.position = mousePositionInWorld;
            HandleWatchPoint(rightShoulder, rightShoulderWatchPoint, rightShoulderWatchPointFactor);  
        }
    }

    void HandleRightShoulderWatchPoint() {
        Vector3 direction = (rightShoulderWatchPoint.position - rightShoulder.position).normalized;
        rightShoulderWatchPoint.position = rightShoulder.position + direction * rightShoulderWatchPointFactor;
    }

    void HandleWatchPoint(Transform start, Transform end, float factor) {
        Vector3 direction = (end.position - start.position).normalized;
        end.position = start.position + direction * factor;
    }
}
