using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform pointGrab;
    public Transform parentLocation;

    RaycastHit hit;
    bool checkObj;
    bool isGrab;
    GameObject objectPort;
    PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        checkObj = Physics.Raycast(parentLocation.position, parentLocation.forward,out hit,2.5f,layerMask);

        if (hit.transform!=null)
        {
            objectPort = hit.transform.gameObject;
        }


       /* if (Input.GetKey(KeyCode.Space))
        {
            if (objectPort != null)
            {
                grabObject();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (objectPort != null)
            {
                objectPort.GetComponent<JumpControl>().throwUP();
                throwObject();
            }
        }*/
    }

    public void grabObject()
    {
        if (objectPort.transform.GetComponent<SimpleIA>()!=null) {
            objectPort.transform.GetComponent<SimpleIA>().getNavMeshAgent().enabled = false;
            objectPort.transform.GetComponent<SimpleIA>().enabled = false;
            objectPort.GetComponent<JumpControl>().enabled = true;
            objectPort.GetComponent<JumpControl>().setGravity(false);
            isGrab = true;
        }
        objectPort.transform.position = pointGrab.position;
        objectPort.transform.eulerAngles = transform.eulerAngles;
    }

    public void throwObject()
    {
        if (objectPort.transform.GetComponent<SimpleIA>() != null)
        {
          //  objectPort.transform.GetComponent<SimpleIA>().getNavMeshAgent().enabled = true;
          //  objectPort.transform.GetComponent<SimpleIA>().enabled = true;
            objectPort.GetComponent<JumpControl>().setGravity(true);

        }
        objectPort = null;
    }

    public GameObject getGrabObject()
    {
        if (objectPort!=null) {

            return objectPort;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(parentLocation.position, parentLocation.position +transform.forward * 2.5f);
    }

}
