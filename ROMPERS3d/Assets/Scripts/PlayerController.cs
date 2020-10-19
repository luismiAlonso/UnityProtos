using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerSettings playerSettings;
    public Checkers checkers;
    InputControl inputControl;
    Rigidbody rb;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        inputControl = InputControl.instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LinealMove();
        checkers.canMove = false;
    }

    public void LinealMove()
    {
        Vector3 asix = Vector3.zero;

        if (InputControl.instance.getAxisControl().x > 0 || InputControl.instance.getAxisControl().x < 0)
        {
            asix = new Vector3(InputControl.instance.getAxisControl().x,0,0);
        }
        else if (InputControl.instance.getAxisControl().y > 0 || InputControl.instance.getAxisControl().y < 0)
        {
            asix = new Vector3(0, 0, InputControl.instance.getAxisControl().y);

        }
        else
        {
            asix = Vector3.zero;
        }
        speed = playerSettings.speedMove * Time.deltaTime;
        rb.velocity = new Vector3(asix.x * speed, rb.velocity.y, asix.z * speed);
        if (!asix.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(asix,Vector3.up), Time.deltaTime * 40f);

        }
    }


}
