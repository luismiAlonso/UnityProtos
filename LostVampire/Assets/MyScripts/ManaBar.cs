using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    private float actualMana;

    public void setDamageMana(float mana)
    {
        slider.value -= mana;
        actualMana = slider.value;
    }

    public void setUpMana(float mana)
    {
        slider.value += mana;
        actualMana = slider.value;
    }

    public void setMana(float mana)
    {
        slider.value = mana;
        actualMana = slider.value;
    }

    public float getActualMana()
    {
        return actualMana;
    }
}
