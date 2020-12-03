using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float maxExplosion;
    public LayerMask layerMask;
    public float TimeToExplosion;
    public float speedExplosion;

    float currentSpeedExplosion;
    float currentTimeToExplosion;
    float radioActual;
    RaycastHit hit;
    bool activeExplosion;
   
    void FixedUpdate()
    {
        if (activeExplosion) {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radioActual, layerMask);

            for (int i=0;i<hitColliders.Length;i++)
            {
                if (hitColliders[i].transform.tag=="PLayer")
                {
                   // Destroy(hitColliders[i].gameObject);
                }
            }
        }
      
    }

    private void Update()
    {
        if (currentTimeToExplosion >= TimeToExplosion)
        {
            activeExplosion = true;
            makeRangeExplosion();
        }
        else
        {
            currentTimeToExplosion += Time.deltaTime;

        }
    }

    void makeRangeExplosion()
    {

        currentSpeedExplosion = Mathf.Clamp01(currentSpeedExplosion + (0.2f * Time.deltaTime));
        float t = currentSpeedExplosion / speedExplosion;
        radioActual = Mathf.Lerp(radioActual,maxExplosion, t);

        if (t >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioActual);
    }

}
