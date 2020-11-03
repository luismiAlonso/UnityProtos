﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlInteract : MonoBehaviour
{
   
    [SerializeField]
    MeshRenderer meshRenderer;
    [HideInInspector]
    public bool isInShadow;
    [HideInInspector]
    public bool stunnedControl;

    PlayerControl playerControl;
    private float timeMana;
    private float timeLife;

    SetEffects setEffects;
    Coroutine coStun;
    SimpleIA simpleIA;
    // Start is called before the first frame update
    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        setEffects = GetComponent<SetEffects>();
        simpleIA = GetComponent<SimpleIA>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region Player Methods

    public float getLife()
    {
       return playerControl.setthing.life;
    }

    public float getMana()
    {
        return playerControl.setthing.mana;
    }

    //
    public void settingDamageLifeGlobal()
    {
        if (CanvasManager.instance.healhtBar.getActualHealth() <= 0 && !playerControl.checkers.isDead && !playerControl.checkers.invulnerability)
        {
            // rPGCharacterControllerFREE.Death();
            dead();
           
        }
        else if(!playerControl.checkers.isDead && !playerControl.checkers.invulnerability)
        {
            //damage enemy or others
            CanvasManager.instance.healhtBar.setDamageHealht(playerControl.setthing.damageSunLight);
            playerControl.setthing.life = CanvasManager.instance.healhtBar.getActualHealth();
        }
    }

    public void settingLife(float life)
    {
        CanvasManager.instance.healhtBar.setHealht(life);
        playerControl.setthing.life = CanvasManager.instance.healhtBar.getActualHealth();

        if (playerControl.setthing.life<=0)
        {
            if (GetComponent<BodyChange>().isPlayer) {
                playerControl.checkers.isDead = true;
                dead();
            }
            
        }
    }

    public void settingDamageLifeBySun()
    {
        if (timeLife >= playerControl.setthing.timeToReduceLife) {

            if (!isInShadow && CanvasManager.instance.healhtBar.getActualHealth() == 0 && !playerControl.checkers.isDead && !playerControl.checkers.invulnerability)
            {
                dead();
                // rPGCharacterControllerFREE.Death();
            }
            else if (!isInShadow && !playerControl.checkers.isDead && !playerControl.checkers.invulnerability)
            {
                //only damage sun
                CanvasManager.instance.healhtBar.setDamageHealht(playerControl.setthing.damageSunLight);
                playerControl.setthing.life = CanvasManager.instance.healhtBar.getActualHealth();
                //skinnedMeshRenderer.material.color = Random.ColorHSV();
            }

            timeLife = 0;
        }
        else
        {
            timeLife += Time.deltaTime;

        }
    }

  
    public void settingLifeBySun(float unitLife)
    {
        if (timeLife >= playerControl.setthing.timeToReduceLife)
        {
            //only damage sun
            CanvasManager.instance.healhtBar.setHealht(unitLife);
            playerControl.setthing.life = CanvasManager.instance.healhtBar.getActualHealth();
            //skinnedMeshRenderer.material.color = Random.ColorHSV();

        }
        else
        {

        }
    }

    public void settingDamageManaGlobal()
    {
        if (timeMana >= playerControl.setthing.timeToReduceMana)
        {
            //damage enemy or others
            CanvasManager.instance.manaBar.setDamageMana(playerControl.setthing.damageManaBodyChange);
            playerControl.setthing.mana = CanvasManager.instance.manaBar.getActualMana();
            // Manager.instance.mana = CanvasManager.instance.manaBar.getActualMana();
            timeMana = 0;

        }
        else
        {
            timeMana += Time.deltaTime;

        }
    }

    public void settingManaGlobal()
    {
        if (playerControl.setthing.mana < 1) {

            if (timeMana >= playerControl.setthing.timeToUPmana)
            {
                //damage enemy or others
                CanvasManager.instance.manaBar.setUpMana(playerControl.setthing.manaUpInShadow);
                playerControl.setthing.mana = CanvasManager.instance.manaBar.getActualMana();
                // Manager.instance.mana = CanvasManager.instance.manaBar.getActualMana();
                timeMana = 0;

            }
            else
            {
                timeMana += Time.deltaTime;

            }
        }
    }

    public void settingMana(float mana)
    {
        CanvasManager.instance.manaBar.setMana(mana);
        playerControl.setthing.mana = CanvasManager.instance.manaBar.getActualMana();
    }

    void dead()
    {
        //temporal
        if (playerControl.isActiveAndEnabled) {
            StartCoroutine("timeForRestart");
        }
    }

    IEnumerator timeForRestart()
    {
        playerControl.transform.eulerAngles = new Vector3(90, 0, 0);
        playerControl.GetRigidbody().useGravity = true;
        //playerControl.transform.GetComponent<Collider>().isTrigger = true;
        //GetComponent<BodyChange>().enabled = false;
        playerControl.enabled = false;
        yield return new WaitForSeconds(1.5f);
        Manager.instance.Restart();
    }

    public void stunnedPlayer(float timeStunned)
    {
        if (!stunnedControl)
        {
            if (setEffects.GetFX("fxStun") != null)
            {
                setEffects.PlayFx("fxStun");
            }
            if (setEffects.GetSX("sxStun") != null)
            {
                setEffects.GetSX("sxStun").Play();
            }
           
            coStun = StartCoroutine(IstunnedPlayer(timeStunned));
        }

       
    }

    IEnumerator IstunnedPlayer(float t)
    {
        while (t > 0 )
        {
            transform.Rotate(0, 1000 * Time.deltaTime, 0);
            t -= Time.deltaTime;

            stunnedControl = true;
            playerControl.checkers.canMove = false;
            playerControl.checkers.canJump = false;
            playerControl.checkers.canDash = true;
            playerControl.checkers.isStuned = true;
            yield return null;
        }
        //Debug.Log("antes de cancelar");
        setEffects.noneFx("fxStun");
        //Debug.Log("despues de cancelar");
        playerControl.checkers.canMove = true;
        playerControl.checkers.canJump = true;
        playerControl.checkers.canDash = false;
        playerControl.checkers.isStuned = false;    
        stunnedControl = false;
    }

    #endregion Player Methods


    #region NPC Methods

    public void stunnedNPC(SimpleIA simpleIA, float timeStunned)
    {
        if (!stunnedControl)
        {
            if (setEffects.GetFX("fxStun") != null)
            {
                setEffects.PlayFx("fxStun");
            }
            if (setEffects.GetSX("sxStun") != null)
            {
                setEffects.GetSX("sxStun").Play();
            }
            stunnedControl = true;
            simpleIA.setVisor(false);
            simpleIA.getNavMeshAgent().enabled = false;
            simpleIA.enabled = false;
            coStun= StartCoroutine(IstunnedNPC(timeStunned,simpleIA));
        }

       
    }

    IEnumerator IstunnedNPC(float t,SimpleIA sIA)
    {
        while (t>0 && !playerControl.checkers.isDominated)
        {

            transform.Rotate(0, 1000 * Time.deltaTime, 0);
            t -= Time.deltaTime;

            playerControl.checkers.isStuned = true;
            yield return null;
        }
        //Debug.Log("antes de cancelar");
        if (!playerControl.checkers.isDominated) {
            setEffects.noneFx("fxStun");
            playerControl.checkers.isStuned = false;
            sIA.getNavMeshAgent().enabled = true;
            sIA.enabled = true;
            sIA.setVisor(true);
            stunnedControl = false;
        }
        else
        {
            setEffects.noneFx("fxStun");
           // Debug.Log("despues de cancelar");
            playerControl.checkers.isStuned = false;
            sIA.getNavMeshAgent().enabled = false;
            sIA.enabled = false;
            sIA.setVisor(true);
            stunnedControl = false;
        }
    }

 
    #endregion NPC Methods


}
