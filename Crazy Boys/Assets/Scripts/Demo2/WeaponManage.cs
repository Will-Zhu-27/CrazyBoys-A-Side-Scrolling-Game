﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponManage : MonoBehaviour
{
    public bool isInfiniteBullets;
    public int ownBullets = 0;
    public int maxClipCapacity = 16;
    [SerializeField] private int currentClipCapacity;

    public GameObject bulletPrefab;
    public Vector3 bulletRotationOffset;
    [SerializeField] private float bulletSpeed = 10f;
    private AudioSource audioSource;
    public AudioClip handgunShoot;
    public AudioClip handgunReload;
    public AudioClip gunEmpty;

    private void Start() {
        currentClipCapacity = maxClipCapacity;
        audioSource = this.GetComponent<AudioSource>();
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
            return true;
        }
    }

    public bool Reloading() {
        if (audioSource.isPlaying || currentClipCapacity == maxClipCapacity) {
            return false;
        }

        if (isInfiniteBullets) {
            audioSource.clip = handgunReload;
            audioSource.Play();
            currentClipCapacity = maxClipCapacity;
        } else {
            if (ownBullets <= 0) {
                audioSource.clip = gunEmpty;
                audioSource.Play();
                return false;
            }
            audioSource.clip = handgunReload;
            audioSource.Play();
            if (ownBullets >= maxClipCapacity) {
                ownBullets -= maxClipCapacity;
                currentClipCapacity = maxClipCapacity;
            } else {
                currentClipCapacity = ownBullets;
                ownBullets = 0;
            }
        }
        return true;
    }

    public void GetBullet(int addBullets) {
        ownBullets += addBullets;
    }
}