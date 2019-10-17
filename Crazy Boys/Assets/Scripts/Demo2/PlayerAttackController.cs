﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private KeyCode meleeAttackKeyCode = KeyCode.F;
    public int meleeDamage = 100;
    public Melee meleeScript;
    // public GameObject melee;
    public GameObject bulletPrefab;
    public Transform rightBulletSpawn;
    public Transform leftBulletSpawn;
    [SerializeField] private KeyCode shootingKeyCode = KeyCode.Mouse0 ;
    public AudioClip handgunShoot;
    [SerializeField] private KeyCode reloadingKeyCode = KeyCode.R ;
    public AudioClip handgunReload;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private Vector3 bulletRotationOffset = new Vector3(90, 0, 0);
    private bool isRightShooting = true;
    private AudioSource audioSource;
    private Animator animator;
    private int isKickId;
    private bool isKick = false;

    // Start is called before the first frame update
    void Start() {
        audioSource = this.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        isKickId = Animator.StringToHash("isKick");
        meleeScript.meleeDamage = this.meleeDamage;
        meleeScript.gameObject.SetActive(false);
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
        if (Input.GetKeyDown(meleeAttackKeyCode)) {
            isKick = true;
        } else if (Input.GetKeyUp(meleeAttackKeyCode)) {
            isKick = false;
        }
        animator.SetBool(isKickId, isKick);
    }

    private void KickStartEvent() {
        meleeScript.gameObject.SetActive(true);
    }

    private void KickEndEvent() {
        meleeScript.gameObject.SetActive(false);
    }

}