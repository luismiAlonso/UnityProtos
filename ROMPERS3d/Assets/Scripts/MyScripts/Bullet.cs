﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum TypeBullet { normal, explosion, parabole, reflect }
    public TypeBullet typeBullet;

    public float damage;
    public float speedBullet;
    public float TimeToDestroy;
    public int numColisions;
    public LayerMask layerColEffect;
    Rigidbody rg;

    private SetEffects setEffects;
    Vector3 currentVelocity;
    int auxNumColision;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        auxNumColision = numColisions;
        setEffects = GetComponent<SetEffects>();
        setEffects.PlaySx("SxFire");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if (!Manager.instance.fullStop) {

            rg.isKinematic = false;

            if (typeBullet == TypeBullet.reflect || typeBullet == TypeBullet.normal)
            {
                 currentVelocity = transform.forward * speedBullet;

                if (!ReflectProjectile(currentVelocity).Equals(Vector3.zero))
                {
                    rg.velocity = ReflectProjectile(currentVelocity);
                }
                else if (ReflectProjectile(currentVelocity).Equals(Vector3.zero) && auxNumColision == numColisions)
                {
                    rg.velocity = currentVelocity;
                }
            }
        }
        else
        {
            rg.isKinematic = true;
        }
    }

    private Vector3 ReflectProjectile(Vector3 currentVelocity)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, currentVelocity);
        Vector3 velocity=Vector3.zero;

        if (Physics.Raycast(ray, out hit,0.5f, layerColEffect))
        {
            if (hit.transform.GetComponent<IWall>()!=null && hit.transform.GetComponent<IWall>().getTypeWall() == "goma") {

                velocity = Vector3.Reflect(currentVelocity, hit.normal);

                if (numColisions > 0) {

                    numColisions--;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        return velocity;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag=="Player")
        {

            // Destroy(other.gameObject);
            other.transform.position = Manager.instance.checkPoints[0].position;

        }else if (other.transform.tag == "movilWall")
        {

            if (typeBullet == TypeBullet.normal)
            {

                if (other.transform.GetComponent<IWall>().getTypeWall()=="barro") {

                    other.transform.GetComponent<IWall>().DesgasteWall(damage);
                    Destroy(gameObject);
                }
                else if (other.transform.GetComponent<IWall>().getTypeWall() == "metal" )
                {
                    Destroy(gameObject);
                    
                }
            }else if (typeBullet == TypeBullet.reflect)
            {

                if (other.transform.GetComponent<IWall>().getTypeWall() == "barro")
                {
                    other.transform.GetComponent<IWall>().DesgasteWall(damage);
                    Destroy(gameObject);
                }
                else if (other.transform.GetComponent<IWall>().getTypeWall() == "metal")
                {
                    Destroy(gameObject);
                }
            }

        }else if (other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);

        }else if (other.transform.tag == "ground")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (typeBullet == TypeBullet.normal)
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.transform.tag == "movilWall")
        {
            Debug.Log("hola??");
        }
    }
}