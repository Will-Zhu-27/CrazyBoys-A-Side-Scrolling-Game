using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameManager.itemType itemType;
    void Start() {
        Destroy(this.gameObject, GameManager.Instance.itemDisappearTime);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0.0f, GameManager.Instance.itemRotateSpeed * Time.deltaTime, 0.0f, Space.World);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") {
            col.GetComponent<PlayerStatus>().addHP(5000);
            Destroy(this.gameObject); 
        }
    }
}
