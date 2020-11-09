using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Checkers 
{
    public Transform parentObj;
    public float radiusCheckFront;
    public float maxDistanceFront;
    public float maxDistanceGrounded;
    public float radiusCollisionThrow;
    public LayerMask layerThrow;
    public LayerMask layerMaskGround;
    public LayerMask layerMaskWall;
    public float currentHitDistanceFront;
    [HideInInspector]
    public bool blockingPass;
    [HideInInspector]
    public bool collisionThrow;
    [HideInInspector]
    public bool isGrounded;
    public bool canMove=true;
    public bool canJump=true;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool invulnerability;
    [HideInInspector]
    public bool isDetectado;
    [HideInInspector]
    public bool canAtack = true;
    [HideInInspector]
    public bool remoteControl = false;
    public bool isStuned = false;
    [HideInInspector]
    public bool isDominated = false;
    [HideInInspector]
    public bool isCaptured = false;
    public bool canRotate = true;
    public bool canDash = true;

    RaycastHit hit;
    Vector3 pointBlocking;
    Vector3 pointMouse;

    public struct objDataColl
    {
        public bool checkCollThrow;
        public Transform objColl;
    }

   /* public bool isGroundCheck()
    {
        isGrounded = Physics.Raycast(parentObj.position, Vector3.down, out hit, maxDistanceGrounded, layerMaskGround,QueryTriggerInteraction.UseGlobal);
        return isGrounded;
    }*/

    public bool isTraspasable()
    {
        blockingPass = Physics.SphereCast(parentObj.position, radiusCheckFront, parentObj.forward, out hit, maxDistanceFront, layerMaskWall, QueryTriggerInteraction.UseGlobal);

        if (blockingPass)
        {
            currentHitDistanceFront = hit.distance;
            pointBlocking = hit.point;
        }
        else
        {
            currentHitDistanceFront = maxDistanceFront;
        }
      
        return blockingPass;
    }

    public objDataColl checkCollisionONthrow()
    {
        objDataColl objColl = new objDataColl();
        objColl.checkCollThrow = false;
        Collider[] hitColliders = Physics.OverlapSphere(parentObj.position, radiusCollisionThrow, layerThrow);

        foreach (var hitCollider in hitColliders)
        {
           // Debug.Log(hitCollider.transform.name+" - "+ parentObj.name +" - "+hitCollider.transform.GetComponent<SimpleIA>());

            if (hitCollider.transform.name!=parentObj.name && hitCollider.transform.GetComponent<SimpleIA>()!=null && 
                hitCollider.transform.GetComponent<SimpleIA>().typeNPC!=SimpleIA.TypeNPC.tanque)
            {
                objColl.checkCollThrow = true;
                objColl.objColl = hitCollider.transform;
            }
        }

        return objColl;
    }

    public Vector3 getPointBloquing()
    {
        return pointBlocking;
    }

    public bool isMovingMouse(Vector3 actualMousePos)
    {
        if (actualMousePos.Equals(pointMouse))
        {
            pointMouse = actualMousePos;

            return true;
        }
        else
        {
            pointMouse = actualMousePos;

            return false;
        }

    }
   
}
