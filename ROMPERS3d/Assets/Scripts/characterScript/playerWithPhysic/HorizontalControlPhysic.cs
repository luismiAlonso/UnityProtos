using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalControlPhysic : MonoBehaviour
{

    public float speedWalk;
    public float speedRotation;
    public float limitAngleSlope;
    public PhysicMaterial fullFriccion;
    public PhysicMaterial noFriccion;
    [HideInInspector]
    public PlayerControl playerControl;

    private Vector3 input;
    private Vector3 slopPerp;
    private Vector3 newMove;
    private CapsuleCollider capCollider;

    private float angle;
    private float slopeAngle;
    private float slopeAngleOld;
    private bool onSlope;
    private bool freno;

    RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        capCollider = GetComponent<CapsuleCollider>();
    }


    public void WalkToTargetDir()
    {

        // CancelHorizontalVelocity();
        if (playerControl.Grounded && !onSlope && !playerControl.JumpTriggered)
        {

            newMove.Set(speedWalk * input.x, playerControl.getRigidBody().velocity.y, 0);
           // newMove = Vector3.ClampMagnitude(newMove, speedWalk);
            playerControl.getRigidBody().velocity = newMove;
        }

        if (onSlope && !playerControl.JumpTriggered)
        {
            playerControl.getRigidBody().velocity = Vector3.zero;
            //Debug.Log("onSlope");

            if (slopPerp.x<0) {

                newMove.Set(speedWalk * slopPerp.x * -input.x, speedWalk * slopPerp.y * -input.x, 0);
                //Debug.Log("iz");
            }
            else
            {
                newMove.Set(speedWalk * slopPerp.x * +input.x, speedWalk * slopPerp.y * +input.x, 0);
                //Debug.Log("dr");
            }

            newMove = Vector3.ClampMagnitude(newMove, speedWalk);
            playerControl.getRigidBody().velocity = newMove;
        }

        if (!playerControl.Grounded)
        {
            onSlope = false;
            newMove.Set(speedWalk * input.x, playerControl.getRigidBody().velocity.y, 0);
            playerControl.getRigidBody().velocity = newMove ;
        }
       
    }

    public void setInput()
    {
        if (!playerControl.control2d)
        {

            input = InputControl.instance.getAxisControl();
            input = new Vector3(input.x, playerControl.getRigidBody().velocity.y, input.y);
            input = Vector3.ClampMagnitude(input, 1f);
        }
        else
        {
            input = InputControl.instance.getAxisControl();
            input = new Vector3(input.x,0, 0);
        }
    }

    public Vector3 getInput()
    {
        return input;
    }

    public Vector3 getAxis()
    {
        return new Vector3(InputControl.instance.getAxisControl().x, 0, InputControl.instance.getAxisControl().y);
    }

    public void checkFrontSlope()
    {

        RaycastHit checkFloor;
        //RaycastHit checkBack;
        //A side

        if (Physics.Raycast(playerControl.pointFrontCheck.position, -transform.up, out checkFloor, 0.5f, playerControl.DefaultLayerMask))
        {
            //Debug.DrawLine(playerControl.pointFrontCheck.position,transform.position+Vector3.down, Color.red, 0.5f);

            if (!playerControl.Grounded && !freno && !playerControl.JumpTriggered && !playerControl.canFly)
            {
                if (checkFloor.transform.GetComponent<IWall>() != null && checkFloor.transform.GetComponent<IWall>().getTypeWall()!="goma") {

                    newMove.Set(speedWalk * input.x, -5, 0);
                    playerControl.getRigidBody().velocity = newMove;
                    freno = true;

                }else if (checkFloor.transform.GetComponent<IWall>() == null)
                {
                    newMove.Set(speedWalk * input.x, -5, 0);
                    playerControl.getRigidBody().velocity = newMove;
                    freno = true;
                }

            }
            else if(playerControl.Grounded && freno && !playerControl.JumpTriggered && !playerControl.canFly)
            {
                if (checkFloor.transform.GetComponent<IWall>() != null && checkFloor.transform.GetComponent<IWall>().getTypeWall() != "goma") {

                    Vector3 temp = Vector3.Cross(checkFloor.normal, Vector3.down);
                    slopPerp = Vector3.Cross(temp, checkFloor.normal).normalized;
                    slopeAngle = Vector3.Angle(checkFloor.normal, Vector3.up);

                    if (slopeAngle > 0 && (slopeAngle < limitAngleSlope))
                    {
                        onSlope = true;
                    }
                    else
                    {
                        onSlope = false;

                    }

                    freno = false;
                }
                else if(checkFloor.transform.GetComponent<IWall>() == null)
                {

                    Vector3 temp = Vector3.Cross(checkFloor.normal, Vector3.down);
                    slopPerp = Vector3.Cross(temp, checkFloor.normal).normalized;
                    slopeAngle = Vector3.Angle(checkFloor.normal, Vector3.up);

                   // Debug.Log(slopeAngle);

                    if (slopeAngle > 0 && (slopeAngle < limitAngleSlope))
                    {
                        onSlope = true;
                    }
                    else
                    {
                        onSlope = false;

                    }

                    freno = false;
                }
            }
            else
            {
                freno = false;
            }

        }


        if (onSlope && input.magnitude < 0.001f)
        {
           // Debug.Log("friccion");
            capCollider.sharedMaterial = fullFriccion;
        }
        else
        {
           // Debug.Log("no friccion");
            capCollider.sharedMaterial = noFriccion;
        }
       

    }

    public bool isOnSlope()
    {
        return freno;
    }

    public void faceJump()
    {
        if (playerControl.controlInteract.isBlockPath() && !playerControl.Grounded)
        {
            newMove.Set(0, playerControl.getRigidBody().velocity.y, 0);
            playerControl.getRigidBody().velocity = newMove;
            playerControl.canMove = false;
           // Debug.Log("colision no suelo");
        }
        else if (playerControl.controlInteract.isBlockPath() && playerControl.Grounded)
        {
            playerControl.canMove = true;
           // Debug.Log("colision si suelo");
        }
        else if (!playerControl.controlInteract.isBlockPath() && playerControl.Grounded)
        {
            playerControl.canMove = true;
           // Debug.Log("No bloqueo colision si suelo");

        }else if (!playerControl.controlInteract.isBlockPath() && !playerControl.Grounded)
        {
            playerControl.canMove = true;

        }
    }

    void calculateDir()
    {
        if (!playerControl.control2d)
        {
            angle = Mathf.Atan2(input.x, input.z);
            angle = Mathf.Rad2Deg * angle;
            angle += Camera.main.transform.eulerAngles.y;
        }
    }

    public void rotate()
    {
        if (!playerControl.control2d)
        {
            if (!getAxis().Equals(Vector3.zero)  /*&& playerControl.checkers.canRotate*/)
            {
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
            }

        }
        else
        {

            if (Mathf.Abs(input.x) != 0)
            {
                if (input.x > 0f)
                {
                    angle = 90;

                }
                else if (input.x < 0f)
                {
                    angle = 270;

                }

                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }

        /*
         * 
         * If use RigidBody 
         * 
        if (!rb.velocity.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        */
    }

   
    private void OnCollisionStay(Collision col)
    {

        if (col.transform.tag== "movilWall" || col.transform.tag=="ground") {

            foreach (ContactPoint p in col.contacts)
            {
                Vector3 bottom = capCollider.bounds.center - (Vector3.up * capCollider.bounds.extents.y);
                Vector3 curve = bottom + (Vector3.up * capCollider.radius);

                Debug.DrawLine(curve, p.point, Color.blue, 0.5f);
                Vector3 dir = curve - p.point;

                if (dir.y > 0f)
                {
                    // slopPerp = Vector3.ProjectOnPlane(curve, p.normal).normalized ;
                    //slopeAngle = Vector3.Angle(p.normal, Vector3.up);
                    Vector3 temp = Vector3.Cross(p.normal, Vector3.down);
                    slopPerp = Vector3.Cross(temp, p.normal).normalized;
                    slopeAngle = Vector3.Angle(p.normal, Vector3.up);

                    if (slopeAngle > 0 && (slopeAngle < limitAngleSlope))
                    {
                        onSlope = true;
                    }
                    else
                    {
                        onSlope = false;

                    }
                }
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "movilWall" || collision.transform.tag == "ground")
        {
           // onSlope = false;
        }
    }

}
