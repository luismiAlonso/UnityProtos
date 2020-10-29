using System;
using UnityEngine;
[Serializable]
public class SetthingAtributePlayer
{
    public float speedMove;
    public float speedRotationLookMouse;
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
    public float TimeDurationArcJump;
    public float TimeDurationDash;
    public float TimeDurationPropulsion;
    public float delayDash;
    public float forceDash;
    public float distanceDash;
    [HideInInspector]
    public float distanceToBlockingDash;
    public float distanceJumpArc;
    public float forceJump;
    public float fallMultiplier;

}
