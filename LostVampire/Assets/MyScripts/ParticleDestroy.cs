using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public ParticleSystem particleEmissor;
    public AudioSource audioSource;
    public GameObject prefbParticle;
    public int numParticleCustom;

    public void CreateEmissor()
    {
        audioSource.Play();
        Instantiate(particleEmissor,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void creteCustomEmissor()
    {
        audioSource.Play();
        for (int i=0;i<numParticleCustom;i++)
        {
            GameObject part = Instantiate(prefbParticle, transform.position,Quaternion.identity);
            part.GetComponent<Rigidbody>().AddForceAtPosition(transform.position * 5, transform.position);
            part.transform.localScale = new Vector3(Random.Range(0.1f,0.3f), Random.Range(0.1f, 0.3f), Random.Range(0.1f, 0.3f));
            Destroy(part,Random.Range(0.1f,0.5f));
        }
        Destroy(gameObject);
    }
}
