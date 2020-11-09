using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWalkable : MonoBehaviour
{
    public enum TypeSurface { lava = 0, sagrado = 1 };
    public TypeSurface typeSurface;
    public float damage;
    public float speedSlow;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag=="Player"  && other.transform.GetComponent<PlayerControl>() != null)
        {
            if (typeSurface== TypeSurface.lava )
            {
                other.transform.GetComponent<PlayerMove>().setSpeedMove(speedSlow);
                other.transform.GetComponent<PlayerControl>().checkers.canDash = true;
                other.transform.GetComponent<PlayerControl>().checkers.canJump = false;

            }else if (typeSurface == TypeSurface.sagrado)
            {

            }

        }else if (other.transform.tag == "NPC" && other.GetComponent<SimpleIA>()!=null && other.GetComponent<SimpleIA>().typeNPC==SimpleIA.TypeNPC.normal)
        {
            if (typeSurface == TypeSurface.lava)
            {
                other.transform.GetComponent<PlayerMove>().setSpeedMove(speedSlow);
                other.transform.GetComponent<PlayerControl>().checkers.canDash = true;
                other.transform.GetComponent<PlayerControl>().checkers.canJump = false;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player"  && other.transform.GetComponent<PlayerControl>() != null)
        {
            if (typeSurface == TypeSurface.lava)
            {
                other.transform.GetComponent<PlayerMove>().setSpeedMove(other.transform.GetComponent<PlayerMove>().speedMove);
                other.transform.GetComponent<PlayerControl>().checkers.canDash = true;
                other.transform.GetComponent<PlayerControl>().checkers.canJump = true;

            }else if (typeSurface == TypeSurface.sagrado)
            {

            }
        }
        else if (other.transform.tag == "NPC" && other.GetComponent<SimpleIA>() != null && other.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.normal)
        {
            other.transform.GetComponent<PlayerMove>().setSpeedMove(other.transform.GetComponent<PlayerMove>().speedMove);
            other.transform.GetComponent<PlayerControl>().checkers.canDash = true;
            other.transform.GetComponent<PlayerControl>().checkers.canJump = true;
        }
    }
}
