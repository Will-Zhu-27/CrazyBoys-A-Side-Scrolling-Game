using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    // need its parent to assign demage value
    [HideInInspector] public int meleeDamage = 0;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy") {
            col.GetComponent<Enemy>().TakeDamage(meleeDamage);
            // print("kick enemy!");
        }
    }
}
