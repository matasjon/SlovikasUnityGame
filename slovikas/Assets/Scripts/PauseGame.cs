using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isGamePaused = false;

    [SerializeField] GameObject pauseMenu;

    public string place;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "game")
            {
                if (isGamePaused)
                {
                    GameResume();

                }
                else
                {
                    GamePause();
                }
            }
        }
    }

    public void GameResume()
    {
        MainMenu.place = "";
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
       
        isGamePaused = false;
    }

    void GamePause()
    {

        MainMenu.place = "PAUSE";
        pauseMenu.SetActive(true);
       
        Time.timeScale = 0;

        isGamePaused = true;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("game");


        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        

        isGamePaused = false;

    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("game");
        MainMenu.place = "MAIN";
        pauseMenu.SetActive(false);
        Time.timeScale = 0;

        isGamePaused = false;
    }

    public void QuitGameButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        isGamePaused = false;
        Application.Quit();
        Debug.Log("Game is exiting");

    }

    public void SettingsButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        isGamePaused = false;
     
    }



}
