using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetEffects))]
public class ArmaDistancia : MonoBehaviour
{
    public enum TypeWeaponDist { normal = 0, clero = 1,tanque=2 };
    public TypeWeaponDist typeWeapon;
    public float DelayShoot;
    public Transform disparador;
    public GameObject prefbBullets;
    public bool canShoot = true;

    PlayerControl playerControl;
    SetEffects setEffects;

    private void Awake()
    {
        playerControl = transform.parent.GetComponent<PlayerControl>();
        setEffects = GetComponent<SetEffects>();
    }
    public void shooting()
    {

        if (canShoot)
        {
            switch (typeWeapon)
            {
                case TypeWeaponDist.normal:
                    StartCoroutine("shootNormal");
                   // Debug.Log("normal");
                    ; break;
                case TypeWeaponDist.clero:
                    StartCoroutine("shootNormal");
                   // Debug.Log("clero");
                    ; break;
                case TypeWeaponDist.tanque:
                    dropBomb();
                   // Debug.Log("clero");
                   ; break;
            }
        }
    }

    void dropBomb()
    {
        //Instantiate your projectile
        if (setEffects.GetSX("sxStump") != null)
        {
            setEffects.GetSX("sxStump").Play();
        }

        if (setEffects.GetFX("fxStump") != null)
        {
            //setEffects.PlayFx("fxStump");
            //Debug.Log(setEffects.GetFX("fxBomb"));

           // GameObject bomb = Instantiate(setEffects.GetFX("fxBomb").gameObject, disparador.position, disparador.rotation);
        }
    }

    IEnumerator shootNormal()
    {
        //Instantiate your projectile
        if (setEffects.GetSX("sxFireBullet")!=null)
        {
            setEffects.GetSX("sxFireBullet").Play();
        }

        if (setEffects.GetFX("fxFireBullet") != null)
        {
           
            GameObject bullet = Instantiate(setEffects.GetFX("fxFireBullet").gameObject, disparador.position, disparador.rotation);
            bullet.GetComponent<Bullets>().setIdParent(transform.parent.gameObject.GetInstanceID());
            bullet.GetComponent<Bullets>().enemyDominated = playerControl.checkers.isDominated;
            bullet.SetActive(true);
            bullet.transform.GetComponent<Bullets>().enabled = true;
            bullet.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GameObject bullet = Instantiate(prefbBullets, disparador.position, disparador.rotation);
            bullet.GetComponent<Bullets>().setIdParent(transform.parent.gameObject.GetInstanceID());
            bullet.GetComponent<Bullets>().enemyDominated = playerControl.checkers.isDominated;

        }

            canShoot = false;
            //wait for some time
            yield return new WaitForSeconds(DelayShoot);
            canShoot = true;
        
    }

    public void ShootingRemote()
    {
        if (canShoot)
        {
            switch (typeWeapon)
            {
                case TypeWeaponDist.normal:
                    StartCoroutine("shootNormal");
                    // Debug.Log("normal");
                    ; break;
                case TypeWeaponDist.clero:
                    StartCoroutine("shootNormal");
                    // Debug.Log("clero");
                    ; break;
            }
        }
    }
}
