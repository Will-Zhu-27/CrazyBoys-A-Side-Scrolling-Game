using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionResponse : MonoBehaviour
{
    public int damage = 50;
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy") {
            if (col.GetComponent<Enemy>().TakeDamage(damage)) {
               Destroy(this.gameObject); 
            }
            
        } else if (col.tag == "Player") {
            if (col.GetComponent<PlayerStatus>().TakeDamage(damage)) {
                Destroy(this.gameObject); 
            }
        } else {
            Destroy(this.gameObject);
        }
    }
}
