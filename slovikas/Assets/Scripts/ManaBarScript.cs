using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider manaSlider;
    [SerializeField] private Text textComp;




    public void SetMana(int mana)
    {
        manaSlider.value = mana;
        textComp.text = manaSlider.value.ToString();

    }


    public void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
        manaSlider.value = mana;
    }
}
