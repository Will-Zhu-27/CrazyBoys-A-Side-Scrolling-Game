using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [Range(0, 10f)]
    public float timeScale = 0.5f;
    [SerializeField] private KeyCode slowActionKeyCode = KeyCode.Mouse1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(slowActionKeyCode)) {
            print("The World!");
            Time.timeScale = timeScale;
        } else if(Input.GetKeyUp(slowActionKeyCode)){
            Time.timeScale = 1f;
        }
        
    }
}
