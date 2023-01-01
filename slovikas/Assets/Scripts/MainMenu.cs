using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    //[SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hpxpgoldMenu;

    HealthBarScript healthBar;
    ManaBarScript manaBar;

    

    public static string place = "MAIN";

    public void Awake()
    {
        //mainMenu.SetActive(true);
    }
    public void StartButton()
    {
        //SceneManager.LoadScene("game");

        mainMenu.SetActive(false);
        Time.timeScale = 1;
       // gameOverMenu.SetActive(false);
        hpxpgoldMenu.SetActive(true);

        place = "MAIN";

        
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        place = "MAIN";
       // gameOverMenu.SetActive(false);

    }


    public void QuitButton()
    {
            Application.Quit();
            Debug.Log("Game is exiting");
        
    }

    public void returnFromSettings()
    {
        if(place == "MAIN")
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
           // hpxpgoldMenu.SetActive(false);
        }
        else if(place == "PAUSE")
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(false);
            pauseMenu.SetActive(true);
           // hpxpgoldMenu.SetActive(false);
        }
    }
}
