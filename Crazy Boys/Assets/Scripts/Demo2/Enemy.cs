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
    
    public int attackTimesThreshold = 5;
    [SerializeField] private float attackRestTime = 2f;
    private int currentAttackTimes = 0;

    [HideInInspector] public bool isUnderRest = false;
    private void Start() {
        currentHp = maxHp;
        animator = this.GetComponent<Animator>();
        isShootingId = Animator.StringToHash("isShooting");
        animator.SetBool(isShootingId, isShooting);
        audioSource = this.GetComponent<AudioSource>();
        enemyFieldOfView = this.GetComponent<EnemyFieldOfView>();
    }

    public void setIsShooting(bool isShooting) {
        if (this.isUnderRest) {
            this.isShooting = false;
        } else {
            this.isShooting = isShooting;
        }
        animator.SetBool(isShootingId, this.isShooting);
    }

    public void TakeDamage(int damage) {
        currentHp -= damage;
        print("current hp: " + currentHp);
        if (currentHp <= 0) {
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
        currentAttackTimes++;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bulletRotationOffset);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        audioSource.clip = rifleShooting;
        audioSource.Play();
        
        if (currentAttackTimes >= attackTimesThreshold) {
            StartCoroutine("Rest", this.attackRestTime);
        }
    }

    void OnAnimatorIK() {
        if (ikActive && enemyFieldOfView.visibleTargets.Count != 0) {
            if (isHeadWatch) {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(enemyFieldOfView.visibleTargets[0].position);
            }
        }
	}

    IEnumerator Rest(float restTime) {
        this.setIsShooting(false);
        this.isUnderRest = true;
        yield return new WaitForSeconds(restTime);
        this.isUnderRest = false;
        this.currentAttackTimes = 0;
    }
}
