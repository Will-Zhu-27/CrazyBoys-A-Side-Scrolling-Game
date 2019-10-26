using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionResponse : MonoBehaviour
{
    public int damage = 50;
    private void OnTriggerEnter(Collider col)
    {
        ParticleSystem effect;
        if (col.tag == "Enemy") {
            if (col.GetComponent<Enemy>().TakeDamage(damage)) {
                effect = Instantiate(GameManager.Instance.bloodEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject); 
            }
            
        } else if (col.tag == "Player") {
            if (col.GetComponent<PlayerStatus>().TakeDamage(damage)) {
                
                effect = Instantiate(GameManager.Instance.bloodEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject); 
            }
        } else {
            effect = Instantiate(GameManager.Instance.obstacleEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
