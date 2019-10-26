using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float itemRotateSpeed = 60.0f;
    public float itemDisappearTime = 8.0f;
    [Range(0.0f, 1.0f)]
    public float itemDropRate = 0.5f;
    public GameObject itemPrefab;
    public float spinCoolDown = 1.0f;
    public UIManage uIManage;
    [Range(0, 1.0f)]
    public float timeScaleChange = 0.5f;
    public float timePowerTime = 3f;
    public float recoverTimePowerTime = 5f;
    public bool isPlayerDie = false;

    public ParticleSystem bloodEffect;
    public ParticleSystem obstacleEffect;

    public enum itemType {Health, Ammo};
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
