using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public UIManage uIManage;


    private void Awake() {
        currentClipCapacity = maxClipCapacity;
    }
    private void Start() {
        audioSource = this.GetComponent<AudioSource>();
    }

    public bool Fire(Vector3 direction, Vector3 startPosition, Quaternion startRotation, int layer) {
        if (currentClipCapacity <= 0) {
            // need reloading !
            audioSource.clip = gunEmpty;
            audioSource.Play();
            return false;
        } else {
            currentClipCapacity--;
            GameObject bullet = Instantiate(bulletPrefab, startPosition, startRotation);
            bullet.layer = layer;
            bullet.transform.Rotate(bulletRotationOffset);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode.Impulse);
            audioSource.clip = handgunShoot;
            audioSource.Play();
            uIManage.UpdateBulletText();
            return true;
        }
    }

    public bool Reloading() {
        if (currentClipCapacity == maxClipCapacity || (audioSource.clip == handgunReload && audioSource.isPlaying)) {
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
        uIManage.SetReloadingUI(true);
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
                    uIManage.UpdateBulletText();
                    break;
                }
            } else {
                break;
            }
            yield return null;
        }
        uIManage.SetReloadingUI(false);
    }

    public void AddBullets(int addBullets) {
        ownBullets += addBullets;
        uIManage.UpdateBulletText();
    }
}
