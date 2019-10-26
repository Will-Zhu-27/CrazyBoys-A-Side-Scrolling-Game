using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManage))]
public class PlayerUIManage : MonoBehaviour
{
    public Text bulletText;
    public GameObject reloadingUI;
    private Animator reloadingUIAnimator;
    public WeaponManage weaponManage;
    // Start is called before the first frame update
    void Start()
    {
        updateBulletText();
        reloadingUIAnimator = reloadingUI.GetComponent<Animator>();
    }

    public void updateBulletText() {
        bulletText.text =weaponManage.currentClipCapacity + " / " + weaponManage.maxClipCapacity + "\n" + weaponManage.ownBullets;
    }

    public void setReloadingUI(bool active) {
        if (active == true) {
            reloadingUI.SetActive(active);
            reloadingUIAnimator.SetBool("isReloading", true);
        } else {
            StartCoroutine(ReloadingUIDisableEvent());
        }
        
    }

    IEnumerator ReloadingUIDisableEvent() {
        reloadingUIAnimator.SetBool("isReloading", false);
        while(true) {
            if (reloadingUI.active == true) {
                break;
            }
            if (!reloadingUIAnimator.GetCurrentAnimatorStateInfo(1).IsName("UIDisappear") && !reloadingUIAnimator.IsInTransition(1)) {
                break;
            }
            yield return null;
        }
        reloadingUI.SetActive(false);
    }
}
