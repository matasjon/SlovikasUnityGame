using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldShowingScript : MonoBehaviour
{

    
    [SerializeField] private Text textComp;

    public void SetGold(int gold)
    {
        textComp.text = gold.ToString();
    }

}
