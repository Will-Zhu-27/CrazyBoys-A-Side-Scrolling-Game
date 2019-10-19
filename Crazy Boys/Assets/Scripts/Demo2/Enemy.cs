using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(EnemyFieldOfView))]
public class Enemy : MonoBehaviour
{
    public int maxHp = 100;
    [SerializeField] private int currentHp;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private Vector3 bulletRotationOffset = new Vector3(90, 0, 0);
    private Animator animator;
    private float tempTime = 0;
    private float alpha;
    private bool isDie = false;
    [SerializeField] private bool isShooting = false;
    private int isShootingId;
    private AudioSource audioSource;
    [SerializeField] private AudioClip rifleShooting;
    public bool ikActive = true;
    public bool isHeadWatch = true;
    private EnemyFieldOfView enemyFieldOfView;
    public Transform lookAim;
    public bool autoAttack = true;

    private void Start() {
        currentHp = maxHp;
        animator = this.GetComponent<Animator>();
        isShootingId = Animator.StringToHash("isShooting");
        animator.SetBool(isShootingId, isShooting);
        audioSource = this.GetComponent<AudioSource>();
        enemyFieldOfView = this.GetComponent<EnemyFieldOfView>();
    }

    // private void Update() {
    //     if (Input.GetKeyDown(KeyCode.G)) {
    //         isShooting = true;
    //     } else if (Input.GetKeyUp(KeyCode.G)) {
    //         isShooting = false;
    //     }
    //     animator.SetBool(isShootingId, isShooting);
    // }

    public void setIsShooting(bool isShooting) {
        this.isShooting = isShooting;
        animator.SetBool(isShootingId, isShooting);
    }

    public void TakeDamage(int damage) {
        currentHp -= damage;
        print("current hp: " + currentHp);
        if (currentHp <= 0) {
            // animator.Play("Falling Back Death");
            Die();
        }
    }

    private void Die() {
        float tep = Random.Range(0f, 1f);
        if (tep > 0.5) {
            animator.Play("Falling Back Death");
        } else {
            animator.Play("Falling Forward Death");
        }
        
        // StartCoroutine(DestroyAfterAnimation("Falling Back Death"));
        // isDie = true;
        // print(skin.GetComponent<SkinnedMeshRenderer>().material.color);
        // skin.GetComponent<SkinnedMeshRenderer>().material.color = new Color(1,1,1,0);
    }

    // IEnumerator DestroyAfterAnimation(string stateName) {
    //     while (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
    //         yield return null;
    //     }
    //     Destroy(this.gameObject);
    // }

    /// <summary> 
    /// “Falling Back Death” Clip Event
    /// </summary> 
    private void MissionComplete() {
        Destroy(this.gameObject);
    }

    /// <summary> 
    /// “Gunplay” Clip Event
    /// </summary> 
    private void ShootingEvent() {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bulletRotationOffset);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        audioSource.clip = rifleShooting;
        audioSource.Play();
    }

    void OnAnimatorIK() {
        if (ikActive && enemyFieldOfView.visibleTargets.Count != 0) {
            if (isHeadWatch) {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(enemyFieldOfView.visibleTargets[0].position);
            }
        }
        // if (ikActive) {
        //     if (isHeadWatch) {
        //         animator.SetLookAtWeight(1);
        //         animator.SetLookAtPosition(lookAim.position);
        //     }
        // }
	}
}
