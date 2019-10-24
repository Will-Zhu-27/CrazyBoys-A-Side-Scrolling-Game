using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHP = 1000;
    [SerializeField] private int currentHP = 0;
    [SerializeField] private bool isVincible = false;
    private Animator animator;
    public UIManage uIManage;

    private void Start() {
        currentHP = maxHP;
        animator = this.GetComponent<Animator>();
    }

    public bool TakeDamage(int damage) {
        if (this.isVincible) {
            return false;
        }
        currentHP -= damage;
        print("Player HP:" + currentHP);
        if (currentHP <= 0) {
            Die();
        }
        uIManage.UpdateHPSlider((float)currentHP/maxHP);
        return true;
    }

    private void Die() {
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<PlayerMoveController>().enabled = false;
        this.GetComponent<PlayerAttackController>().enabled = false;
        this.GetComponent<PlayerIKController>().enabled = false;
        animator.applyRootMotion = true;
        animator.Play("Mutant Dying");
    }

    private void SetVincible(int status) {
        if (status != 0) {
            this.isVincible = true;
        } else {
            this.isVincible = false;
        }
    }

    public void addHP(int increment) {
        currentHP = currentHP + increment > maxHP ? maxHP : currentHP + increment;
        uIManage.UpdateHPSlider((float)currentHP/maxHP);
    }


}
