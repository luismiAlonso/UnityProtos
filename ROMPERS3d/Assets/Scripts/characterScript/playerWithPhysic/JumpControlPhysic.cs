using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControlPhysic : MonoBehaviour
{
    public float jumpForce;
    public float jumpRemoteForce;
    [HideInInspector]
    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();

    }

    // Update is called once per frame
    void Update()
    {
        //resetJump();
    }

    public void Jump()
    {

        if (InputControl.instance.getButtonsControl("Button0") && playerControl.Grounded)
        {
            playerControl.setEffects.PlaySx("SxJump");
            playerControl.getRigidBody().velocity = new Vector3(playerControl.horizontalControlPhysic.getInput().x, 1 * jumpForce, 0); //Vector3.up * jumpForce;
            playerControl.JumpTriggered = true;
        }
    }

    public void remoteJump()
    {
        playerControl.setEffects.PlaySx("SxJump");
        playerControl.transform.GetComponent<Rigidbody>().velocity=  new Vector3(playerControl.horizontalControlPhysic.getInput().x, 1 * jumpRemoteForce, 0);

    }


}
