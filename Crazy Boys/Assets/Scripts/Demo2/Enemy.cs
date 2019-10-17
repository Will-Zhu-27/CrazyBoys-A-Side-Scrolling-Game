using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public Transform skin;
    public int maxHp = 100;
    [SerializeField] private int currentHp;
    private Animator animator;
    private float tempTime = 0;
    private float alpha;
    private bool isDie = false;

    private void Start() {
        currentHp = maxHp;
        animator = this.GetComponent<Animator>();
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
        animator.Play("Falling Back Death");
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
    private void MissionComplete() {
        Destroy(this.gameObject);
    }
    // private void OnTriggerEnter(Collider col)
    // {
    //     if (col.tag == "Melee") {
    //         print("I am kicked!");
    //         col.GetComponentInParent()
    //     }

    // }
}
