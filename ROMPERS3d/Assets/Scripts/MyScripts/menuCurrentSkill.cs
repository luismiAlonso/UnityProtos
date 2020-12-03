using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCurrentSkill : MonoBehaviour
{
    public Image imageSkillWall;
    public Text numCreateText;
    public Text cashMoney;
    public int maxIncrement=3;

    private int indexSkillWall;

    // Start is called before the first frame update
    void Start()
    {
        imageSkillWall.sprite = CanvasControlParent.instance.getMaterialSkillOnUse().superMat.spriteWallMenu;
        indexSkillWall = CanvasControlParent.instance.indexSuperMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        changeSkillWall();
    }

    void changeSkillWall()
    {

        if (InputControl.instance.getButtonsControl("Button4"))
        {
           // Debug.Log((indexSkillWall + 1) +" // "+ CanvasControlParent.instance.getAllMaterialSkillEnable().Length);

            if ((indexSkillWall + 1) < CanvasControlParent.instance.getAllMaterialSkillEnable().Length)
            {
                indexSkillWall++;

            }
            else
            {
                indexSkillWall = 0; //loop

            }

            if (CanvasControlParent.instance.isMaterialSkillEnable(indexSkillWall))
            {
                CanvasControlParent.instance.indexSuperMaterial = indexSkillWall;
                imageSkillWall.sprite = CanvasControlParent.instance.getMaterialSkillOnUse().superMat.spriteWallMenu;
            }
            else
            {
                Debug.Log("no esta disponible");
            }

        }
        else if (InputControl.instance.getButtonsControl("Button5"))
        {

            if ((indexSkillWall - 1) >=0)
            {
                indexSkillWall--;

            }
            else
            {
                indexSkillWall = CanvasControlParent.instance.getAllMaterialSkillEnable().Length - 1;

            }

            if (CanvasControlParent.instance.isMaterialSkillEnable(indexSkillWall))
            {
                CanvasControlParent.instance.indexSuperMaterial = indexSkillWall;
                imageSkillWall.sprite = CanvasControlParent.instance.getMaterialSkillOnUse().superMat.spriteWallMenu;

            }
            else
            {
                Debug.Log("no esta disponible");
            }

        }
    }

    public void setNumCreate(int value)
    {
        numCreateText.text = value.ToString();
    }

    public void setCurrentMoney(int currentMoney)
    {
        cashMoney.text = currentMoney.ToString();
    }
}
