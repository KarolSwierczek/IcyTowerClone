using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script controlls button functions in main menu.
public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
