using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHP = 1000;
    [SerializeField] private int currentHP = 0;

    private void Start() {
        currentHP = maxHP;
    }

    public void takeDamage(int damage) {
        currentHP -= damage;
        print("Player HP:" + currentHP);
    }
}
