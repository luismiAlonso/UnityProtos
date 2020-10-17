using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    void Start()
    {
        generateSoulsPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateSoulsPanel()
    {
        for (int i=0; i<ManagerRenderCuller.instance.listRendersCullers.Length;i++)
        {
            if (ManagerRenderCuller.instance.listRendersCullers[i].tag!="Player") {

                soulsImg imgSouls = Instantiate(Resources.Load<soulsImg>("MyPrefabs/components/soulsImage")) as soulsImg;
                imgSouls.transform.parent = transform;
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
}
