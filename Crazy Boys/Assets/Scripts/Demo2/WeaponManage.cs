using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUIManage))]
[RequireComponent(typeof(AudioSource))]
public class WeaponManage : MonoBehaviour
{
    public bool isInfiniteBullets;
    public int ownBullets = 0;
    public int maxClipCapacity = 16;
    public int currentClipCapacity;
    public GameObject bulletPrefab;
    public Vector3 bulletRotationOffset;
    [SerializeField] private float bulletSpeed = 10f;
    private AudioSource audioSource;
    public AudioClip handgunShoot;
    public AudioClip handgunReload;
    public AudioClip gunEmpty;
    private PlayerUIManage playerUIManage;

    private void Awake() {
        currentClipCapacity = maxClipCapacity;
    }
    private void Start() {
        audioSource = this.GetComponent<AudioSource>();
        playerUIManage = GetComponent<PlayerUIManage>();
    }

    public bool Fire(Vector3 direction, Vector3 startPosition, Quaternion startRotation) {
        if (currentClipCapacity <= 0) {
            // need reloading !
            audioSource.clip = gunEmpty;
            audioSource.Play();
            return false;
        } else {
            currentClipCapacity--;
            GameObject bullet = Instantiate(bulletPrefab, startPosition, startRotation);
            bullet.transform.Rotate(bulletRotationOffset);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode.Impulse);
            audioSource.clip = handgunShoot;
            audioSource.Play();
            playerUIManage.updateBulletText();
            return true;
        }
    }

    public bool Reloading() {
        if (currentClipCapacity == maxClipCapacity) {
            return false;
        }
        if (isInfiniteBullets || ownBullets >= 0) {
            audioSource.clip = handgunReload;
            audioSource.Play();
            StartCoroutine(ReloadingEvent());
            return true;
        } else {
            audioSource.clip = gunEmpty;
            audioSource.Play();
            return false;
        }
    }

    IEnumerator ReloadingEvent() {
        playerUIManage.setReloadingUI(true);
        while(true) {
            if (audioSource.clip == handgunReload) {
                if (audioSource.isPlaying == false) {
                    if (ownBullets >= maxClipCapacity) {
                        currentClipCapacity = maxClipCapacity;
                        ownBullets -= maxClipCapacity;
                    } else {
                        currentClipCapacity = ownBullets;
                        ownBullets = 0;
                    }
                    playerUIManage.updateBulletText();
                    break;
                }
            } else {
                break;
            }
            yield return null;
        }
        playerUIManage.setReloadingUI(false);
    }

    public void GetBullet(int addBullets) {
        ownBullets += addBullets;
        playerUIManage.updateBulletText();
    }
}
