using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ControlInteract))]
[RequireComponent(typeof(SetEffects))]
public class PlayerControl : MonoBehaviour {

    public SetthingAtributePlayer setthing;
    public Checkers checkers;
    public GameObject BodyAll;
    [HideInInspector]
    public ControlInteract controlInteract;
    // public AnimatorControl animatorControl;
    [HideInInspector]
    public SetEffects setEffects;
    [HideInInspector]
    public PlayerMove playerMove;
    [HideInInspector]
    public JumpControl jumpControl;
    [HideInInspector]
    public DashControl dashControl;
    [HideInInspector]
    public CharacterController characterController;

    Rigidbody rg;
    CameraControl cam;
    RaycastHit hit;
    Vector3 mousePos;
    Vector3 dirFacing;
    Vector3 inputs;


    void Awake()
    {
        cam = Camera.main.GetComponent<CameraControl>();
        playerMove = GetComponent<PlayerMove>();
        jumpControl = GetComponent<JumpControl>();
        dashControl = GetComponent<DashControl>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        setEffects = GetComponent<SetEffects>();
        controlInteract = GetComponent<ControlInteract>();
        checkers.isMovingMouse(mousePos = Util.getMousePointWorld(Manager.instance.GlobalUsePad));
    }

    void FixedUpdate()
    {
       
    }

    private void LateUpdate()
    {
       
    }

    private void Update()
    {

        if (!Manager.instance.fullStop)
        {
            //mouseController();
        }
        if (!Manager.instance.fullStop)
        {
            // move3d();
            inputs = new Vector3(InputControl.instance.getAxisControl().x, 0, InputControl.instance.getAxisControl().y);

            if (playerMove!=null) {
                checkers.isTraspasable();
                playerMove.setInput(inputs);
                playerMove.aplyGravity();
                playerMove.PlayerMovement();
                playerMove.calculateDir();
                playerMove.rotate();
                jumpControl.JumpInput();
                dashControl.inputDash();
            }
        }

    }

    /*
    void mouseController()
    {
        if (Manager.instance.faceControl)
        {

            mousePos = InputControl.instance.getAxisFree(); //Util.getMousePointWorld(Manager.instance.GlobalUsePad); //Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!Manager.instance.GlobalUsePad)
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
                         if (hit.transform.tag != "Player")
                         {
                             dirMouseHit = hit.point;
                             transform.LookAt(aimDir);
                         }

                        dirFacing = hit.point;
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir), Time.deltaTime * setthing.speedRotationLookMouse);
                    }
                }
            }
            else
            {
                /*if (!Droid.instance.isAutoimp) {

                      if (mousePos.sqrMagnitude > 0.0f)
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
                      }
                }
                else
                {
                    if (Droid.instance.getTarget()) {

                         Vector3 aimDir = new Vector3(Droid.instance.getTarget().position.x - transform.position.x, 2, Droid.instance.getTarget().position.z - transform.position.z);
                         aimDir.Normalize();

                         float angle = Mathf.Rad2Deg * Mathf.Atan2(aimDir.x, aimDir.z);
                         m_LookAngle = Mathf.SmoothDampAngle(m_LookAngle, angle, ref m_AngleVelocity, m_RotationSpeed);

                         transform.eulerAngles = new Vector3(90, m_LookAngle, 0);
                     }
                }

            }
        }

    }*/

  
    
    //END INTERFACE

    IEnumerator delayToMove(float timeWay)
    {
        //setCanMove(false);
        yield return new WaitForSeconds(timeWay);
        // setCanMove(true);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall") && checkers.blockingPass)
        {
            checkers.canMove = false;
        }
        else if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall") && !checkers.blockingPass)
        {
            checkers.canMove = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall") /*&& !characterController.isGrounded*/)
        {
            checkers.canMove = true;
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

    public void inactivePlayer()
    {
        Transform[] objs = BodyAll.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform obj in objs)
        {
            if (obj.GetComponent<MeshRenderer>()!=null)
            {
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        this.enabled = false;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(checkers.parentObj.position, checkers.parentObj.position + checkers.parentObj.forward * checkers.currentHitDistanceFront);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkers.parentObj.position + checkers.parentObj.forward * checkers.currentHitDistanceFront, checkers.radiusCheckFront);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkers.parentObj.position, checkers.parentObj.position + Vector3.down * checkers.maxDistanceGrounded);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkers.parentObj.position, checkers.radiusCollisionThrow);
    }




}
