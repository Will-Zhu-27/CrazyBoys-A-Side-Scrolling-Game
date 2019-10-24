using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameManager.itemType itemType;
    public GameObject healthItem;
    public GameObject ammoItem;
    void Start() {
        if (Random.Range(0.0f, 1.0f) <= 0.5) {
            itemType = GameManager.itemType.Ammo;
        } else {
            itemType = GameManager.itemType.Health;
        }
        // Destroy(this.gameObject, GameManager.Instance.itemDisappearTime);
        if (itemType == GameManager.itemType.Ammo) {
            ammoItem.SetActive(true);
            healthItem.SetActive(false);
        } else if (itemType == GameManager.itemType.Health) {
            ammoItem.SetActive(false);
            healthItem.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0.0f, GameManager.Instance.itemRotateSpeed * Time.deltaTime, 0.0f, Space.World);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") {
            if (itemType == GameManager.itemType.Health) {
                col.GetComponent<PlayerStatus>().AddHP(5000);
            } else if (itemType == GameManager.itemType.Ammo) {
                col.GetComponent<WeaponManage>().AddBullets(1000);
            }
            
            Destroy(this.gameObject, 0.05f); 
        }
    }
}
