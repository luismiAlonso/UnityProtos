﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ControlInteract))]
public class PlayerControl : MonoBehaviour {

    public bool usePad;
    public bool faceControl;
    public SetthingAtributePlayer setthing;
    public Checkers checkers;

    [HideInInspector]
    public ControlInteract controlInteract;
    // public AnimatorControl animatorControl;
    SetEffects effects;
    Rigidbody rg;
    CameraControl cam;
    RaycastHit hit;
    Vector3 mousePos;
    Vector3 dirFacing;
    Vector3 inputs;

    void Awake() {
        cam = Camera.main.GetComponent<CameraControl>();    
    }

    void Start() {
        rg = GetComponent<Rigidbody>();
        effects = GetComponent<SetEffects>();
        controlInteract = GetComponent<ControlInteract>();
        checkers.isMovingMouse(mousePos = Util.getMousePointWorld(usePad));
    }

    void FixedUpdate()
    {
       

    }

    private void LateUpdate()
    {
        if (!Manager.instance.fullStop) {
           //mouseController();
        }
        if ( !Manager.instance.fullStop)
        {
            move3d();
        }
    }

    private void Update()
    {       
        if (!Manager.instance.fullStop) {
            checkers.isGroundCheck();
            checkers.isTraspasable();
            mouseController();
            fasterJump();
            //dash();
            dashPhysics();
        }

    }
 

