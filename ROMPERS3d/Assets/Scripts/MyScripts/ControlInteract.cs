using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInteract : MonoBehaviour
{
    public LayerMask layerMaskFront;
    public LayerMask layerMaskTerrain;

    RaycastHit hitTerrain;
    RaycastHit hit;
    Rigidbody rb;

    bool checkFront;
    bool checkDown;
    bool checkTop;
    bool checkWallInteract;

    private CMF.AdvancedWalkerController playerControl;

    private void Awake()
    {
        playerControl = GetComponent<CMF.AdvancedWalkerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }


    void CheckInput()
    {
        if (InputControl.instance.getButtonsControl("Button1") && !CheckTop() && CheckFront()) {


            if (hit.transform != null && hit.transform.GetComponent<FallWallPhysics>() != null)
            {
                //hit.transform.GetComponent<FallWallPhysics>().unableCheckWalls();
                hit.transform.GetComponent<FallWallPhysics>().fallWall(transform.forward);
            }

        }
        else if (InputControl.instance.getButtonsControlOnPress("Button1") && CheckTop())
        {
            if (hit.transform != null && hit.transform.GetComponent<TopWall>() != null)
            {
                //hit.transform.GetComponent<FallWallPhysics>().unableCheckWalls();             
                hit.transform.GetComponent<TopWall>().incerementForce();
            }
           
        }
        else if (InputControl.instance.getButtonsControlOnRelease("Button1") && CheckTop())
        {
            if (hit.transform != null && hit.transform.GetComponent<TopWall>() != null)
            {
                hit.transform.GetComponent<TopWall>().ThrowWallTop();
            }
        }
    }


    public bool CheckFront()
    {
        checkFront = Physics.Raycast(transform.position, transform.forward, out hit, 1.0f, layerMaskFront, QueryTriggerInteraction.UseGlobal);
        return checkFront;
    }

    public bool CheckTop()
    {
        checkTop = Physics.Raycast(transform.position, transform.up, out hit, 3.0f, layerMaskFront, QueryTriggerInteraction.UseGlobal);
        return checkTop;
    }

    public bool CheckDown()
    {
        checkDown = Physics.Raycast(transform.position, -transform.up, out hit, 3.0f, layerMaskFront, QueryTriggerInteraction.UseGlobal);
        return checkDown;
    }

    public bool isBlockPath()
    {
        checkWallInteract = Physics.Raycast(transform.position,transform.forward, out hitTerrain, 0.4f, layerMaskTerrain, QueryTriggerInteraction.UseGlobal);
        //Draw a Ray forward from GameObject toward the hit
        return checkWallInteract;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            // rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
     
    }

    private void OnCollisionExit(Collision collision)
    {
      
    }

    private void OnDrawGizmos()
    {

        if (checkWallInteract) {

            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.forward * hitTerrain.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * hitTerrain.distance, transform.localScale);
        }
        else
        {
            Gizmos.color = Color.green;

            Gizmos.DrawRay(transform.position, transform.forward * 0.1f);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * 0.1f , transform.localScale);
        }
    }
}
