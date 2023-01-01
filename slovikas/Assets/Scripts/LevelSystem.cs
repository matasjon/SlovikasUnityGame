using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSystem : MonoBehaviour
{

    public static LevelSystem instance;
    public int level =1;
    public float currentXP = 0;
    public float requiredXP =100;

    private float lerpTimer;
    private float delayTimer;

    [SerializeField]
    public Player player;

    [Header("UI")]
    public Image FIllXPImage;
    public Image UpdateXPImage;

    [SerializeField]
    private Text LevelText;

    [SerializeField]
    private Text DMGBUttonText;

    [SerializeField]
    private Text HPButtonText;

    [SerializeField]
    private Text MaxHPButtonText;

    MainCanvasControl mainCanvas;


    private void Awake(){

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FIllXPImage.fillAmount = currentXP / requiredXP;
        UpdateXPImage.fillAmount = currentXP / requiredXP;
        mainCanvas = gameObject.transform.parent.parent.GetChild(0).transform.GetComponent<MainCanvasControl>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
    }

    public void LevelUpgrade(bool isEnemyKilled){

        if (isEnemyKilled)
        {
            //FIllXPImage.fillAmount += 0.2f;
            GainExperienceFlatRate(23);

            isEnemyKilled = false;
        }

        if(currentXP >= requiredXP)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXP / requiredXP;
        float FXP = FIllXPImage.fillAmount;
        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            UpdateXPImage.fillAmount = xpFraction;
            //if(delayTimer > 3)
           // {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                FIllXPImage.fillAmount = Mathf.Lerp(FXP, UpdateXPImage.fillAmount, percentComplete);
            //}
        }
    }

    public void GainExperienceFlatRate(int xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        FIllXPImage.fillAmount = 0;
        UpdateXPImage.fillAmount = 0;
        currentXP = currentXP - requiredXP;
        requiredXP = Convert.ToInt32(requiredXP + 0.35 * requiredXP);
        LevelText.text = level.ToString();
        mainCanvas.GameLevelUp();
        updateText();
    }

    public void assignPlayer(Player player)
    {
        this.player = player;
    }
    public void updateText()
    {
        UpdateDMGButtonText();
        UpdateHPButtonText();
        UpdateMaxHPButtonText();
    }

    private void UpdateDMGButtonText()
    {
        DMGBUttonText = GameObject.Find("DMGButtonText").GetComponent<Text>();
        DMGBUttonText.text = $"Damage + {level / 2 + 3}";
    }
    private void UpdateHPButtonText()
    {
        HPButtonText = GameObject.Find("HPButtonText").GetComponent<Text>();
        HPButtonText.text = $"Health + {player.maxHealth / 3}";
    }
    private void UpdateMaxHPButtonText()
    {
        MaxHPButtonText = GameObject.Find("MaxHPButtonText").GetComponent<Text>();
        MaxHPButtonText.text = $"Max Health + {level * 2.5 + 5}";
    }

    public void IncreaseDamage()
    {
        player.damage += level/2 + 3;
    }

    public void IncreaseHealth()
    {
        player.health += player.maxHealth / 3;
    }

    public void IncreaseAttackSpeed()
    {
        player.attackSpeed -= (player.attackSpeed /10);
    }

    public void IncreaseCriticalChance()
    {
        player.criticalChance += 3;
    }

    public void IncreaseMovementSpeed()
    {
        player.movementSpeed += 0.2f;
    }

    public void IncreaseMaxHealth()
    {
        player.maxHealth += Convert.ToInt32(level * 2.5 + 5);
    }
}
