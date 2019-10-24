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
    public Transform target;
    public Vector3 chestRotateOffset = Vector3.zero;
    public Transform chest;
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

    public bool TakeDamage(int damage) {
        currentHp -= damage;
        print("current hp: " + currentHp);
        if (currentHp <= 0) {
            Die();
        }
        return true;
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
    /// trigger when die clip is over
    /// </summary> 
    private void MissionComplete() {
        if (Random.Range(0.0f, 1.0f) <= GameManager.Instance.itemDropRate) {
            GameObject item = Instantiate(GameManager.Instance.itemPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        }

        
        Destroy(this.gameObject);
    }

    /// <summary> 
    /// “Gunplay” Clip Event
    /// </summary> 
    private void ShootingEvent() {
        currentAttackTimes++;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.layer = this.gameObject.layer;
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
            Transform target = enemyFieldOfView.visibleTargets[0];
            if (isHeadWatch) {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(enemyFieldOfView.visibleTargets[0].position);
            }
            // Quaternion chestRotate = animator.GetBoneTransform(HumanBodyBones.Chest).rotation;
            // animator.SetBoneLocalRotation(HumanBodyBones.Chest, Quaternion.Euler(this.chestRotateOffset));
            // animator.GetBoneTransform(HumanBodyBones.UpperChest).rotation
            
            
        }
	}

    void LateUpdate() {
        if (enemyFieldOfView.visibleTargets.Count != 0) {
            Vector3 tempVector = target.position - this.chest.transform.position;
            tempVector.z = 0;
            Vector3 toDirection;
            if (tempVector.x > 0.1) {
                toDirection = Vector3.right;
            } else if(tempVector.x < -0.1) {
                toDirection = Vector3.left;
            } else {
                return;
            }
            Quaternion temp = Quaternion.FromToRotation(tempVector, toDirection);
            this.chest.transform.Rotate(-(temp.eulerAngles));
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
