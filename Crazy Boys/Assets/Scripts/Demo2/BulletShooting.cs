using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform bulletAim;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0 ;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private Vector3 bulletRotationOffset = Vector3.zero;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootingKeyCode)) {
            Vector3 rotation = bulletSpawn.rotation.eulerAngles + bulletRotationOffset;
            Quaternion quaternion = Quaternion.Euler(rotation);

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, quaternion);
            // bullet.transform.LookAt(bulletAim);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
            // Physics.IgnoreCollision(bulletRigidbody, bulletSpawn.GetComponent)
        }
    }
}
