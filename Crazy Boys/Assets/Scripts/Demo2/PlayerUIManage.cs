using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManage))]
public class PlayerUIManage : MonoBehaviour
{
    public Text bulletText;
    public WeaponManage weaponManage;
    // Start is called before the first frame update
    void Start()
    {
        updateBulletText();
    }

    public void updateBulletText() {
        bulletText.text =weaponManage.currentClipCapacity + " / " + weaponManage.maxClipCapacity + "\n" + weaponManage.ownBullets;
    }
}
