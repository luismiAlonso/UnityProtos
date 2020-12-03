using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngravityFall : MonoBehaviour
{
    public bool isInGravity;
    public float speedH;
    public float forceUP;
    public float fallMultiplier;
    public float timeCanFly;

    private float auxTimeFly;
    private PlayerControl playerControl;
    private Vector3 newMove;
    private GameObject topWall;
    private bool OnPushUP;

    // Start is called before the first frame update
    void Start()
    {
        auxTimeFly = timeCanFly;
        playerControl = Manager.instance.playerControl;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Upforce();
    }

    
    public void Upforce()
    {
        if (InputControl.instance.getButtonsControl("Button0") && playerControl.canFly)
        {
            Debug.Log("push");
            playerControl.getRigidBody().AddForce(Vector3.up * forceUP,ForceMode.Acceleration);
        }
    }


    public void timeFly()
    {
        if (isInGravity) {

            if (auxTimeFly > 0)
            {
                auxTimeFly -= Time.fixedDeltaTime;
            }
            else
            {
                auxTimeFly = timeCanFly;
                setIngravity(false, topWall);
               // Debug.Log("finished");
            }
        }
    }

    public void pushUp()
    {
        if (playerControl.canFly) {

            playerControl.getRigidBody().velocity = Vector3.up * forceUP * Time.deltaTime;
            OnPushUP = true;
        }
        
    }

    public bool isOnpush()
    {
        return OnPushUP;
    }

    public void setOnPush(bool b)
    {
        OnPushUP = b;
    }

    public void setIngravity(bool b,GameObject wall)
    {
        isInGravity = b;

        if (isInGravity) {

            playerControl.activeGravity = false;
            playerControl.getRigidBody().useGravity = false;
            playerControl.canJump = false;
            playerControl.canFly = true;
            topWall = wall;
        }
        else
        {
            //playerControl.getRigidBody().velocity = Vector3.zero;
            playerControl.getRigidBody().useGravity = true;
            playerControl.activeGravity = true;
            playerControl.canJump = true;
            playerControl.canFly = false;
            Destroy(topWall);

        }
    }

    

}
