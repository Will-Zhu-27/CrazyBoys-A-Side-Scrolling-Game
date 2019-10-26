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
    public RectTransform crossHair;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBulletText();
        reloadingUIAnimator = reloadingUI.GetComponent<Animator>();
        Cursor.visible = false;
        crossHair.position = Input.mousePosition;
    }

    void Update() {
        crossHair.position = Input.mousePosition;
    }

    public void UpdateBulletText() {
        bulletText.text =weaponManage.currentClipCapacity + " / " + weaponManage.maxClipCapacity + "\n" + weaponManage.ownBullets;
    }

    public void SetReloadingUI(bool active) {
        if (active == true) {
            reloadingUI.SetActive(active);
            reloadingUIAnimator.SetBool("isReloading", true);
        } else {
            // StartCoroutine(ReloadingUIDisableEvent());
            reloadingUIAnimator.SetBool("isReloading", false);
        }
        
    }

    IEnumerator ReloadingUIDisableEvent() {
        reloadingUIAnimator.SetBool("isReloading", false);
        while(true) {
            // if (reloadingUI.active == true) {
            //     break;
            // }
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
