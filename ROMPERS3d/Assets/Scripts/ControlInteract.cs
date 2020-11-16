using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInteract : MonoBehaviour
{
    public LayerMask layerMaskFront;
    RaycastHit hit;
    Rigidbody rb;

    bool checkFront;
    bool checkDown;
    bool checkTop;

    private void Awake()
    {

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

           /* if (hit.transform != null && hit.transform.GetComponent<CheckWall>() != null) {
                Vector3 dirPoint =  hit.point - transform.position ;
                hit.transform.GetComponent<CheckWall>().wallNode.unableCheckWalls();
                hit.transform.GetComponent<CheckWall>().wallNode.fallWall(dirPoint);
            }*/ 

        }else if (InputControl.instance.getButtonsControl("Button1") && CheckTop())
        {
            if (hit.transform != null && hit.transform.GetComponent<TopWall>() != null)
            {
                //hit.transform.GetComponent<FallWallPhysics>().unableCheckWalls();
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
        checkTop = Physics.Raycast(transform.position, transform.up, out hit, 1.0f, layerMaskFront, QueryTriggerInteraction.UseGlobal);
        return checkTop;
    }

    public bool CheckDown()
    {
        checkDown = Physics.Raycast(transform.position, -transform.up, out hit, 1.0f, layerMaskFront, QueryTriggerInteraction.UseGlobal);
        return checkDown;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            // rb.constraints = RigidbodyConstraints.FreezeAll;

        }
    }

    void OnDrawGizmosSelected()
    {

        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1.0f);

    }

   

}
