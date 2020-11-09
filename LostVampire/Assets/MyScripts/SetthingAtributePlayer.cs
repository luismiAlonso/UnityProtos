using System;
using UnityEngine;
[Serializable]
public class SetthingAtributePlayer
{
    public float radiusHit;
    public float timeDelayToMove;
    public float life=1;
    public float mana=1;
    public float timeToReduceLife = 0.5f;
    public float timeToReduceMana = 0.5f;
    public float timeToUPmana = 0.5f;
    public float damageManaBodyChange=0.005f;
    public float manaUpInShadow= 0.005f;
    public float damageSunLight;
    [HideInInspector]
    public float distanceToBlockingDash;

}
