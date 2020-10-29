using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerRenderCuller : MonoBehaviour
{
    #region Singleton
    private static ManagerRenderCuller managerRender;
    public static ManagerRenderCuller instance
    {
        get
        {
            if (managerRender == null)
            {
                managerRender = FindObjectOfType<ManagerRenderCuller>();
            }
            return managerRender;
        }
    }

    #endregion Singleton
    public List<GameObject> listRendersCullers=new List<GameObject>();
    public LOS.LOSSource[] artificialSun;
    public Renderer meshCullerPlayer;
    public LayerMask layerMask;

    private void Start()
    {
        setRendersCuller();
    }

    private void Update()
    {
        cullerSunAndShadows();
    }


    void setRendersCuller()
    {
        listRendersCullers = new List<GameObject>();

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform[] ply = player.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in ply)
        {
            if (t.name == "meshCuller")
            {
                listRendersCullers.Add(t.gameObject);
            }
        }

        foreach (GameObject npc in npcs)
        {
            if (npc.GetComponent<LOS.LOSCuller>() != null)
            {
                listRendersCullers.Add(npc);
            }
        }

        ControlSouls.instance.generateSoulsPanel();

    }

    void cullerSunAndShadows()
    {
        bool flag = false;

        for (int i=0;i< artificialSun.Length && !flag; i++)
        {

           if(LOS.LOSHelper.CheckBoundsVisibility(artificialSun[i], meshCullerPlayer.bounds, layerMask))
            {
                if (Manager.instance.playerControl!=null && Manager.instance.playerControl.setEffects.GetFX("fxSunDamage") != null)
                {
                    Manager.instance.playerControl.setEffects.PlayFx("fxSunDamage");
                }
                if (Manager.instance.playerControl != null && Manager.instance.playerControl.setEffects.GetFX("fxRestaureMana") != null)
                {
                    Manager.instance.playerControl.setEffects.noneFx("fxRestaureMana");
                }

                if (Manager.instance.playerControl != null && !Manager.instance.playerControl.checkers.isCaptured) {
                    Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().settingDamageLifeBySun();
                    Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().isInShadow = false;
                }

                //Debug.Log("Te veo");
                flag =true;
            }
            
        }

        if (!flag) {

            if (Manager.instance.playerControl != null && Manager.instance.playerControl.setEffects.GetFX("fxRestaureMana") != null )
            {
                Manager.instance.playerControl.setEffects.PlayFx("fxRestaureMana");
            }
            if (Manager.instance.playerControl != null && Manager.instance.playerControl.setEffects.GetFX("fxSunDamage") != null )
            {
                Manager.instance.playerControl.setEffects.noneFx("fxSunDamage");
            }
            //Debug.Log("No Te veo");
            Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().settingManaGlobal();
            Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().isInShadow = true;

        }
    }
}
