using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorControl))]
public class CombatPLayer : MonoBehaviour
{
    PlayerControl playerControl;
    public SettingArmasMelee settingArmasMelee;
    public AnimatorControl animaControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        atackMeleeBase();
    }

    void atackMeleeBase()
    {
        if (Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().isInShadow)
        {
            if (Input.GetButtonDown("buttonB"))
            {
               // StartCoroutine("IatackMeleeBase");
            }
        }
        else
        {
            if (Input.GetButtonDown("buttonB"))
            {
               // Debug.Log("en sombra");
                
            }
        }
    }

    IEnumerator IatackMeleeBase()
    {
        animaControl.getAnimator("super").SetTrigger("atackMeleeBase");
        settingArmasMelee.armasMelee[settingArmasMelee.indexArmaMelee].hitMelee();
        playerControl.checkers.canMove = false;
        playerControl.checkers.canAtack = false;
        playerControl.GetRigidbody().velocity = Vector3.zero;
        yield return new WaitForSeconds(settingArmasMelee.timeAtackMelee);
        playerControl.checkers.canMove = true;
        playerControl.checkers.canAtack = true;

    }

}
