using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float itemRotateSpeed = 60.0f;
    public float itemDisappearTime = 8.0f;
    [Range(0.0f, 1.0f)]
    public float itemDropRate = 0.5f;
    public GameObject itemPrefab;
    public float spinCoolDown = 1.0f;
    public GameObject crossHair;
    public UIManage uIManage;
    [Range(0, 1.0f)]
    public float timeScaleChange = 0.5f;
    public float timePowerTime = 3f;
    public float recoverTimePowerTime = 5f;
    public bool isPlayerActive = true;

    public ParticleSystem bloodEffect;
    public ParticleSystem obstacleEffect;
    public GameObject gameOverUI;
    public GameObject winUI;
    public GameObject player;
    public GameObject win;

    public enum itemType {Health, Ammo};
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void GameOver() {
        isPlayerActive = false;
        gameOverUI.SetActive(true);
        // winUI.SetActive(false);
        Cursor.visible = true;
        crossHair.SetActive(false);
    }

    public void Win() {
        isPlayerActive = false;
        winUI.SetActive(true);
        // gameOverUI.SetActive(false);
        Cursor.visible = true;
        crossHair.SetActive(false);
        player.GetComponent<PlayerStatus>().StopPlayer();
        player.GetComponent<Animator>().applyRootMotion = true;
        player.GetComponent<Animator>().Play("Win");
        win.SetActive(true);
    }

    public void OnButtonRetry() {
        print("retry button");
        SceneManager.LoadScene("Indoor Demo");
    }

    public void OnButtonExit() {
        SceneManager.LoadScene("Main Menu");
    }


}
