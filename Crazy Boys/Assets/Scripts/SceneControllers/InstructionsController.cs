﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour
{

    public void GoBackMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
