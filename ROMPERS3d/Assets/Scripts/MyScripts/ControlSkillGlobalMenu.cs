using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSkillGlobalMenu : MonoBehaviour
{
    public Text currentCash;
    public SlotsMatSkill[] slotsMatSkills;

    private int indexSlot;
    private bool canNextSelect;

    // Start is called before the first frame update
    void Start()
    {
        setSlots();
        upDateCurrentCash();
      //  upDateSlotsMatSkill();
    }

    // Update is called once per frame
    void Update()
    {
        // upDateCurrentCash();
        navigationMenu();
        purchaseNewMatSkill();
    }

    void navigationMenu()
    {
        Vector3 nav =InputControl.instance.getAxisControl();
        //Debug.Log(nav.ToString("F8"));

        if (nav.x > 0 && !canNextSelect)
        {
            canNextSelect = true;

            if (indexSlot < slotsMatSkills.Length-1) {
                indexSlot++;
            }
            else
            {
                indexSlot = 0;
            }
            CanvasControlParent.instance.setEffects.PlaySx("SxNavMenu");
        }
        else if (nav.x < 0 && !canNextSelect)
        {
            canNextSelect = true;

            if (indexSlot > 0) {

                indexSlot--;
            }
            else
            {
                indexSlot = slotsMatSkills.Length - 1;
            }
            CanvasControlParent.instance.setEffects.PlaySx("SxNavMenu");

        }
        else if (nav.x == 0)
        {
            canNextSelect = false;
        }

        navSelector();
    }

    void purchaseNewMatSkill()
    {
        if (InputControl.instance.getButtonsControl("Button1"))
        {
            if (!slotsMatSkills[indexSlot].getIsSelectorActive() && CanvasControlParent.instance.cash >= CanvasControlParent.instance.getAllMaterialSkill()[indexSlot].priceMatSkill)
            {
                CanvasControlParent.instance.getAllMaterialSkill()[indexSlot].isEnable = true;

                CanvasControlParent.instance.removeCash(CanvasControlParent.instance.getAllMaterialSkill()[indexSlot].priceMatSkill);
                CanvasControlParent.instance.setEffects.PlaySx("SxPurchaseSkill");

                //add fx
                setSlots();
                upDateCurrentCash();
            }
            else
            {
                Debug.Log("Not enougth money or ");
            }
        }
    }

    void upDateCurrentCash()
    {
        if (currentCash.text!=CanvasControlParent.instance.cash.ToString()) {
            currentCash.text = CanvasControlParent.instance.cash.ToString();
        }
    }

    void setSlots()
    {
        for (int i=0;i < slotsMatSkills.Length;i++)
        {
            for (int j=0;j < CanvasControlParent.instance.getAllMaterialSkill().Length;j++ ) {

                if ((int)slotsMatSkills[i].typeSmaterial == (int)CanvasControlParent.instance.getAllMaterialSkill()[j].superMat.typeSmaterial)
                {
                    slotsMatSkills[i].setValuePurchase(CanvasControlParent.instance.getAllMaterialSkill()[j].priceMatSkill);

                    if (CanvasControlParent.instance.getAllMaterialSkill()[j].isEnable)
                    {
                        slotsMatSkills[i].setSkillMat(true);
                       //activeMaterialSkills.Add(new MaterialSkillWallStats(CanvasControlParent.instance.getAllMaterialSkill()[j].priceMatSkill,true, CanvasControlParent.instance.getAllMaterialSkill()[j].superMat));
                    }
                    else
                    {
                        slotsMatSkills[i].setSkillMat(false);
                    }

                    if (indexSlot != i)
                    {
                        slotsMatSkills[i].OffSelector();
                    }
                }
            }
           
        }
    }

    void navSelector()
    {
        for (int i = 0; i < slotsMatSkills.Length; i++)
        {
            slotsMatSkills[i].OffSelector();
        }
        slotsMatSkills[indexSlot].onSelector();
    }

    void upDateSlotsMatSkill()
    {
        foreach (SlotsMatSkill slot in slotsMatSkills)
        {
            if ((int)slot.typeSmaterial==(int)CanvasControlParent.instance.getMaterialSkillOnUse().superMat.typeSmaterial)
            {
                Debug.Log(slot.typeSmaterial+" Activo");
            }
            else
            {
                Debug.Log(slot.typeSmaterial + "No Activo");

            }
        }
    }
}
