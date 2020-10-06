using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLigthing : MonoBehaviour
{
    [SerializeField]
    Transform targetToRotate;
    [SerializeField]
    Vector3[] limitRotationPoint;
    [SerializeField]
    float speedMoveLithing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputKeyBoardMovementLighting();
    }

    void inputKeyBoardMovementLighting()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {


          /*  if (transform.position.y <= limitRotationPoint[0].y) {

                transform.position = new Vector3(transform.position.x, limitRotationPoint[0].y, limitRotationPoint[0].z);
            }
            transform.RotateAround(targetToRotate.position, -Vector3.left, speedMoveLithing * Time.deltaTime);*/


        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            
            /*if (transform.position.y <= limitRotationPoint[0].y)
            {

                transform.position = new Vector3(transform.position.x, limitRotationPoint[0].y, limitRotationPoint[0].z);
            }
            transform.RotateAround(targetToRotate.position, Vector3.left, speedMoveLithing * Time.deltaTime);*/

        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
           /* if (transform.position.y <= limitRotationPoint[0].y)
            {

                transform.position = new Vector3(transform.position.x, limitRotationPoint[0].y, transform.position.z);
            }

            transform.RotateAround(targetToRotate.position, Vector3.forward, speedMoveLithing * Time.deltaTime);*/

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            /*if (transform.position.y <= limitRotationPoint[0].y)
            {

                transform.position = new Vector3(transform.position.x, limitRotationPoint[0].y, transform.position.z);
            }
            transform.RotateAround(targetToRotate.position, -Vector3.forward, speedMoveLithing * Time.deltaTime);*/

        }
    }
}
