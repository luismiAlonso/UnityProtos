using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaMelee : MonoBehaviour
{
    public enum TypeWeaponMelee { hit = 0, combo=1, areaShieldIA = 2 };
    public TypeWeaponMelee typeWeapon;
    public float maxRangeAreaAtack;
    public float timeTransitionAreaAtack;
    public PlayerControl MyPlayerControl;
    public float Damage;
    public bool active = false;

    private Vector3 scaleOriginalCollisionArea;
    private Vector3 maxArea;

    // Start is called before the first frame update
    void Start()
    {
        scaleOriginalCollisionArea = transform.localScale;
        maxArea = new Vector3(maxRangeAreaAtack, maxRangeAreaAtack, maxRangeAreaAtack);
        if (MyPlayerControl == null)
        {
            Debug.LogError("Need PLayerControl");
        }
    }


    private void Update()
    {
        resetShieldArea();
    }

    public void hitMelee()
    {
        switch (typeWeapon)
        {
            case TypeWeaponMelee.hit:
                active = true;
                ; break;
            case TypeWeaponMelee.combo:
                ; break;
            case TypeWeaponMelee.areaShieldIA:
                makeAreaShield();
                ; break;
        }

    }


    void resetShieldArea()
    {
        switch (typeWeapon)
        {
            case TypeWeaponMelee.hit:
                if (MyPlayerControl.checkers.canAtack)
                {
                    active = false;
                }
                ; break;
            case TypeWeaponMelee.combo:

                ; break;
            case TypeWeaponMelee.areaShieldIA:

                if (!Manager.instance.playerControl.checkers.isDetectado && !MyPlayerControl.checkers.canAtack)
                {
                    StopAllCoroutines();
                    StartCoroutine("IresetShield");
                    MyPlayerControl.checkers.canAtack = true;
                    active = false;
                }

                ; break;
           
        }
       
    }

    IEnumerator IresetShield()
    {
        while (Vector3.Distance(transform.localScale, scaleOriginalCollisionArea) > 0.1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleOriginalCollisionArea, Time.deltaTime * timeTransitionAreaAtack);
            yield return null;
        }
    }

    void makeAreaShield()
    {
        StartCoroutine("ImakeAreaShield");     
    }

    IEnumerator ImakeAreaShield()
    {
        while (Vector3.Distance(transform.localScale, maxArea) > 0.1f)
        {
            active = true;
            transform.localScale = Vector3.Lerp(transform.localScale, maxArea, Time.deltaTime * timeTransitionAreaAtack);
            yield return null;
        }

    }

    void takeDamageMeleeOnTrigger(Collider other)
    {
        if (other!=null) {
            other.GetComponent<ControlInteract>().settingLife(CanvasManager.instance.healhtBar.getActualHealth() - Damage);
            Vector3 dir = transform.position - other.transform.position;
            dir = -dir.normalized;
            dir = dir * 5;
            other.GetComponent<Rigidbody>().MovePosition(transform.position + dir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (typeWeapon)
        {
            case TypeWeaponMelee.hit:

                if (active && other.transform.tag == "NPC" && other.transform.GetComponent<ControlInteract>()!=null 
                    && other.transform.GetComponent<SimpleIA>().typeNPC==SimpleIA.TypeNPC.clero)    //solo afecta a clero
                {
                   // other.transform.GetComponent<ControlInteract>().stunnedNPC(other.transform.GetComponent<SimpleIA>(),2);
                }
                ; break;
            case TypeWeaponMelee.combo:

                ; break;
            case TypeWeaponMelee.areaShieldIA:

                if (other.transform.name == "player")
                {
                    takeDamageMeleeOnTrigger(other);
                }

                ; break;

        }
       
    }
}
