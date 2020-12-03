using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MaterialSkillWallStats 
{
    public int priceMatSkill;
    public bool isEnable;
    public SuperMaterial superMat;

    public MaterialSkillWallStats(int priceM, bool enableMat ,SuperMaterial sMat)
    {
        priceMatSkill = priceM;
        isEnable = enableMat;
        superMat = sMat;
    }
}
