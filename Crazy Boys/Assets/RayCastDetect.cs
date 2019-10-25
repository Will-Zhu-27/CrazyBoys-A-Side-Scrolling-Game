using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDetect : MonoBehaviour
{
    public LayerMask detectLayer;
    public float rayLength;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, rayLength, detectLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Debug.Log(hit.transform.position);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * rayLength, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
