using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void StartGameLevelEasy()
    {
        SceneManager.LoadScene("Indoor Demo");
    }

    public void StartGameLevelHard()
    {
        SceneManager.LoadScene("Indoor");
    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
