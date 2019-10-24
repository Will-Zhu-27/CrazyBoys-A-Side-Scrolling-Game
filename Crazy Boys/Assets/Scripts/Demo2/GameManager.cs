using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float itemRotateSpeed = 60.0f;
    public float itemDisappearTime = 8.0f;

    public enum itemType {Health, Ammo};
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
