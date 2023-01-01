using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvasControl : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject levelUpMenu;
    //[SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hpxpgoldMenu;

    [SerializeField] GameObject gameHud;

    //[SerializeField] GameObject gameOverMusic;

  

    public static string place = "MAIN";

    public static bool isGamePaused = false;

    //settings
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;


    Resolution[] resolutions;


    Resolution cacheResolution;

    //----------------------settings------------------
    private void Start()
    {
        cacheResolution.width = 0;
        cacheResolution.height = 0;

        Time.timeScale = 0;
        isGamePaused = true;

        //resolutions = Screen.resolutions;

         resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
       
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        audioMixer.SetFloat("volume", 0);
     

  

        for (int i = 0; i < resolutions.Count(); i++)
        {
            
            string option = resolutions.ElementAt(i).width + "x" + resolutions.ElementAt(i).height;
            options.Add(option);

            if (resolutions.ElementAt(i).width == Screen.currentResolution.width &&
                resolutions.ElementAt(i).height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions.ElementAt(resolutionIndex);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {

        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    //--------------------------------------------------------------------------
    //----------------------------------pause
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
        place = "PAUSE";

        pauseMenu.SetActive(false);
        gameHud.SetActive(true);
        hpxpgoldMenu.SetActive(true);
        levelUpMenu.SetActive(false);


        Time.timeScale = 1;

        isGamePaused = false;
    }

    void GamePause()
    {
        place = "PAUSE";
        gameHud.SetActive(false);
        pauseMenu.SetActive(true);

        hpxpgoldMenu.SetActive(false);

        Time.timeScale = 0;

        isGamePaused = true;
    }
    public void GameLevelUp()
    {
        gameHud.SetActive(false);
        levelUpMenu.SetActive(true);

        hpxpgoldMenu.SetActive(false);

        Time.timeScale = 0;

        isGamePaused = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("game");
        place = "MAIN";
        //pauseMenu.SetActive(false);
        Time.timeScale = 0;

        isGamePaused = true;
    }

    public void QuitGameButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting");

    }



    //-----------------------------------------


    public void StartButton()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        place = "MAIN";
        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);

       // hpxpgoldMenu.SetActive(true);

       

    }

    public void SettingsMainButton()
    {
        Time.timeScale = 0;
        isGamePaused = true;

        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);

        hpxpgoldMenu.SetActive(false);

       

        place = "MAIN";

    }

    public void QuitButton()
    { 
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void returnFromSettings()
    {
        if (place == "MAIN")
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
            hpxpgoldMenu.SetActive(false);

           

            Time.timeScale = 0;
            isGamePaused = true;
            place = "MAIN";
        }
        else if (place == "PAUSE")
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(false);
            pauseMenu.SetActive(true);

            

            

            Time.timeScale = 0;
            isGamePaused = true; 
            place = "PAUSE";

        }
    }

}
