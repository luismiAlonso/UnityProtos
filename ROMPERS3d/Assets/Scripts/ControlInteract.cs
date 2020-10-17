using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInteract : MonoBehaviour
{
    public LayerMask layerMaskFront;
    RaycastHit hit;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFront();
    }

    void CheckFront()
    {
        if (InputControl.instance.getButtonsControl("Button1")) {

            bool checkFront = Physics.Raycast(transform.position, transform.forward, out hit, 1.0f, layerMaskFront, QueryTriggerInteraction.UseGlobal);
            if (hit.transform != null && hit.transform.GetComponent<wallNode>() != null)
            {
                hit.transform.GetComponent<wallNode>().prepareFallWall(hit.normal);
                Debug.Log("hit");
            }
        }
    }

    void OnDrawGizmosSelected()
    {

        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1.0f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="Enemy")
        {
           // rb.constraints = RigidbodyConstraints.FreezeAll;

        }
    }


}
