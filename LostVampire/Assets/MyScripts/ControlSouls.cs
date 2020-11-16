using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlSouls : MonoBehaviour
{
    #region Singleton
    private static ControlSouls controlSouls;
    public static ControlSouls instance
    {
        get
        {
            if (controlSouls == null)
            {
                controlSouls = FindObjectOfType<ControlSouls>();
            }
            return controlSouls;
        }
    }

    #endregion Singleton
    private List<soulsImg> imgSoulsList = new List<soulsImg>();
    private int countSouls;
    public GameObject doorExit;
    private bool doorExist;

    private void Awake()
    {

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateSoulsPanel()
    {
        resetSouls();
        imgSoulsList = new List<soulsImg>();
        //Debug.Log(ManagerRenderCuller.instance.listRendersCullers.Count);

        for (int i=0; i<ManagerRenderCuller.instance.listRendersCullers.Count;i++)
        {
            if (ManagerRenderCuller.instance.listRendersCullers[i].tag!="Player") {
                soulsImg imgSouls = Instantiate(Resources.Load<soulsImg>("MyPrefabs/components/ImageSouls")) as soulsImg;
                imgSouls.transform.parent = transform;
                imgSouls.transform.localScale = Vector3.one;
                imgSoulsList.Add(imgSouls);
            }
        }
    }

    public void activeImgSouls()
    {
        if (!doorExist) {

            bool flag = false;

            for (int i = 0; i < imgSoulsList.Count && !flag; i++)
            {
               
                if(!imgSoulsList[i].isActived())
                {
                    imgSoulsList[i].setActive(true);
                    flag = true;
                    countSouls++;
                }
            }
            //Debug.Log(imgSoulsList.Count+"//"+ countSouls);
            //todas las posesiones de almas completadas creamos puerta
            if (imgSoulsList.Count == countSouls)
            {
                Manager.instance.indexLevel++;
                GameObject coffin = Instantiate(doorExit);
                if (Manager.instance.playerControl.setEffects.GetSX("sxCoffinSpawn") !=null)
                {
                    Manager.instance.playerControl.setEffects.GetSX("sxCoffinSpawn").Play();
                }
                Vector3 randPos = RandomNavmeshLocation(6.5f);
                coffin.transform.position = new Vector3(randPos.x, 2, randPos.z);
                doorExist = true;
            }
        }
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += Manager.instance.playerControl.transform.position;
        NavMeshHit hit;
        randomDirection = new Vector3(randomDirection.x,0, randomDirection.z);
         Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void resetSouls()
    {
        for (int i = 0; i < imgSoulsList.Count ; i++)
        {
            Debug.Log(imgSoulsList[i].name);
            Destroy(imgSoulsList[i]);         
        }
    }
}
