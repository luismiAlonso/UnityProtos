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
    public GameObject[] listRendersCullers;
    public LOS.LOSSource[] artificialSun;
    public Renderer meshCullerPlayer;
    public LayerMask layerMask;

    private void Update()
    {
        cullerSunAndShadows();
    }

    void cullerSunAndShadows()
    {
        bool flag = false;

        for (int i=0;i< artificialSun.Length && !flag; i++)
        {
           if(LOS.LOSHelper.CheckBoundsVisibility(artificialSun[i], meshCullerPlayer.bounds, layerMask))
            {
                if (Manager.instance.playerControl.setEffects.GetFX("fxSunDamage") != null)
                {
                    Manager.instance.playerControl.setEffects.GetFX("fxSunDamage").Play();
                }
                Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().settingDamageLifeBySun();
                Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().isInShadow = false;
                flag=true;
            }
            else
            {
            
            }
        }

        if (!flag) {

            if (Manager.instance.playerControl.setEffects.GetFX("fxRestaureMana") != null && !Manager.instance.playerControl.setEffects.GetFX("fxRestaureMana").isPlaying)
            {
                Manager.instance.playerControl.setEffects.GetFX("fxRestaureMana").Play();
            }
            if (Manager.instance.playerControl.setEffects.GetFX("fxSunDamage") != null && Manager.instance.playerControl.setEffects.GetFX("fxSunDamage").isPlaying)
            {
                Manager.instance.playerControl.setEffects.GetFX("fxSunDamage").Stop();
            }
            Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().settingManaGlobal();
            Manager.instance.playerControl.gameObject.GetComponent<ControlInteract>().isInShadow = true;
        }
    }
}
