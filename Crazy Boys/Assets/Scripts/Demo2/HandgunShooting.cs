using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunShooting : MonoBehaviour
{
    private Vector3 lookPos;
    public GameObject aim;
    private PlayerIKController playerIKController;

    private void Start() {
        playerIKController = GetComponent<PlayerIKController>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            HandleAimingPos();
        } else {
            playerIKController.setIKActive(false);
        }
        
    }


    private void HandleAimingPos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
            aim.transform.position = lookPos;
            playerIKController.setIKActive(true);
        } else {
            playerIKController.setIKActive(false);
        }
    }

}
