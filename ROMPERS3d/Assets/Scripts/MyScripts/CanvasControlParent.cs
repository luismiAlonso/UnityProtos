using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControlParent : MonoBehaviour
{
    #region Singleton
    private static CanvasControlParent canvasControlParent;

    public static CanvasControlParent instance
    {
        get
        {
            if (canvasControlParent == null)
            {
                canvasControlParent = FindObjectOfType<CanvasControlParent>();
            }
            return canvasControlParent;
        }
    }

    #endregion Singleton

    public int cash;
    public MaterialSkillWallStats[] listMaterialSkilStats;
    public int indexSuperMaterial;
    public menuCurrentSkill menuCurrent;
    public ControlSkillGlobalMenu globalMenu;

    public bool isActiveGlobalMenu;

    [HideInInspector]
    public SetEffects setEffects;
    // Start is called before the first frame update
    void Start()
    {
        globalMenu.gameObject.SetActive(false);
        isActiveGlobalMenu = false;
        setItemMoneyItem(0);
        setEffects = GetComponent<SetEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        inputsMenu();
    }

    void inputsMenu()
    {

       if (InputControl.instance.getButtonsControl("Button2") && Manager.instance.onTriggerMenu)
        {
            if (isActiveGlobalMenu)
            {
                globalMenu.gameObject.SetActive(false);
                isActiveGlobalMenu = false;
                menuCurrent.gameObject.SetActive(true);
                Manager.instance.fullStop = false;
                Manager.instance.onTriggerMenu = false;
                Manager.instance.playerControl.getRigidBody().isKinematic = false;
                setEffects.PlaySx("SxCloseMenu");

            }
            else
            {
                globalMenu.gameObject.SetActive(true);
                isActiveGlobalMenu = true;
                menuCurrent.gameObject.SetActive(false);
                Manager.instance.fullStop = true;
                Manager.instance.playerControl.getAnim().SetBool("isMoving", false);
                Manager.instance.playerControl.getRigidBody().isKinematic = true;
                setEffects.PlaySx("SxOpenMenu");
            }
        }
    }

    public void openMenu()
    {
        if (isActiveGlobalMenu)
        {
            globalMenu.gameObject.SetActive(false);
            isActiveGlobalMenu = false;
            menuCurrent.gameObject.SetActive(true);
            Manager.instance.fullStop = false;
        }
        else
        {
            globalMenu.gameObject.SetActive(true);
            isActiveGlobalMenu = true;
            menuCurrent.gameObject.SetActive(false);
            Manager.instance.fullStop = true;

        }
    }


    public MaterialSkillWallStats getMaterialSkillOnUse()
    {
        return getAllMaterialSkillEnable()[indexSuperMaterial];
    }

    public MaterialSkillWallStats getMaterialSkillByIndex(int idexMat)
    {
        return listMaterialSkilStats[idexMat];
    }

    public bool isMaterialSkillEnable(int index)
    {
        return getAllMaterialSkillEnable()[index].isEnable;
    }

    public MaterialSkillWallStats[] getAllMaterialSkillEnable()
    {
        List<MaterialSkillWallStats> enableMats = new List<MaterialSkillWallStats>();
        foreach (MaterialSkillWallStats matSkill in listMaterialSkilStats)
        {
            if (matSkill.isEnable)
            {
                enableMats.Add(matSkill);
            }
        }
        return enableMats.ToArray();
    }

    public MaterialSkillWallStats[] getAllMaterialSkill()
    {
        List<MaterialSkillWallStats> enableMats = new List<MaterialSkillWallStats>();
        foreach (MaterialSkillWallStats matSkill in listMaterialSkilStats)
        {
            enableMats.Add(matSkill);
        }
        return enableMats.ToArray();
    }

    public void setItemMoneyItem(int value)
    {
        cash += value;
        menuCurrent.setCurrentMoney(cash);
    }

    public void removeCash(int value)
    {
        if ((cash-value)>=0) {

            cash -= value;
            menuCurrent.setCurrentMoney(cash);
        }
        else
        {
            menuCurrent.setCurrentMoney(0);

        }
    }
}
