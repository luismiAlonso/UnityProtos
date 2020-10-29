using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SettingAtacksIA 
{
    public float distanceAtack;
    public float distanceAtackMelee;
    public float timeUseArmaDistancia;
    public float timeUseMelee;
    public bool isPrepareNextAtack;
    public int indexArma;
    public ArmaDistancia[] Arma;
    public ArmaMelee[] ArmaMelee;

}
