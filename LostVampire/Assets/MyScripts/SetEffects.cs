using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffects : MonoBehaviour
{
    public enum TypeCharacter { player = 0, cleroE = 1, baseE=2, itemMana=3 };
    public TypeCharacter typeCharacter;
    public Transform soundsScene;
    private List<Transform> listVFX = new List<Transform>();
    private List<Transform> listSFX = new List<Transform>();

    private void Start()
    {
        soundsScene = GameObject.Find("soundManager").transform;
        setList();
    }

    void setList()
    {
        //load sounds scene
        Transform[] sfx = soundsScene.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform tsx in sfx)
        {
            if (tsx.tag == "sx")
            {
                listSFX.Add(tsx);
            }
        }

        if (typeCharacter== TypeCharacter.baseE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("MyPrefabs/vfxSfx/base/VFX/");
            foreach (Transform tvfx in vfx)
            {
                listVFX.Add(tvfx);
            }
           
        }
        else if (typeCharacter == TypeCharacter.cleroE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("MyPrefabs/vfxSfx/clero/VFX/");
            foreach (Transform tvfx in vfx)
            {
                listVFX.Add(tvfx);
            }
            
        }
        else if (typeCharacter == TypeCharacter.player)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("MyPrefabs/vfxSfx/player/VFX/");

            foreach(Transform tvfx in vfx)
            {
                listVFX.Add(tvfx);
            }
            
        }

        if (typeCharacter == TypeCharacter.baseE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("MyPrefabs/vfxSfx/itemMAna/VFX/");
            foreach (Transform tvfx in vfx)
            {
                listVFX.Add(tvfx);
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
