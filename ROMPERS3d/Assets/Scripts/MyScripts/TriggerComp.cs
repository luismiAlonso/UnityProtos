using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComp : MonoBehaviour
{
    public enum TypeTriggerComp {activaPlayer, bloqueadorWall , other };
    public TypeTriggerComp typeTriggerComp;
    public bool active;
    
    private void OnTriggerStay(Collider other)
    {
        if (typeTriggerComp==TypeTriggerComp.activaPlayer && other.transform.tag=="Player")
        {
            active = true;

        }else if (typeTriggerComp == TypeTriggerComp.bloqueadorWall && other.transform.tag == "movilWall")
        {
            active = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (typeTriggerComp == TypeTriggerComp.activaPlayer && other.transform.tag == "Player")
        {
            active = false;

        }
        else if (typeTriggerComp == TypeTriggerComp.bloqueadorWall && other.transform.tag == "movilWall")
        {
            active = false;

        }
    }
}