    void mouseController()
    {
        if (faceControl) {

            mousePos = Util.getMousePointWorld(usePad); //Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!usePad)
            {

                // locationsElements[3].position = new Vector3(mousePos.x, 0, mousePos.z);
                // transform.rotation = Quaternion.LookRotation(Vector3.down, transform.position - transform.position);
                Vector2 direction = new Vector2(mousePos.x, mousePos.y);

                if (direction.magnitude != 0)
                {
                    //Debug.Log(Input.mousePosition);
                    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(mouseRay, out hit))
                    {
                        Vector3 aimDir = new Vector3(hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z);
                        /* if (hit.transform.tag != "Player")
                         {
                             dirMouseHit = hit.point;
                             transform.LookAt(aimDir);
                         }*/

                        dirFacing = hit.point;
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir), Time.deltaTime * setthing.speedRotationLookMouse);
                    }
                }
            }
            else
            {
                if (!Droid.instance.isAutoimp) {

                    /*  if (mousePos.sqrMagnitude > 0.0f)
                      {
                          previusAngle = Quaternion.LookRotation(Vector3.down, mousePos);
                          //player.transform.rotation = previusAngle;
                          transform.rotation = Quaternion.Lerp(transform.rotation, previusAngle, Time.deltaTime * 15);

                      }
                      else
                      {

                          if (!mousePos.Equals(Vector3.zero))
                          {
                              transform.rotation = previusAngle; //Quaternion.LookRotation(Vector3.down, mousePos);
                              transform.rotation = Quaternion.Lerp(transform.rotation, previusAngle, Time.deltaTime * 5);
                          }
                      }*/
                }
                else
                {
                    /* if (Droid.instance.getTarget()) {

                         Vector3 aimDir = new Vector3(Droid.instance.getTarget().position.x - transform.position.x, 2, Droid.instance.getTarget().position.z - transform.position.z);
                         aimDir.Normalize();

                         float angle = Mathf.Rad2Deg * Mathf.Atan2(aimDir.x, aimDir.z);
                         m_LookAngle = Mathf.SmoothDampAngle(m_LookAngle, angle, ref m_AngleVelocity, m_RotationSpeed);

                         transform.eulerAngles = new Vector3(90, m_LookAngle, 0);
                     }*/
                }

            }
        }

    }

    void move3d() {

        
        if (!checkers.remoteControl && checkers.canMove) {

            if (!usePad)
            {
                float verticalAxes = Input.GetAxis("Vertical");
                float horizontalAxes = Input.GetAxis("Horizontal");
                inputs = new Vector3(horizontalAxes, 0, verticalAxes);
                inputs = Vector3.ClampMagnitude(inputs, 1f);
                rg.velocity = new Vector3(inputs.x * setthing.speedMove, rg.velocity.y, inputs.z * setthing.speedMove);
                //If face control activated but not use, direction arrow rotate player
                rotateToDirectionNotFacingMouse(inputs);

            }
            else
            {
                float verticalAxes = Input.GetAxisRaw("PadAxisV");
                float horizontalAxes = Input.GetAxisRaw("PadAxisH");
                inputs = new Vector3(horizontalAxes, 0, verticalAxes);
                inputs = Vector3.ClampMagnitude(inputs, 1f);
                rg.velocity = new Vector3(inputs.x * setthing.speedMove, rg.velocity.y, inputs.z * setthing.speedMove);
                rotateToDirectionNotFacingMouse(inputs);
            }
        }


        if (checkers.isGrounded && checkers.canAtack)
        {
            checkers.canMove = true;
        }
    }

    void changeFacingControl()
    {
        if (faceControl)
        {
            faceControl = false;
            dirFacing = transform.forward;
        }
        else
        {
            faceControl = true;
        }
    }

    void rotateToDirectionNotFacingMouse(Vector3 _inputs)
    {

        if (!checkers.remoteControl && Input.GetKeyDown(KeyCode.LeftShift))
        {
            changeFacingControl();         
        }

        if (!faceControl && !_inputs.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_inputs), Time.deltaTime * 40f);
        }
    }


    //START INTERFACE INTERACT

    void simpleJump()
    {
        if (usePad) {

            if (!checkers.remoteControl && checkers.canJump && checkers.isGrounded && Input.GetButtonDown("buttonB"))
            {
                rg.velocity = new Vector3(0, setthing.forceJump, 0);

                if (effects.GetSX("sxJump") != null)
                {
                    effects.GetSX("sxJump").Play();
                }
            }
        }
        else
        {
            if (!checkers.remoteControl && checkers.canJump && checkers.isGrounded && Input.GetButtonDown("Fire1"))
            {
                rg.velocity = new Vector3(0, setthing.forceJump, 0);
                if (effects.GetSX("sxJump") != null)
                {
                    effects.GetSX("sxJump").Play();
                }
            }
        }

     
    }

    void fasterJump()
    {

        if (usePad) {

            if (!checkers.remoteControl && checkers.canJump && checkers.isGrounded && Input.GetButtonDown("buttonB"))
            {
                rg.velocity = new Vector3(0, setthing.forceJump, 0);

                if (effects.GetSX("sxJump")!=null) {

                    effects.GetSX("sxJump").Play();
                }
                if (effects.GetFX("fxJump") != null)
                {
                    effects.GetSX("fxJump").Play();
                }
            }
        }
        else {

            if (!checkers.remoteControl && checkers.canJump && checkers.isGrounded && Input.GetButtonDown("Fire1"))
            {
                rg.velocity = new Vector3(0, setthing.forceJump, 0);

                if (effects.GetSX("sxJump")!=null) {

                    effects.GetSX("sxJump").Play();
                }
            }
        }

        if (!checkers.isGrounded)
        {
            rg.velocity += Vector3.up * Physics.gravity.y * setthing.fallMultiplier  * Time.deltaTime;
        }
       
    }

    public void remoteFasterJump()
    {
        rg.velocity = new Vector3(rg.velocity.x, setthing.forceJump, rg.velocity.z);

        if (!checkers.isGrounded)
        {
            rg.velocity += Vector3.up * Physics.gravity.y * setthing.fallMultiplier * Time.deltaTime;

        }
    }

    void dash()
    {
        if (usePad) {

            if (!checkers.remoteControl && Input.GetButtonDown("buttonA"))
            {
                Vector3 normaLizeDir = Vector3.zero;
                Vector3 dir = Vector3.zero;

                if (effects.GetSX("sxDash")!=null) {
                    effects.GetSX("sxDash").Play();
                }

                if (!checkers.blockingPass) {

                    dir = dirFacing - transform.position;
                    normaLizeDir = dir.normalized * setthing.distanceDash;

                    if (usePad && !faceControl)
                    {
                        normaLizeDir = inputs * setthing.distanceDash;
                    }
                    normaLizeDir = normaLizeDir + transform.position;
                }
                else
                {
                    normaLizeDir = checkers.getPointBloquing();
                    Debug.Log(transform.position + "/" + dir);

                }

                normaLizeDir.y = 0.0f;
                StartCoroutine("dashIE", normaLizeDir);
            }
        }
        else
        {
            if (!checkers.remoteControl && Input.GetButtonDown("Fire2"))
            {
                Vector3 normaLizeDir = Vector3.zero;
                Vector3 dir = Vector3.zero;

                effects.GetSX("sxDash").Play();

                if (!checkers.blockingPass)
                {

                    dir = dirFacing - transform.position;
                    normaLizeDir = dir.normalized * setthing.distanceDash;

                    if (usePad && !faceControl)
                    {
                        normaLizeDir = inputs * setthing.distanceDash;
                    }
                    normaLizeDir = normaLizeDir + transform.position;
                }
                else
                {
                    normaLizeDir = checkers.getPointBloquing();
                    Debug.Log(transform.position + "/" + dir);

                }

                normaLizeDir.y = 0.0f;
                StartCoroutine("dashIE", normaLizeDir);
            }
        }
    }

    IEnumerator dashIE(Vector3 dir)
    {
        setthing.distanceToBlockingDash =Vector3.Distance(transform.position, dir);
        while (setthing.distanceToBlockingDash >= 0.5f)
        {
            setthing.distanceToBlockingDash =  Vector3.Distance(transform.position, dir);
            float step = setthing.forceDash * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,dir, step);
            yield return null;
        }
        rg.velocity = Vector3.zero;
    }


    void dashPhysics()
    {
        if (!checkers.remoteControl && (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("buttonA")))
        {
            effects.GetSX("sxDash").Play();
            Vector3 normaLizeDir = transform.forward * setthing.distanceDash;
            normaLizeDir.y = 0.0f;
            StartCoroutine("dashPhysiscIE", normaLizeDir);
        }
    }
   
    public void remoteDashPhysic()
    {
        Vector3 normaLizeDir = transform.forward * setthing.distanceDash;
        normaLizeDir.y = 0.0f;
        StartCoroutine("dashPhysiscIE", normaLizeDir);
    }

    IEnumerator dashPhysiscIE(Vector3 dir)
    {
        float time = setthing.TimeDurationDash;

        while (time >= 0)
        {       
            rg.MovePosition(transform.position + dir * Time.deltaTime * setthing.forceDash); 
            time -= Time.deltaTime;
            yield return null;
        }
        rg.velocity = Vector3.zero;

    }

    public void ArcJump()
    {
        Vector3 pointDash = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.L) && checkers.isGrounded)
        {
            Vector3 dir = dirFacing - transform.position;
            Vector3 normaLizeDir = dir.normalized * setthing.distanceJumpArc;
            normaLizeDir.y = 0;
            pointDash = calculateVelocity(transform.position + normaLizeDir, transform.position, setthing.TimeDurationArcJump);
            rg.velocity = pointDash;
        }
    }


    Vector3 calculateVelocity(Vector3 target,Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
        
    }

    public void stump()
    {
      /*  anim[0].SetInteger("state", 9);
        anim[1].SetInteger("state", 9);
        StartCoroutine("delayToMove", setthing.timeDelayToMove);*/
    }
  


    //END INTERFACE
   
    IEnumerator delayToMove(float timeWay)
    {
        //setCanMove(false);
        yield return new WaitForSeconds(timeWay);
       // setCanMove(true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall") && !checkers.isGrounded)
        {
            checkers.canMove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log(other.transform.tag+"-"+ other.transform.name);
        if (other.transform.tag=="coffin")
        {
            Debug.Log("sldk");
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    public Rigidbody GetRigidbody()
    {
        return rg;
    }
    public Vector3 GetInputMove()
    {
        return inputs;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(checkers.parentObj.position, checkers. parentObj.position + checkers.parentObj.forward * checkers.currentHitDistanceFront);
        Gizmos.DrawWireSphere(checkers.parentObj.position + checkers.parentObj.forward * checkers.currentHitDistanceFront, checkers.radiusCheckFront);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkers.parentObj.position, checkers.parentObj.position + Vector3.down * checkers.maxDistanceGrounded);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkers.parentObj.position,checkers.radiusCollisionThrow);
    }


}