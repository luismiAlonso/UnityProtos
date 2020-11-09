using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speedMove;
    public float speedRotation;
    [SerializeField] private float fallMultiplier;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;
    //[SerializeField] private bool activeGravity;

    private CharacterController charController;
    private JumpControl jumpControl;
    private DashControl dashControl;

    PlayerControl playerControl;
    private float angle;
    private float limitSlopeOld;
    private Vector3 input;
    private float speedAux;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        charController = GetComponent<CharacterController>();
        limitSlopeOld = charController.slopeLimit;
        speedAux = speedMove;
    }

    private void Update()
    {
       
    }

    public void PlayerMovement()
    {
        // Vector3 forwardMovement = transform.forward * vertInput;
        // Vector3 rightMovement = transform.right * horizInput;
        if (!playerControl.dashControl.isDashing() && !playerControl.jumpControl.isThrowing() && playerControl.checkers.canMove)
        {
            // charController.SimpleMove(new Vector3(input.x, jumpControl.getYJump().y, input.z) * playerControl.setthing.speedMove);
            charController.Move(new Vector3(input.x, playerControl.jumpControl.getYJump().y, input.z) * speedAux * Time.deltaTime);

        } 

        if ((input.y != 0 || input.x != 0) && OnSlope())
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);     
    }

    public void setInput(Vector3 _input)
    {
        input = Vector3.ClampMagnitude(_input, 1f);
     
    }

     Vector3 getInput()
    {
        return input;
    }

   public void calculateDir()
    {
        angle = Mathf.Atan2(input.x, input.z);
        angle = Mathf.Rad2Deg * angle;
        angle += Camera.main.transform.eulerAngles.y;
    }

   public void rotate()
    {
        if (!input.Equals(Vector3.zero) && playerControl.checkers.canRotate) {
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
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

    private bool OnSlope()
    {
        if (playerControl.jumpControl.isJumping())
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    public float getSpeedMove()
    {
        return speedMove;
    }

    public void setSpeedMove(float speed)
    {
        speedAux=speed;
    }

    public void aplyGravity()
    {
        //only use if not jumping
       /* if ( !charController.isGrounded && activeGravity ) {
             charController.Move(-Vector3.up * fallMultiplier * Time.deltaTime);
        }*/
    }
}
