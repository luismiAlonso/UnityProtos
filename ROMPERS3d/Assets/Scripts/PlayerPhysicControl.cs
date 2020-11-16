using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicControl : MonoBehaviour
{
    public float speedMove;
    public float gravity;
    public float radiusGrounded;
    public float forceSlope;
    public LayerMask layerMaskGround;
    public Transform pointGround;
    Vector3 input;
    Rigidbody rg;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slopeControl();
        checkGround();
        getInput();
        move();
    }

    void getInput()
    {
        input = new Vector3(InputControl.instance.getAxisControl().x,0, 0); 
        input=Vector3.ClampMagnitude(input, 1f);
    }

    private void move()
    {
        Vector3 axis=  input* speedMove;
        rg.velocity = new Vector3(axis.x,rg.velocity.y,rg.velocity.z);
    }

    void checkGround()
    {
         Collider[] checks = Physics.OverlapSphere(pointGround.position,radiusGrounded,layerMaskGround,QueryTriggerInteraction.Collide);

        if (checks.Length>0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

   void slopeControl()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,1.5f,layerMaskGround))
        {
            if (hit.transform!=null)
            {
                Debug.Log(Vector3.Angle(hit.normal, -transform.up));
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointGround.position,radiusGrounded);
    }
}
