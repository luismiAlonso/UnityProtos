using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Checkers {

    public bool canMove;
    public bool canJump;
    public bool canRotate;
    public bool isGrounded;
    public Transform localPointGrounded;
    public float radiusGrounded;
    public LayerMask layerMaskGround;

    

    public void checkGround()
    {
        Collider[] cols = Physics.OverlapSphere(localPointGrounded.position,radiusGrounded, layerMaskGround);
        if (cols.Length>0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

}
