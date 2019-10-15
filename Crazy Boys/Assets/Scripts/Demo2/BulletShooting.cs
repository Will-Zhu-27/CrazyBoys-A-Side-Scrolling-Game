using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform rightBulletSpawn;
    public Transform rightBulletRotation;
    public Transform leftBulletSpawn;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0 ;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Vector3 bulletRotationOffset = Vector3.zero;
    private bool isRightShooting = true;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootingKeyCode)) {
            Transform start = null;
            if (isRightShooting) {
                start = rightBulletSpawn;
            } else {
                start = leftBulletSpawn;
            }

            GameObject bullet = Instantiate(bulletPrefab, start.position, start.rotation);
            bullet.transform.Rotate(bulletRotationOffset);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            bulletRigidbody.AddForce(start.forward * bulletSpeed, ForceMode.Impulse);
            isRightShooting = !isRightShooting;
        }
    }
}
