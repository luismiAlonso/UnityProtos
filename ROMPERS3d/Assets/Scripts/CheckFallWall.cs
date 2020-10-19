using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFallWall : MonoBehaviour
{
    public enum DirWall { front, back }
    public DirWall dirwall;
    private bool collisionWallSolid;
    private bool palyerCheckWall;

    public bool isCollisionWall()
    {
        return collisionWallSolid;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<wallNode>()!=null && other.transform.GetComponent<wallNode>().solidWall)
        {
           // Debug.Log("collision con solido");
            collisionWallSolid = true;
        }
        if (other.transform.tag=="Player")
        {
            palyerCheckWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            palyerCheckWall = false;

        }
    }

    public bool getPlayerCheck()
    {
        return palyerCheckWall;
    }
}
