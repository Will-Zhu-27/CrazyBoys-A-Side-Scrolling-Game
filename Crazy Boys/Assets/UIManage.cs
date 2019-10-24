using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    public Text bulletText;
    public GameObject reloadingUI;
    private Animator reloadingUIAnimator;
    public WeaponManage weaponManage;

    public Image hpImageSlider;
    public Image powerImageSlider;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBulletText();
        reloadingUIAnimator = reloadingUI.GetComponent<Animator>();
    }

    public void UpdateBulletText() {
        bulletText.text =weaponManage.currentClipCapacity + " / " + weaponManage.maxClipCapacity + "\n" + weaponManage.ownBullets;
    }

    public void SetReloadingUI(bool active) {
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

    public void UpdateHPSlider(float fillAmount) {
        hpImageSlider.fillAmount = fillAmount;
    }

    public void UpdatePowerSlider(float fillAmount) {
        powerImageSlider.fillAmount = fillAmount;
    }
}
