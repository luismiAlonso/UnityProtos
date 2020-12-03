using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMenu : MonoBehaviour
{
    private SetEffects setEffects;
    // Start is called before the first frame update
    void Start()
    {
        setEffects=GetComponent<SetEffects>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            setEffects.PlayFx("fxPilon");
            Manager.instance.onTriggerMenu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            setEffects.PlayFx("fxPilon");
           
        }
    }

    /* private void OnTriggerStay(Collider other)
     {
         if (other.transform.tag == "Player")
         {
             other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
             other.transform.GetComponent<PlayerControl>().getAnim().SetBool("isMoving",false);
             Manager.instance.onTriggerMenu = true;
         }
     }*/


}
