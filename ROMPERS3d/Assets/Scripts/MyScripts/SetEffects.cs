using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffects : MonoBehaviour
{
    public enum TypeFX { player = 0, baseE = 1, romperE = 2, bombE = 3, rodeE = 4, wall = 5, pilon=6, itemMoney = 7, none=8};
    public TypeFX typeFx;
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

        if (typeFx == TypeFX.baseE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/base/VFX/");

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
        else if (typeFx == TypeFX.romperE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/romper/VFX/");
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
        else if (typeFx == TypeFX.player)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/player/VFX/");

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
        else if (typeFx == TypeFX.bombE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/bomb/VFX/");

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
        else if (typeFx == TypeFX.rodeE)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/rode/VFX/");

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
        else if (typeFx == TypeFX.wall)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/wall/VFX/");

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
        else if (typeFx == TypeFX.pilon)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/pilon/VFX/");

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
        else if (typeFx == TypeFX.itemMoney)
        {
            Transform[] vfx = Resources.LoadAll<Transform>("Prefabs/vfxSfx/itemMoney/VFX/");

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


    }

    public ParticleSystem GetFX(string nameFX)
    {
        ParticleSystem pFx = null;

        foreach (Transform tfx in listVFX)
        {

            if (nameFX == tfx.name)
            {
                if (tfx.GetComponent<ParticleSystem>() != null)
                {

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

    public void PlaySx(string nameSX)
    {
        foreach (Transform tsx in listSFX)
        {

            if (nameSX == tsx.name)
            {
                if (nameSX == tsx.name && !tsx.GetComponent<AudioSource>().isPlaying)
                {
                    tsx.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    public void noneSx(string nameSX)
    {
        foreach (Transform tsx in listSFX)
        {

            if (nameSX == tsx.name)
            {
                if (nameSX == tsx.name && tsx.GetComponent<AudioSource>().isPlaying)
                {
                    tsx.GetComponent<AudioSource>().Stop();
                }
            }
        }
    }

    public void PlayFx(string nameFX)
    {
       // Debug.Log(listVFX.Count);

        foreach (Transform tfx in listVFX)
        {
            if (nameFX == tfx.name && !tfx.GetComponent<FxControl>().useInLoop && !tfx.GetComponent<FxControl>().isLocalObject)
            {
                tfx.gameObject.SetActive(true);
                tfx.GetComponent<ParticleSystem>().Play();

            }
            else if (nameFX == tfx.name && !tfx.GetComponent<FxControl>().useInLoop && tfx.GetComponent<FxControl>().isLocalObject)
            {
                tfx.gameObject.SetActive(true);
                tfx.transform.position = transform.position;
                tfx.GetComponent<ParticleSystem>().Play();
            }
            else if (nameFX == tfx.name && tfx.GetComponent<FxControl>().useInLoop && tfx.GetComponent<FxControl>().isLocalObject)
            {
                if (!tfx.GetComponent<FxControl>().isFxActive())
                {
                    tfx.gameObject.SetActive(true);
                    tfx.transform.position = transform.position;
                    tfx.GetComponent<ParticleSystem>().Play();
                }

            }
            else if (nameFX == tfx.name && tfx.GetComponent<FxControl>().useInLoop && !tfx.GetComponent<FxControl>().isLocalObject)
            {
                if (!tfx.GetComponent<FxControl>().isFxActive())
                {
                    tfx.gameObject.SetActive(true);
                    tfx.GetComponent<ParticleSystem>().Play();
                }
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
                tfx.gameObject.SetActive(false);

            }
        }
    }
}
