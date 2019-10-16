using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionResponse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("hit!");
        Destroy(this.gameObject);
    }
}
