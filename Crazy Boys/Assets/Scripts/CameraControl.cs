using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform lookAtCharacter;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(lookAtCharacter);
    }
}
