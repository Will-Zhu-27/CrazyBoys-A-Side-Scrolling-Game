using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private KeyCode slowActionKeyCode = KeyCode.Mouse1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(slowActionKeyCode)) {
            print("The World!");
            Time.timeScale = GameManager.Instance.timeScaleChange;
        } else if (Input.GetKeyUp(slowActionKeyCode)) {
            Time.timeScale = 1.0f;
        }

        if (Input.GetKey(slowActionKeyCode)) {
            // GameManager.Instance.uIManage.powerImageSlider.fillAmount -= Time.deltaTime * 1f / GameManager.Instance.timePowerTime / Time.timeScale;
        } else {
            GameManager.Instance.uIManage.powerImageSlider.fillAmount += Time.deltaTime * 1f /  GameManager.Instance.recoverTimePowerTime;
        }
        if (GameManager.Instance.uIManage.powerImageSlider.fillAmount <= 0) {
            Time.timeScale = 1.0f;
        }
        
    }

    // void FixedUpdate() {
    //     if (Input.GetKey(slowActionKeyCode)) {
    //         GameManager.Instance.uIManage.powerImageSlider.fillAmount -= Time.fixedDeltaTime * 1f;
    //     } else {
    //         GameManager.Instance.uIManage.powerImageSlider.fillAmount += Time.deltaTime * 0.5f;
            
    //     }
    // }
}
