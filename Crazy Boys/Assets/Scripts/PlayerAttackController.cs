﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(WeaponManage))]
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
    [SerializeField] private KeyCode reloadingKeyCode = KeyCode.R ;
    private bool isRightShooting = true;
    private Animator animator;
    private int isKickId;
    private bool isKick = false;
    private WeaponManage weaponManage;
    public ParticleSystem leftGunEffect;
    public ParticleSystem rightGunEffect;


    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        isKickId = Animator.StringToHash("isKick");
        meleeScript.meleeDamage = this.meleeDamage;
        meleeScript.gameObject.SetActive(false);
        weaponManage = GetComponent<WeaponManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootingKeyCode)) {
            Transform start = null;
            ParticleSystem effect = null;
            if (isRightShooting) {
                start = rightBulletSpawn;
                effect = rightGunEffect;
            } else {
                start = leftBulletSpawn;
                effect = leftGunEffect;
            }
            if(weaponManage.Fire(start.forward, start.position, start.rotation, this.gameObject.layer)) {
                effect.Play();
            }
            isRightShooting = !isRightShooting;
        }
        if (Input.GetKeyDown(reloadingKeyCode)) {
            weaponManage.Reloading();
        }
        if (Input.GetKeyDown(meleeAttackKeyCode) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Quick Roll")) {
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
