using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Player state")]
    [SerializeField, Min(0)]
    public int health = 100;

    [SerializeField, Min(0)]
    public int maxHealth = 100;

    [SerializeField, Min(0)]
    public int mana = 100;

    [SerializeField, Min(0)]
    public int damage = 20;

    [SerializeField, Min(0)]
    private int gold = 0;

    [SerializeField, Min(0)]
    public double criticalChance = 0;

    [SerializeField, Min(0)]
    public double attackSpeed = 0.8;

    [SerializeField, Min(0)]
    public float movementSpeed = 3;

    [SerializeField]
    private bool isVulnerabe = true;

    [SerializeField, Min(0)]
    private int invulnerabilityTime = 1;


    [Header("UI")]
    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text manaText;

    [SerializeField]
    private Text goldText;

    private PlayerController playerController;

    public HealthBarScript healthBar;
    public ManaBarScript manaBar;
    public GoldShowingScript goldAmount;

    

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        healthText = GameObject.Find("HealthUIText").GetComponent<Text>();
        manaText = GameObject.Find("ManaUIText").GetComponent<Text>();
        goldText = GameObject.Find("GoldUIText").GetComponent<Text>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBarScript>();
        manaBar = GameObject.Find("ManaBar").GetComponent<ManaBarScript>();
        goldAmount = GameObject.Find("Gold").GetComponent<GoldShowingScript>();

        health = 100;
        mana = 100;
        gold = 0;

        instance = this;
    }
    public void Update()
    {
        
        UpdateLivesText();
        UpdateManaText();
        UpdateGoldText();
        healthBar.SetHealth(health);
        manaBar.SetMana(mana);
        goldAmount.SetGold(gold);
        healthBar.SetMaxHealth(maxHealth);

    }

    private void UpdateLivesText()
    {
        healthText.text = $"Health: {health}";
       
    }

    private void UpdateManaText()
    {
        manaText.text = $"Mana: {mana}";
    }

    private void UpdateGoldText()
    {
        goldText.text = $"{gold}";
    }

    public IEnumerator StartInvulnerability()
    {
        isVulnerabe = false;
        yield return new WaitForSeconds(invulnerabilityTime);
        isVulnerabe = true;
    }

    public void TakeDamage(int amount)
    {
        if (isVulnerabe == true)
        {
            if (health - amount <= 0 || health <= 0)
            {
                health = 0;
             
                UpdateLivesText();
                
                StopGame();                    
            }
            else
            {
                health -= amount;
               
                UpdateLivesText();
                StartCoroutine(StartInvulnerability());

            }       
        }
    }

    public IEnumerator StartManaRestore(int restore)
    {
        for(int i = 0; i < restore; i++){

            yield return new WaitForSeconds(1);
            mana += 1;
            UpdateManaText();

            if(mana >= 10)
                yield break;
        }
    }

    public void TakeMana(int amount){

        if(mana - amount <= 0 || mana <= 0){

            mana = 0;
            UpdateManaText();
            StartCoroutine(StartManaRestore(10));

        }
        else if(mana - amount > 0 && mana - amount < 10){

            mana -= amount;
            UpdateManaText();
            StartCoroutine(StartManaRestore(10 - mana));
        }
        else{
            mana -= amount;
            UpdateManaText();

        }
    }
      
    private void StopGame()
    {
        playerController.enabled = false;
        healthText.gameObject.SetActive(false);
        manaText.gameObject.SetActive(false);
        goldText.gameObject.SetActive(false);

       // SceneManager.LoadScene("GameOver");

    }
   

    public void AddHealth(int value)
    {
        if(health + value >= maxHealth)
        {
            health = maxHealth;
         
            UpdateLivesText();
        }
        else 
        {
            health += value;
           
            UpdateLivesText();
        }
        
    }

    public void AddMana(int value)
    {
        if(mana + value >= 100)
        {
            mana = 100;
            UpdateManaText();
        }
        else
        {
            mana += value;
            UpdateManaText();
        }
    }

    public void AddGold(int value)
    {
        gold += value;
        UpdateGoldText();
    }

    // private void OnTriggerEnter2D(Collider2D other){

    //     if(other.tag == "EnemyDemon"){
            
    //         TakeDamage(5);
    //     } 
    // }
}
