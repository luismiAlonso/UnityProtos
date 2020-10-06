using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCameraFollow : MonoBehaviour
{


    public LayerMask layerMask;

    public Transform limitLeft;
    public Transform limitRight;
    public Transform limitTop;
    public Transform limitDown;

    private RaycastHit hitCenter;
   /* private RaycastHit hitTop;
    private RaycastHit hitDown;
    private RaycastHit hitLeft;
    private RaycastHit hitRight;*/

    private bool checkLeft;
    private bool checkRight;
    private bool checkTop;
    private bool checkDown;
    private bool checkCenter;

    public struct DirCheckLimitCams
    {
        public bool left;
        public bool right;
        public bool top;
        public bool down;
        public bool center;

    }

    DirCheckLimitCams dcl;
    private Vector3 pointCenter;
    private bool canMoveCamera=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        limitFollowCam();
    }

     void limitFollowCam()
    {
        checkCenter = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hitCenter, 1000, layerMask);

        if (checkCenter) {
            pointCenter = hitCenter.point;
        }

        checkLeft = Physics.Raycast(limitLeft.position, transform.TransformDirection(Vector3.forward), 1000, layerMask);
        checkRight = Physics.Raycast(limitRight.position, transform.TransformDirection(Vector3.forward), 1000, layerMask);
        checkTop = Physics.Raycast(limitTop.position, transform.TransformDirection(Vector3.forward), 1000, layerMask);
        checkDown = Physics.Raycast(limitDown.position, transform.TransformDirection(Vector3.forward), 1000, layerMask);
        dcl = new DirCheckLimitCams();
        dcl.left = checkLeft;
        dcl.right = checkRight;
        dcl.top = checkTop;
        dcl.down = checkDown;

    }

    public void setCanMoveCamera(bool _canMoveCamera)
    {
        canMoveCamera = _canMoveCamera;
    }

    public bool getCanMoveCam()
    {
        return canMoveCamera;
    }

    public DirCheckLimitCams getDisCheckLimitCam()
    {
        return dcl;
    }

    public Vector3 getPointCenter()
    {
        return pointCenter;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitLeft.position, transform.TransformDirection(Vector3.forward * 1000));
        Gizmos.DrawLine(limitRight.position, transform.TransformDirection(Vector3.forward * 1000));
        Gizmos.DrawLine(limitTop.position, transform.TransformDirection(Vector3.forward * 1000));
        Gizmos.DrawLine(limitDown.position, transform.TransformDirection(Vector3.forward * 1000));
        Gizmos.DrawLine(transform.position, transform.TransformDirection(Vector3.forward * 1000));

    }
}
