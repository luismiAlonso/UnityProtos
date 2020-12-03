using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSimpleIA : MonoBehaviour
{

    public bool isStoped;
    public float stopDistance;
    public float speedMove;
    public LayerMask layerMask;

    private Vector3 destination;
    private Vector3 oldDestination;
    private bool isMoving;
    private Rigidbody rg;
    private bool blocking;
    private IAcontrol IAc;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        IAc = GetComponent<IAcontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.fullStop) {
           // rotate();
            isBlocking();
        }

    }

    public void setDestination(Vector3 destiny)
    {
        if (!oldDestination.Equals(destiny)) {
            destination = destiny;
            StartCoroutine("IsetDestination");
            oldDestination = destination;
        }
        
    }

    IEnumerator IsetDestination()
    {

        while (!isStoped && Vector3.Distance(transform.position,destination) > stopDistance  && !blocking)
        { 
            Vector3 dir=Vector3.MoveTowards(transform.position,destination,speedMove * Time.deltaTime);
            rg.MovePosition(dir);
            isMoving = true;
            IAc.getAnim().SetBool("isMoving",true);
            yield return null;
        }

        IAc.getAnim().SetBool("isMoving", false);
        isMoving = false;
    }

    void isBlocking()
    {
        blocking = Physics.Raycast(transform.position,transform.forward,0.5f,layerMask,QueryTriggerInteraction.Collide);
    }

    void rotate()
    {
        Vector3 dir = (transform.position - destination).normalized;

        if (dir.x > 0)
        {
            transform.eulerAngles = new Vector3(0,270,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0,90, 0);

        }
    }

    public bool getMoving()
    {
        return isMoving;
    }

    public bool getStoped()
    {
        return isStoped;
    }

    public void setStopped(bool stop)
    {
        isStoped = stop;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag=="movilWall")
        {
            if (collision.transform.GetComponent<IWall>().isActiveWall() && collision.transform.GetComponent<IWall>().getTypeWall()!="goma")
            {
                Destroy(gameObject);
            }
            else
            {
                rg.isKinematic = true;
            }

        }else if (collision.transform.tag=="Player")
        {
            rg.isKinematic = true;
            collision.transform.position= Manager.instance.checkPoints[0].position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "movilWall")
        {
            if (!collision.transform.GetComponent<IWall>().isActiveWall())
            {
                rg.isKinematic = false;
            }

        }
        else if (collision.transform.tag == "Player")
        {
            rg.isKinematic = false;

        }
        
    }
}
