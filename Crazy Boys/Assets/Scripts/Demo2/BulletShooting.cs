using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform rightBulletSpawn;
    public Transform rightBulletRotation;
    public Transform leftBulletSpawn;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0 ;
    public AudioClip handgunShoot;
    [SerializeField] private KeyCode reloadingKeyCode = KeyCode.R ;
    public AudioClip handgunReload;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private Vector3 bulletRotationOffset = Vector3.zero;
    private bool isRightShooting = true;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootingKeyCode)) {
            audioSource.clip = handgunShoot;
            audioSource.Play();
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
        if (Input.GetKeyDown(reloadingKeyCode)) {
            if (!audioSource.isPlaying) {
                audioSource.clip = handgunReload;
                audioSource.Play();
            }
        }
    }
}
