using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealhtBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    public bool isLocalCanvas;
    private float actualHealth;

    private void Update()
    {
        lookAtCamera();
    }

    public void setDamageHealht(float damage)
    {
        slider.value -= damage;
        actualHealth = slider.value;
    }

    public void setHealht(float life)
    {
        slider.value = life;
        actualHealth = slider.value;
    }

    public float getActualHealth()
    {
        return actualHealth;
    }

    void lookAtCamera()
    {
        if (isLocalCanvas) {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }

}
