using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsMouse : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform watchPoint;
    private Vector3 screenPostion;
    private Vector3 mousePositionOnScreen;
    private Vector3 mousePositionInWorld;

    // Update is called once per frame
    void Update()
    {
        screenPostion = Camera.main.WorldToScreenPoint(startPoint.position);
        mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenPostion.z;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        watchPoint.position = mousePositionInWorld;
    }
}
