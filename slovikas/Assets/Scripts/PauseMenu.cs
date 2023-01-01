using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        PauseGame.isGamePaused = false;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("game");


    }
    public void MainMenuButton()
    {
        MainMenu.place = "MAIN";
        SceneManager.LoadScene("game");
    }
    
    public void QuitGameButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void SettingsButton()
    {
        Debug.Log("settings button");
    }




}
