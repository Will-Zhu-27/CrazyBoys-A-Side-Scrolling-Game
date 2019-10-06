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

        
    }



}
