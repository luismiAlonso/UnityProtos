using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlMove : MonoBehaviour
{
    public float speedMove;
    public Transform camera;
    private Vector3 forward, right;
    Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        //initIsometricView();
    }

    // Update is called once per frame
    void Update()
    {
        //inputKeyboardTopView();
        inputKeyBoardIsometricView();
    }
    
    /* 
    void initIsometricView()
    {
        forward = camera.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(0,90, 0) * forward;
    }*/

    void inputKeyBoardIsometricView()
    {
        /* //Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"),0,Input.GetAxis("VerticalKey"));
         Vector3 rightMovement = right * speedMove * Time.deltaTime * Input.GetAxis("HorizontalKey");
         Vector3 upMovement = forward * speedMove * Time.deltaTime * Input.GetAxis("VerticalKey");

         Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

         transform.forward = heading;
         transform.position += rightMovement;
         transform.position += upMovement;*/
        float verticalAxes = Input.GetAxisRaw("VerticalKey");
        float horizontalAxes = Input.GetAxisRaw("HorizontalKey");
        rg.velocity = new Vector3(verticalAxes * speedMove, 0,  horizontalAxes * speedMove); // sin aceleracion 
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void inputKeyboardTopView()
    {

        float verticalAxes = Input.GetAxisRaw("VerticalKey");
        float horizontalAxes = Input.GetAxisRaw("HorizontalKey");
        rg.velocity = new Vector3(horizontalAxes * speedMove, 0, verticalAxes * speedMove); // sin aceleracion 
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
         
    }


}
