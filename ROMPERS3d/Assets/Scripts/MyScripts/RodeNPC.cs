using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodeNPC : MonoBehaviour
{
    public LayerMask layerMaskGround;
    public LayerMask layerMaskFront;

    public float speedRode;
    public Transform checkGrounder;
    public float timeMove;
    public float timeNextBum=0.5f;
    public float rangeBum=2;

    private bool checkGround;
    private bool checkFronter;
    private bool checkBacker;
    private bool activo;
    private bool activeBum;
    private float auxTime;
    private bool onCollision;
    private Rigidbody rg;
    private IAcontrol IAc;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        IAc = GetComponent<IAcontrol>();
        auxTime = timeMove;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rodeToTarget(Vector3 target)
    {
        activeBum = false;
        StartCoroutine("IErode",target);
    }

    IEnumerator IErode(Vector3 point)
    {
        Vector3 dir= ( new Vector3(point.x, transform.position.y, 0) - transform.position).normalized;
        rotate(dir);
        rg.isKinematic = false;

        while (!activo && !checkFront() && !checkBack())
        {
            rg.MovePosition(transform.position + dir * speedRode * Time.deltaTime);
            IAc.getAnim().SetBool("isAtack",true);

            if (auxTime>0)
            {
                auxTime -= Time.deltaTime;
            }
            else
            {
                auxTime = timeMove;
                activo = true;
            }


            yield return null;
        }

        auxTime = timeMove;
        IAc.getAnim().SetBool("isAtack", false);

        //rg.isKinematic = true;

        activo = false;
    }

    IEnumerator delayToNextAtack(Vector3 point)
    {
        yield return new WaitForSeconds(0.5f);
        onCollision = false;
        StartCoroutine("IErode", point);
    }

    bool checkFloor()
    {
        checkGround = Physics.Raycast(checkGrounder.position, -transform.up, 0.5f, layerMaskGround, QueryTriggerInteraction.Collide);
        return checkGround; 
    }

    bool checkFront()
    {
        RaycastHit hitInfo;
        checkFronter = Physics.Raycast(checkGrounder.position, transform.forward,out hitInfo, 1.0f, layerMaskFront, QueryTriggerInteraction.Collide);

        if (hitInfo.transform!=null && hitInfo.transform.tag=="Player")
        {
            doitBum();
        }

        return checkFronter;
    }

    bool checkBack()
    {
        checkBacker = Physics.Raycast(checkGrounder.position, -transform.forward, 0.5f, layerMaskFront, QueryTriggerInteraction.Collide);
        return checkBacker;
    }

    void doitBum()
    {
        if (!activeBum)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position,rangeBum, layerMaskFront);

            for (int i=0;i < cols.Length; i++) {

                if (cols[i].transform.tag == "Player")
                {
                    cols[i].transform.position = Manager.instance.checkPoints[0].position;
                }
            }

          //  activeBum = true;
            
        }
    }

    void rotate(Vector3 dir)
    {
        if (dir.x > 0)
        {
            transform.eulerAngles = new Vector3(0,90,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0,270, 0);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,rangeBum);
    }
}
