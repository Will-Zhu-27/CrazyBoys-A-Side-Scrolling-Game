using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionResponse : MonoBehaviour
{
    public int damage = 50;
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy") {
            col.GetComponent<Enemy>().TakeDamage(damage);
            print("hit damage!");
        }

        // Debug.Log("hit!");
        Destroy(this.gameObject);
    }
}
