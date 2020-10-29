using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaMelee : MonoBehaviour
{
    public enum TypeWeaponMelee { hit = 0, combo=1, areaShieldIA = 2,stump=3 };
    public TypeWeaponMelee typeWeapon;
    public float maxRangeAreaAtack;
    public float timeTransitionAreaAtack;
    public PlayerControl MyPlayerControl;
    public float Damage;
    public bool active = false;

    private Vector3 scaleOriginalCollisionArea;
    private Vector3 maxArea;
    private SetEffects setEffects;
    // Start is called before the first frame update
    void Start()
    {
        setEffects = GetComponent<SetEffects>();
        scaleOriginalCollisionArea = transform.localScale;
        maxArea = new Vector3(maxRangeAreaAtack, maxRangeAreaAtack, maxRangeAreaAtack);
        if (MyPlayerControl == null)
        {
            Debug.LogError("Need PLayerControl");
        }
    }


    private void Update()
    {
        resetArea();
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

                if (setEffects.GetFX("fxCupule")!=null)
                {
                    setEffects.PlayFx("fxCupule");
                }
                if (setEffects.GetSX("sxCupula")!=null && !active)
                {
                    setEffects.GetSX("sxCupula").Play();
                }
                makeAreaShield();

                ; break;
            case TypeWeaponMelee.stump:

                if (setEffects.GetFX("fxStump") != null)
                {
                    setEffects.PlayFx("fxStump");
                }
                if (setEffects.GetSX("sxStump") != null && !active)
                {
                    setEffects.GetSX("sxStump").Play();
                }
                makeStump();
                ; break;
        }

    }


    public void resetArea()
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

                if (Manager.instance.playerControl!=null && !Manager.instance.playerControl.checkers.isDetectado && !MyPlayerControl.checkers.canAtack && active)
                {
                    if (setEffects.GetFX("fxCupule")!=null)
                    {
                        setEffects.noneFx("fxCupule");
                    }
                    if (setEffects.GetFX("fxCupuleDead")!=null)
                    {
                        setEffects.PlayFx("fxCupuleDead");
                    }
                    if (setEffects.GetSX("sxCupulaFin")!=null && active)
                    {
                        setEffects.GetSX("sxCupulaFin").Play();
                    }

                    StopAllCoroutines();
                    StartCoroutine("IresetShield");
                }

                ; break;

            case TypeWeaponMelee.stump:

               
                ; break;

        }
       
    }

    public bool isActiveShield()
    {
        return active;
    }

    IEnumerator IresetShield()
    {
        active = false;

        while (Vector3.Distance(transform.localScale, scaleOriginalCollisionArea) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleOriginalCollisionArea, Time.deltaTime * timeTransitionAreaAtack);
            yield return null;
        }
        MyPlayerControl.checkers.canAtack = true;
    }

    void makeAreaShield()
    {
        StartCoroutine("ImakeAreaShield");     
    }

    void makeStump()
    {

        StartCoroutine("ImakeStump");

    }

    IEnumerator ImakeStump()
    {
        while (Vector3.Distance(transform.localScale, maxArea) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxArea, Time.deltaTime * timeTransitionAreaAtack);
            active = true;
            yield return null;
        }

        active = false;

        while (Vector3.Distance(transform.localScale, Vector3.zero) > 0.01f)
        {
            Debug.Log("disminuye");
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * timeTransitionAreaAtack);
            yield return null;
        }
    }

    
    IEnumerator ImakeAreaShield()
    {
        while (Vector3.Distance(transform.localScale, maxArea) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxArea, Time.deltaTime * timeTransitionAreaAtack);
            active = true;
            yield return null;
        }
    }

    void takeDamageMeleeOnTrigger(Collider other)
    {
        if (other!=null && active) {

            if (setEffects.GetSX("sxCupulaRepulsion")!=null)
            {
                setEffects.GetSX("sxCupulaRepulsion").Play();
            }

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

                if (other.transform.name == "player" && active)
                {
                    takeDamageMeleeOnTrigger(other);
                }

                ; break;
            case TypeWeaponMelee.stump:

                if (other.transform.tag == "Player" && active)
                {
                   // takeDamageMeleeOnTrigger(other);

                }else if (other.transform.tag == "NPC" && active)
                {

                }

                ; break;

        }
       
    }

    
}
