using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAcontrol : MonoBehaviour
{
    public enum TypeEnemy { baseE, romperE, bombE, rodeE }
    public TypeEnemy typeEnemy;
    [SerializeField]
    private int IdEnemy;
    [SerializeField]
    private float distanceView;
    [SerializeField]
    private float speedMove;
    [SerializeField]
    private float timeToChangeDir;
    [SerializeField]
    private LayerMask layerMaskTargets;
    [SerializeField]
    private GameObject prefabBom;
    RaycastHit hitFront;
    RaycastHit hitBack;
    RaycastHit hitLeft;
    RaycastHit hitRight;

    bool checkFrontLeft;
    bool checkFront;
    bool checkFrontRight;

    bool checkBack;
    bool checkRight;
    bool checkLeft;
    bool canMove;
    bool activeReloj;

    float auxTimeToChangeDir;
    List<Vector3> freeDir = new List<Vector3>();
    Vector3 dirMove;
    Rigidbody rb;
    BoxCollider collider;
   
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        canMove = true;
        activeReloj = true;
        dirMove = transform.forward;
            
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        checkDirections();
        moveLineal();
    }

    private void Update()
    {
        if (typeEnemy== TypeEnemy.bombE || typeEnemy == TypeEnemy.baseE) { 
            relojToChangeDIrection();
        }
    }

    void checkDirections()
    {
        Vector3 size = collider.size;
        Vector3 center = new Vector3(collider.center.x, collider.center.y, collider.center.z);
        Vector3 vertex1 = new Vector3(center.x + size.x / 2, center.y, center.z + size.z / 2);
        Vector3 vertex4 = new Vector3(center.x - size.x / 2, center.y, center.z + size.z / 2);

        checkFrontLeft = Physics.Raycast(transform.TransformPoint(vertex1), transform.forward, out hitFront, distanceView, layerMaskTargets);
        checkFrontRight = Physics.Raycast(transform.TransformPoint(vertex4), transform.forward, out hitFront, distanceView, layerMaskTargets);

        checkFront = Physics.Raycast(transform.position, transform.forward, out hitFront, distanceView, layerMaskTargets);
        checkBack = Physics.Raycast(transform.position, transform.forward *-1, out hitBack, distanceView, layerMaskTargets);
        checkLeft = Physics.Raycast(transform.position, transform.right * -1, out hitLeft, distanceView, layerMaskTargets);
        checkRight = Physics.Raycast(transform.position, transform.right, out hitRight, distanceView, layerMaskTargets);
        //Debug.Log("front: "+checkFront+" back: "+checkBack+" left: "+checkLeft+" right: "+checkRight);

        if (checkFront || checkFrontLeft || checkFrontRight)
        {
            if (typeEnemy != TypeEnemy.romperE) {
                setFreeDir();
                dirMove = GetRandomDir();
                StartCoroutine("IEstayMoment");

                if (!dirMove.Equals(Vector3.zero))
                {
                    transform.rotation = Quaternion.LookRotation(dirMove); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), Time.deltaTime * 40f);
                }

            }else if (typeEnemy == TypeEnemy.romperE && hitFront.transform.GetComponent<wallNode>().solidWall )
            {
                setFreeDir();
                dirMove = GetRandomDir();
                StartCoroutine("IEstayMoment");

                if (!dirMove.Equals(Vector3.zero))
                {
                    transform.rotation = Quaternion.LookRotation(dirMove); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), Time.deltaTime * 40f);
                }

            }else if (typeEnemy == TypeEnemy.romperE && !hitFront.transform.GetComponent<wallNode>().solidWall)
            {
                romperWall(hitFront.point);
            }
            
        }

        if (checkLeft && checkRight && typeEnemy == TypeEnemy.romperE)
        {
            if (!hitLeft.transform.GetComponent<wallNode>().solidWall)
            {
                romperWall(hitLeft.point);
            }
            else if (!hitRight.transform.GetComponent<wallNode>().solidWall)
            {
                romperWall(hitRight.point);
            }
        }
    }

    void romperWall(Vector3 _dirWall)
    {
        canMove = false;
        Vector3 dirWall = _dirWall - transform.position;

        if (!dirWall.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.LookRotation(dirWall); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), Time.deltaTime * 40f);
        }
    }

    void relojToChangeDIrection()
    {
        if (activeReloj) {

            if (timeToChangeDir > auxTimeToChangeDir) {

                auxTimeToChangeDir += Time.deltaTime;

            } else {

                dirMove = GetRandomDir();
                StartCoroutine("IEstayMoment");

                if (typeEnemy == TypeEnemy.bombE && prefabBom!=null)
                {
                    GameObject bomb = Instantiate(prefabBom,transform.position,Quaternion.identity);
                }

                if (!dirMove.Equals(Vector3.zero))
                {
                    transform.rotation = Quaternion.LookRotation(dirMove); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), Time.deltaTime * 40f);
                }
                auxTimeToChangeDir = 0;
            }
        }
    }

    Vector3 GetRandomDir()
    {
        int ranDir = Random.Range(0, freeDir.Count);
        Vector3 newDir = freeDir[ranDir];
        return newDir;
    }

    void setFreeDir()
    {
        freeDir = new List<Vector3>();
        for (int i=0; i< 4;i++)
        {
            if (!checkFrontLeft && i==0)
            {
                freeDir.Add(transform.right * -1);

            }
            else if (!checkFrontLeft && i == 0)
            {
                freeDir.Add(transform.right * -1);

            }
            else if (!checkLeft && i == 0 )
            {
                freeDir.Add(transform.right * -1);

            }

            if (!checkRight && i == 1)
            {
                freeDir.Add(transform.right);

            }

            if (!checkFront && i == 2)
            {
                freeDir.Add(transform.forward);

            }

            if (!checkBack && i == 3)
            {
                freeDir.Add(transform.forward * -1);

            }

        }
    }

    private void moveLineal()
    {
        if (canMove) {
            rb.velocity = new Vector3(dirMove.x * speedMove, rb.velocity.y, dirMove.z * speedMove);
        }

    }

    IEnumerator IEstayMoment()
    {
        canMove = false;
        activeReloj = false;
        yield return new WaitForSeconds(1);
        canMove = true;
        activeReloj = true;
    }



    private void OnCollisionStay(Collision collision)
    {
        
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            canMove = false;

           /* dirMove = transform.InverseTransformDirection(dirMove);
            if (!dirMove.Equals(Vector3.zero))
            {
                transform.rotation = Quaternion.LookRotation(dirMove); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), Time.deltaTime * 40f);
            }*/
        }
        if(collision.gameObject.layer==8 && collision.transform.GetComponent<wallNode>()!=null 
            && !collision.transform.GetComponent<wallNode>().getActiveRomper())
        {
           /* rb.velocity = Vector3.zero;
            setFreeDir();
            dirMove = GetRandomDir();
            StartCoroutine("IEstayMoment");

            if (!dirMove.Equals(Vector3.zero))
            {
                transform.rotation = Quaternion.LookRotation(dirMove); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), Time.deltaTime * 40f);
            }*/

        }else if (collision.gameObject.layer == 8 && collision.transform.GetComponent<wallNode>() != null && collision.transform.GetComponent<wallNode>().getActiveRomper())
        {
           /* rb.velocity = Vector3.zero;
            canMove = false;
            Destroy(gameObject,0.2f);*/
        }
    }

    private void OnDrawGizmos()
    {
        // Draws a blue line from this transform to the target
     
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distanceView);
        /*Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * -1 * distanceView);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -1 * distanceView);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distanceView);*/

    }
}
