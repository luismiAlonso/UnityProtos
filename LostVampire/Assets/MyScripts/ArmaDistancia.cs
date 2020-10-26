using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetEffects))]
public class ArmaDistancia : MonoBehaviour
{
    public enum TypeWeaponDist { normal = 0, clero = 1 };
    public TypeWeaponDist typeWeapon;
    public float DelayShoot;
    public Transform disparador;
    public GameObject prefbBullets;
    public bool canShoot = true;

    SetEffects setEffects;

    private void Awake()
    {
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
            }
        }
    }

    public IEnumerator shootNormal()
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
            bullet.SetActive(true);
            bullet.transform.GetComponent<Bullets>().enabled = true;
            bullet.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GameObject bullet = Instantiate(prefbBullets, disparador.position, disparador.rotation);
            bullet.GetComponent<Bullets>().setIdParent(transform.parent.gameObject.GetInstanceID());

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
