using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] private Text textComp;

    [SerializeField] GameObject gameOverScreen;

    [SerializeField] GameObject gameOverMusic;

    [SerializeField] GameObject gameMusic;


    public void SetHealth(int health)
    {
        healthSlider.value = health;
        textComp.text = healthSlider.value.ToString();

        if(healthSlider.value <= 0)
        {
            gameOverScreen.SetActive(true);
            gameOverMusic.SetActive(true);
            gameMusic.SetActive(false);
        }
    }


    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
       // healthSlider.value = health;
    }
}
