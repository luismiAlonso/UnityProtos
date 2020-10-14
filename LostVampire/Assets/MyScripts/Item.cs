using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetEffects))]
public class Item : MonoBehaviour
{
    public enum TypeItem { mana = 0, life = 1,power=2 };
    public TypeItem typeNPC;
    public float powerRange;
    public float timeToDestroy;
    SetEffects setEffects;

    private void Awake()
    {
        setEffects = GetComponent<SetEffects>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (typeNPC==TypeItem.mana) {

                Manager.instance.playerControl.controlInteract.settingMana(Manager.instance.playerControl.controlInteract.getMana() + powerRange);

                if (setEffects.GetFX("fxAddMana")!=null) {

                    setEffects.GetFX("fxAddMana").Play();
                }

                if (setEffects.GetSX("sxAddMana") != null)
                {
                    Debug.Log("Otros");

                    setEffects.GetSX("sxAddMana").Play();
                }

                Destroy(gameObject, timeToDestroy);
            }
            else
            {
                Debug.Log("Otros");
            }
        }
    }
}
