using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Vector3 lookPos;
    public GameObject aim;
    private IKControl iKControl;

    private void Start() {
        iKControl = GetComponent<IKControl>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            HandleAimingPos();
        } else {
            iKControl.ikActive = false;
        }
        
    }


    private void HandleAimingPos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
            iKControl.ikActive = true;
            aim.transform.position = lookPos;
        } else {
            iKControl.ikActive = false;
        }
    }
}
