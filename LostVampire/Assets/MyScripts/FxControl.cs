using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxControl : MonoBehaviour
{

    public bool isLocalObject;
   // public bool useDurabilityParticle;

    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        /* if (particleSystem!=null && useDurabilityParticle) {

             Debug.Log(particleSystem.main.startLifetimeMultiplier);

             if (particleSystem.main.duration< 0.1f)
             {
                 gameObject.SetActive(false);
             }
         }*/

        if (particleSystem != null)
        {
            if (!particleSystem.IsAlive())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
