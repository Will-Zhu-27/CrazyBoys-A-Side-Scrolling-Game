using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    // public Transform skin;
    public int maxHp = 100;
    [SerializeField] private int currentHp;
    private Animator animator;
    private float tempTime = 0;
    private float alpha;
    private bool isDie = false;
    private bool isShooting = false;
    private int isShootingId;

    private void Start() {
        currentHp = maxHp;
        animator = this.GetComponent<Animator>();
        isShootingId = Animator.StringToHash("isShooting");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            isShooting = true;
        } else if (Input.GetKeyUp(KeyCode.G)) {
            isShooting = false;
        }
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

    }
}
