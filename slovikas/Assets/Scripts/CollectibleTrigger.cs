using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CollectibleTrigger : MonoBehaviour
{
    [SerializeField]
    private string healthPotionTag = "HealthPotion";

    [SerializeField]
    private string manaPotionTag = "ManaPotion";

    [SerializeField]
    private string gold1Tag = "Gold1";

    [SerializeField, Min(0)]
    private int healthValue = 25;

    [SerializeField, Min(0)]
    private int manaValue = 33;

    [SerializeField, Min(0)]
    private int gold1Value = 1;

    private Player player;

    private Object thisObject;

    private void Awake()
    {
        player = GetComponent<Player>();

        thisObject = GetComponent<Object>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        var collected = false;

        if (isHealthPotion(otherGameObject))
        {
            player.AddHealth(healthValue);
            collected = true;
        }
        else if (isManaPotion(otherGameObject))
        {
            player.AddMana(manaValue);
            collected = true;
        }
        else if(isGold1(otherGameObject))
        {
           player.AddGold(gold1Value);
            collected = true;
        }

        if (collected)
        {
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
            
        }
    }

    private bool isHealthPotion(GameObject obj)
    {
        return obj.CompareTag(healthPotionTag);
    }

    private bool isManaPotion(GameObject obj)
    {
        return obj.CompareTag(manaPotionTag);
    }
    
    private bool isGold1(GameObject obj)
    {
        return obj.CompareTag(gold1Tag);
    }



}

