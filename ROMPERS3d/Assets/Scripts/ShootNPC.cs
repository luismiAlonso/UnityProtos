using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNPC : MonoBehaviour
{
    public enum TypeShoot { normal, rafaga ,multiple, sostenido }
    public TypeShoot typeShoot;

    public float delayShoot;
    public float timeRafaga;
    public int numBullets;
    public GameObject prefBullet;
    public Transform disparador;

    private bool activeShoot;
 

    public void shoot()
    {
        if (typeShoot==TypeShoot.normal) {

            GameObject bullet = Instantiate(prefBullet, disparador.position, Quaternion.identity);
            bullet.transform.eulerAngles = transform.eulerAngles;

        }else if (typeShoot == TypeShoot.rafaga)
        {
            //IEnumerator
            rafagaShoot();
        }

    }


    void rafagaShoot()
    {
        for (int i=0;i<numBullets;i++)
        {
            StartCoroutine("IEdelayShoot");
        }
    }

    IEnumerator IEdelayShoot()
    {
        yield return new WaitForSeconds(delayShoot);
        GameObject bullet = Instantiate(prefBullet, disparador.position, Quaternion.identity);

    }


}
