using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyBullet : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start() {
        mainCamera = this.GetComponent<Camera>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        GameObject[] bulletList =  GameObject.FindGameObjectsWithTag("Bullet");
        int length = bulletList.Length;
        if (length == 0) {
            return;
        } else {
            for (int i = 0; i < length; i++) {
                // Debug.Log(mainCamera.WorldToViewportPoint(bulletList[i].transform.position));
                Vector3 position = mainCamera.WorldToViewportPoint(bulletList[i].transform.position);
                if (!(position.x >= 0 && position.x <= 1 && position.y >= 0 && position.y <= 1 && position.z >= 0)) {
                    
                    Destroy(bulletList[i]);
                }
            }
        }

        
    }
}
