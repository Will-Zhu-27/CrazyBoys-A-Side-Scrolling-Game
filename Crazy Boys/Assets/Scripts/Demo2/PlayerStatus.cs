using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHP = 1000;
    [SerializeField] private int currentHP = 0;
    private Animator animator;

    private void Start() {
        currentHP = maxHP;
        animator = this.GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        currentHP -= damage;
        print("Player HP:" + currentHP);
        if (currentHP <= 0) {
            Die();
        }
    }

    private void Die() {
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<PlayerMoveController>().enabled = false;
        this.GetComponent<PlayerAttackController>().enabled = false;
        this.GetComponent<PlayerIKController>().enabled = false;
        animator.applyRootMotion = true;
        animator.Play("Mutant Dying");
    }
}
