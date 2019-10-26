using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetect : MonoBehaviour
{
private void OnTriggerEnter(Collider col)
    {
       if (col.tag == "Player") {
           GameManager.Instance.Win();
        }
    }
}
