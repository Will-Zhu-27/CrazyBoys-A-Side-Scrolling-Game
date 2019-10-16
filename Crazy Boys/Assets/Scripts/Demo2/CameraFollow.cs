using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lookAtCharacter;
    public float smoothSpeed = 0.125f;
    private Vector3 posOffset;
    void Start() {
        //posOffset = this.transform.position - lookAtCharacter.position;
        posOffset = new Vector3(0, 0, -8);
    }
    void LateUpdate()
    {
        this.transform.LookAt(lookAtCharacter);
        // Vector3 originPos = this.transform.position;
        // Vector3 characterPos = lookAtCharacter.position;
        // this.transform.Translate(new Vector3(characterPos.x - originPos.x, 0, 0));
        Vector3 desiredPos = lookAtCharacter.position + posOffset;
        Vector3 smoothedPos = Vector3.Lerp(this.transform.position, desiredPos, smoothSpeed);
        this.transform.position = smoothedPos;
    }
}
