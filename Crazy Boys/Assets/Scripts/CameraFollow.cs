using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smothing = 10f;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        offset = transform.position - target.position;
    }
    // Update is called once per frame
    void LateUpdate () {
        Vector3 targetCampos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCampos, smothing * Time.deltaTime);
    }

}
