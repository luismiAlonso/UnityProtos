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

                if (tvfx.GetComponent<FxControl>() != null && tvfx.GetComponent<FxControl>().isLocalObject)
                {

                    Transform objFx = Instantiate(tvfx, transform.position, Quaternion.identity);
                    objFx.transform.parent = transform;
                    objFx.name = tvfx.name;
                    objFx.gameObject.SetActive(false);
                    listVFX.Add(objFx);

                }
                else if (tvfx.GetComponent<FxControl>() != null && !tvfx.GetComponent<FxControl>().isLocalObject)
                {

                    Transform objFx = Instantiate(tvfx, transform.position, Quaternion.identity);
                    objFx.name = tvfx.name;
                    objFx.gameObject.SetActive(false);
                    listVFX.Add(objFx);
                }
            }
           
        }
        else if (typeCharacter == TypeCharacter.cleroE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("MyPrefabs/vfxSfx/clero/VFX/");
            foreach (Transform tvfx in vfx)
            {
                if (tvfx.GetComponent<FxControl>() != null && tvfx.GetComponent<FxControl>().isLocalObject)
                {

                    Transform objFx = Instantiate(tvfx, transform.position, Quaternion.identity);
                    objFx.transform.parent = transform;
                    objFx.name = tvfx.name;
                    objFx.gameObject.SetActive(false);
                    listVFX.Add(objFx);

                }
                else if (tvfx.GetComponent<FxControl>() != null && !tvfx.GetComponent<FxControl>().isLocalObject)
                {

                    Transform objFx = Instantiate(tvfx, transform.position, Quaternion.identity);
                    objFx.name = tvfx.name;
                    objFx.gameObject.SetActive(false);
                    listVFX.Add(objFx);
                }
            }
            
        }
        else if (typeCharacter == TypeCharacter.player)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("MyPrefabs/vfxSfx/player/VFX/");

            foreach(Transform tvfx in vfx)
            {
                if (tvfx.GetComponent<FxControl>()!=null && tvfx.GetComponent<FxControl>().isLocalObject) {

                    Transform objFx = Instantiate(tvfx, transform.position, Quaternion.identity);
                    objFx.transform.parent = transform;
                    objFx.name = tvfx.name;
                    objFx.gameObject.SetActive(false);
                    listVFX.Add(objFx);

                }
                else if (tvfx.GetComponent<FxControl>() != null && !tvfx.GetComponent<FxControl>().isLocalObject)
                {

                    Transform objFx = Instantiate(tvfx, transform.position, Quaternion.identity);
                    objFx.name = tvfx.name;
                    objFx.gameObject.SetActive(false);
                    listVFX.Add(objFx);
                }
            }
            
  
        }
    }

    public ParticleSystem GetFX(string nameFX)
    {
        ParticleSystem pFx = null;

        foreach (Transform tfx in listVFX)
        {
            
            if (nameFX== tfx.name)
            {
                if (tfx.GetComponent<ParticleSystem>()!=null) {

                    pFx = tfx.GetComponent<ParticleSystem>();
                    return pFx;
                }
            }
        }
       
        return pFx;
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

    public void PlayFx(string nameFX)
    {
        foreach (Transform tfx in listVFX)
        {

            if (nameFX == tfx.name && tfx.GetComponent<FxControl>().isLocalObject)
            {            
                tfx.gameObject.SetActive(true);
                tfx.GetComponent<ParticleSystem>().Play();

            }else if (nameFX == tfx.name && !tfx.GetComponent<FxControl>().isLocalObject)
            {
                tfx.gameObject.SetActive(true);
                tfx.transform.position = transform.position;
                tfx.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    public void noneFx(string nameFX)
    {
        foreach (Transform tfx in listVFX)
        {
           // Debug.Log(tfx.name + "/" + nameFX);

            if (nameFX == tfx.name && tfx.GetComponent<FxControl>().isLocalObject)
            {
                tfx.gameObject.SetActive(false);

            }
            else if (nameFX == tfx.name && !tfx.GetComponent<FxControl>().isLocalObject)
            {
                tfx.gameObject.SetActive(true);
               
            }
        }
    }
}
