using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void MissionComplete() {
        this.gameObject.SetActive(false);
    }
}
