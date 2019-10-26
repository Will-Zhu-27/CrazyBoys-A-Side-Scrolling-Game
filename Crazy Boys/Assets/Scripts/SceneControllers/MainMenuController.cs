using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instruction;
    
    public void StartGameLevelEasy()
    {
        SceneManager.LoadScene("Indoor Demo");
        print("press easy");
    }

    public void GoToInstructions()
    {
        mainMenu.SetActive(false);
        instruction.SetActive(true);
    }

    public void OnButtonReturn() {
        mainMenu.SetActive(true);
        instruction.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }
}
