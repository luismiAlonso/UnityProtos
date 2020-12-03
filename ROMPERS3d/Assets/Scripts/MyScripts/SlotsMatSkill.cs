using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotsMatSkill : MonoBehaviour
{
    public enum TypeSmaterial { barro, metal, lava, hielo, goma }
    public TypeSmaterial typeSmaterial;
    
    public Image imageAdd;
    public Image imageSelector;
    public Text priceMat;

    private bool active;
    private bool isSelectorActive;

    public void setValuePurchase(int _priceMat)
    {
        priceMat.text = _priceMat.ToString();
    }
   
    public void setSkillMat(bool b)
    {
        active = b;

        if (active)
        {
            imageAdd.gameObject.SetActive(false);
        }
        else
        {
            imageAdd.gameObject.SetActive(true);

        }
    }

    public bool isActiveMat()
    {
        return active;
    }

    

    public void onSelector()
    {
        imageSelector.gameObject.SetActive(true);
        isSelectorActive = true;
    }

    public void OffSelector()
    {
       
        imageSelector.gameObject.SetActive(false);
        isSelectorActive = false;
    }

    public bool getIsSelectorActive()
    {
        return isSelectorActive;
    }
}
