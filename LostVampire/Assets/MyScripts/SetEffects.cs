using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffects : MonoBehaviour
{
    public Transform VFX;
    public Transform SFX;

    private List<Transform> listVFX = new List<Transform>();
    private List<Transform> listSFX = new List<Transform>();

    private void Start()
    {
        setList();
    }

    void setList()
    {
        Transform[] prelistVFX = VFX.GetComponentsInChildren<Transform>(true);
        foreach (Transform tfx in prelistVFX)
        {
            if (tfx.tag=="fx") {
                listVFX.Add(tfx);
            }
        }

        Transform[] prelistSFX = SFX.GetComponentsInChildren<Transform>(true);
        foreach (Transform tfx in prelistSFX)
        {
            if (tfx.tag == "sx") {
                listSFX.Add(tfx);
            }
        }
    }

    public ParticleSystem GetFX(string nameFX)
    {
        ParticleSystem pS = null;

        foreach (Transform tfx in listVFX)
        {
            if (nameFX== tfx.name)
            {
                if (tfx.GetComponent<ParticleSystem>()!=null) {
                    pS = tfx.GetComponent<ParticleSystem>();
                }
            }
        }
        return pS;
    }

    public AudioSource GetSX(string nameSX)
    {
        AudioSource aS = null;

        foreach (Transform tsx in listSFX)
        {
            if (nameSX == tsx.name)
            {
                if (tsx.GetComponent<AudioSource>() != null)
                {
                    aS = tsx.GetComponent<AudioSource>();
                }
            }
        }
        return aS;
    }
}
