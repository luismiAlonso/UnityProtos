using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpControlPhysic))]
[RequireComponent(typeof(HorizontalControlPhysic))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerControl : MonoBehaviour
{
    public Transform pointGroundCheck;
    public Transform pointFrontCheck;
    public float fallMultiplier;
    public bool activeGravity;
    public float radiusGroundCheck;
    public bool control2d;
    public LayerMask DefaultLayerMask;

    private Animator anim;
    private Rigidbody rg;
    [HideInInspector]
    public JumpControlPhysic jumpControlPhysic;
    [HideInInspector]
    public HorizontalControlPhysic horizontalControlPhysic;
    [HideInInspector]
    public ControlInteract controlInteract;
    [HideInInspector]
    public IngravityFall ingravityFall;

    [HideInInspector]
    public bool Grounded;
    [HideInInspector]
    public bool JumpTriggered;
    [HideInInspector]
    public bool canMove=true;
    public bool canJump = true;
    [HideInInspector]
    public bool canFly = false;
    [HideInInspector]
    public SetEffects setEffects;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody>();
        jumpControlPhysic = GetComponent<JumpControlPhysic>();
        horizontalControlPhysic = GetComponent<HorizontalControlPhysic>();
        controlInteract = GetComponent<ControlInteract>();
        ingravityFall = GetComponent<IngravityFall>();
        setEffects = GetComponent<SetEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.fullStop)
        {
            horizontalControlPhysic.setInput();

            if (canJump) {

                jumpControlPhysic.Jump();
            }

            if (ingravityFall.isInGravity)
            {
               // ingravityFall.Upforce();
            }

            horizontalControlPhysic.rotate();
            
        }
       // horizontalControlPhysic.PrepareWalk();  
    }

    void FixedUpdate()
    {
        if (!Manager.instance.fullStop)
        {
            checkGround();
            horizontalControlPhysic.checkFrontSlope();
            horizontalControlPhysic.faceJump();

            if (canMove)
            {
                horizontalControlPhysic.WalkToTargetDir();
            }

            animateControl();

            aplyGravity();
            ingravityFall.timeFly();
        }

    }

    void animateControl()
    {
        if (Grounded) {

            if (Mathf.Abs(horizontalControlPhysic.getAxis().x) > 0.01f)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);

            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (!Grounded)
        {
            anim.SetBool("isJumping", true);

        }
        else
        {
            anim.SetBool("isJumping", false);

        }
    }

    void aplyGravity()
    {
        if (!Grounded && activeGravity && !ingravityFall.isInGravity) {

            rg.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //Debug.Log("actua gravedad general");
        }
        
    }

    void checkGround()
    {
        Collider[] col = Physics.OverlapSphere(pointGroundCheck.position,radiusGroundCheck, DefaultLayerMask,QueryTriggerInteraction.Collide);
        
        if (col.Length>0)
        {
            Grounded = true;
           // Debug.Log(Grounded);
        }
        else
        {
            Grounded = false;
            //Debug.Log(Grounded);
        }

        if (rg.velocity.y < 0)
        {
            JumpTriggered = false;
        }
    }

    public Animator getAnim()
    {
        return anim;
    }

    public Rigidbody getRigidBody()
    {
        return rg;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointGroundCheck.position,radiusGroundCheck);
    }

}
