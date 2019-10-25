using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionResponse : MonoBehaviour
{
    public int damage = 50;
    private Vector3 effectOffset = new Vector3(-90.0f, 0.0f, 0.0f);
    private void OnTriggerEnter(Collider col)
    {
        Vector3 rotation = this.transform.rotation.eulerAngles + effectOffset;
        ParticleSystem effect;
        if (col.tag == "Enemy") {
            if (col.GetComponent<Enemy>().TakeDamage(damage)) {
                effect = Instantiate(GameManager.Instance.bloodEffect, this.transform.position, Quaternion.Euler(rotation));
                Destroy(this.gameObject); 
            }
            
        } else if (col.tag == "Player") {
            if (col.GetComponent<PlayerStatus>().TakeDamage(damage)) {
                
                effect = Instantiate(GameManager.Instance.bloodEffect, this.transform.position, Quaternion.Euler(rotation));
                Destroy(this.gameObject); 
            }
        } else {
            Destroy(this.gameObject);
        }
    }
}
