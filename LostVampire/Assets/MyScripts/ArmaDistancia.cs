using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDistancia : MonoBehaviour
{
    public enum TypeWeaponDist { normal = 0, clero = 1 };
    public TypeWeaponDist typeWeapon;
    public float DelayShoot;
    public Transform disparador;
    public GameObject[] prefbBullets;
    public bool canShoot = true;
   

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject bullet = Instantiate(prefbBullets[0], disparador.position,disparador.rotation) as GameObject;

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
