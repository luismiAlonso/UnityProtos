using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSimple : MonoBehaviour
{
    public Camera camera;
    public float speedRotation;
    private CMF.AdvancedWalkerController playerControl;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<CMF.AdvancedWalkerController>();

    }

    // Update is called once per frame
    void Update()
    {
        calculateDir();
        rotate();
    }

    void calculateDir()
    {
        if (!playerControl.control2d)
        {
            angle = Mathf.Atan2(playerControl.getInput().x, playerControl.getInput().y);
            angle = Mathf.Rad2Deg * angle;
            angle += camera.transform.eulerAngles.y;
        }
    }

    public void rotate()
    {
        if (!playerControl.control2d)
        {
            if (!playerControl.getInput().Equals(Vector3.zero)  /*&& playerControl.checkers.canRotate*/)
            {
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
            }

        }
        else
        {

            if (Mathf.Abs(playerControl.getInput().x) != 0)
            {
                if (playerControl.getInput().x > 0f)
                {
                    angle = 90;

                }
                else if (playerControl.getInput().x < 0f)
                {
                    angle = 270;

                }

                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }

        /*
         * 
         * If use RigidBody 
         * 
        if (!rb.velocity.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        */
    }
}
