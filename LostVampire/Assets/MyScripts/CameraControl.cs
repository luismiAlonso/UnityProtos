﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    private static CameraControl cameraControl;
    public static CameraControl instance
    {
        get
        {
            if (cameraControl == null)
            {
                cameraControl = FindObjectOfType<CameraControl>();
            }
            return cameraControl;
        }
    }

    public LimitCameraFollow limitCameraFollow;
    public Transform target;
    public Vector3 offset;
    public Vector3[] shakePoints;
    public float margenDrag;
	public Vector3 m_Velocity;
    public float speed;
    public float m_SmoothTime;
    public int state = 1;

    private Rect screenRect;
    private int screenBoundsWidth;
	private int screenBoundsHeight;
    private int indexPoints = 0;
   

    void Start () {

        //dimensiones de pantalla
        screenBoundsWidth = Screen.width;
		screenBoundsHeight = Screen.height;
		screenRect = new Rect (0, 0, screenBoundsWidth, screenBoundsHeight);//marco limitador de pantalla

    }

    private void LateUpdate()
    {
        
        if (state==0)
        {
            StartFollowPlayer();

        }else if (state==1)
        {
            moveLookCam();
        }
        else if(state==2)
        {
            StartFollowPlayer();
            shaking();

        }else if (state==3)
        {
            deadCam();
        }

    }

   

    public void setState(int _state)
    {
        state = _state;
    }

    public void StartFollowPlayer()
    {
        //transform.position = new Vector3(player.transform.position.x + position.x, transform.position.y, player.transform.position.z + position.z);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z), Time.deltaTime * speed);
        Vector3 targetPos = target.position;
        //Keep camera height the same
        targetPos.y = transform.position.y;
        targetPos = Vector3.SmoothDamp(transform.position, targetPos, ref m_Velocity, m_SmoothTime);

        if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && limitCameraFollow.getDisCheckLimitCam().left &&
             target.GetComponent<PlayerControl>().GetInputMove().z ==0)
        {
            transform.position = targetPos;
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0  && limitCameraFollow.getDisCheckLimitCam().right &&
             target.GetComponent<PlayerControl>().GetInputMove().z == 0)
        {
            transform.position = targetPos;
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().z < 0  && limitCameraFollow.getDisCheckLimitCam().down &&
            target.GetComponent<PlayerControl>().GetInputMove().x==0)
        {
            transform.position = targetPos;
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().z > 0 && limitCameraFollow.getDisCheckLimitCam().top &&
            target.GetComponent<PlayerControl>().GetInputMove().x==0)
        {
            transform.position = targetPos;
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && limitCameraFollow.getDisCheckLimitCam().left &&
                target.GetComponent<PlayerControl>().GetInputMove().z > 0 && !limitCameraFollow.getDisCheckLimitCam().top)
        {
            transform.position = new Vector3(targetPos.x,transform.position.y,transform.position.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && limitCameraFollow.getDisCheckLimitCam().left &&
               target.GetComponent<PlayerControl>().GetInputMove().z > 0 && limitCameraFollow.getDisCheckLimitCam().top)
        {

            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && !limitCameraFollow.getDisCheckLimitCam().left &&
              target.GetComponent<PlayerControl>().GetInputMove().z > 0 && limitCameraFollow.getDisCheckLimitCam().top)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0 && limitCameraFollow.getDisCheckLimitCam().right &&
             target.GetComponent<PlayerControl>().GetInputMove().z > 0 && limitCameraFollow.getDisCheckLimitCam().top)
        {
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0 && limitCameraFollow.getDisCheckLimitCam().right &&
            target.GetComponent<PlayerControl>().GetInputMove().z > 0 && !limitCameraFollow.getDisCheckLimitCam().top)
        {
            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0 && !limitCameraFollow.getDisCheckLimitCam().right &&
            target.GetComponent<PlayerControl>().GetInputMove().z > 0 && limitCameraFollow.getDisCheckLimitCam().top)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && limitCameraFollow.getDisCheckLimitCam().left &&
                target.GetComponent<PlayerControl>().GetInputMove().z < 0 && !limitCameraFollow.getDisCheckLimitCam().down)
        {
            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && limitCameraFollow.getDisCheckLimitCam().left &&
               target.GetComponent<PlayerControl>().GetInputMove().z < 0 && limitCameraFollow.getDisCheckLimitCam().down)
        {
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x < 0 && !limitCameraFollow.getDisCheckLimitCam().left &&
              target.GetComponent<PlayerControl>().GetInputMove().z < 0 && limitCameraFollow.getDisCheckLimitCam().down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0 && limitCameraFollow.getDisCheckLimitCam().right &&
               target.GetComponent<PlayerControl>().GetInputMove().z < 0 && !limitCameraFollow.getDisCheckLimitCam().down)
        {
            transform.position = new Vector3(targetPos.x, transform.position.y, transform.position.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0 && !limitCameraFollow.getDisCheckLimitCam().right &&
              target.GetComponent<PlayerControl>().GetInputMove().z < 0 && limitCameraFollow.getDisCheckLimitCam().down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else if (limitCameraFollow.getCanMoveCam() && target.GetComponent<PlayerControl>().GetInputMove().x > 0 && limitCameraFollow.getDisCheckLimitCam().right &&
             target.GetComponent<PlayerControl>().GetInputMove().z < 0 && limitCameraFollow.getDisCheckLimitCam().down)
        {
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            limitCameraFollow.setCanMoveCamera(true);

        }
        else
        {
            if (!target.GetComponent<PlayerControl>().GetInputMove().Equals(Vector3.zero)) {
                limitCameraFollow.setCanMoveCamera(false);    
            }
        }

        //Debug.Log(Mathf.Abs(target.position.z) - Mathf.Abs(limitCameraFollow.getPointCenter().z));

        if ((Mathf.Abs(target.position.z) - Mathf.Abs(limitCameraFollow.getPointCenter().z))<=0.0f 
            && target.GetComponent<PlayerControl>().GetInputMove().z!=0)
        {
            limitCameraFollow.setCanMoveCamera(true);

        }
        if ((Mathf.Abs(target.position.x) - Mathf.Abs(limitCameraFollow.getPointCenter().x)) <= 0.0f 
            && target.GetComponent<PlayerControl>().GetInputMove().x!=0)
        {
            limitCameraFollow.setCanMoveCamera(true);
        }
    }

    public void setTarget(Transform _target)
    {
        target = _target;
    }

    public void shaking()
    {
        if (indexPoints==shakePoints.Length-1) {
            state = 0;
            indexPoints = 0;
          //  Debug.Log(state);
        }
        else
        {
            Vector3 nextDir = new Vector3(shakePoints[indexPoints].x + transform.position.x, transform.position.y,shakePoints[indexPoints].z+transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position,nextDir,Time.deltaTime*200);
            if (Vector3.Distance(transform.position,nextDir)<=0)
            {
                indexPoints++;
            }
          //  Debug.Log(indexPoints+"state "+state);
        }
    }

    
    public void moveLookCam()
    {
        /*Vector3 posCursor = Util.getMousePointWorld(true);
        
        //mirilla.transform.localPosition = transform.position;
        if (posCursor.sqrMagnitude > 0.0f) {
            transform.position = new Vector3(transform.position.x+posCursor.x*20f*Time.deltaTime,20,transform.position.z+posCursor.z*20f*Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x , 20, player.transform.position.z ); 
        }*/

    }

    void deadCam()
    {
        transform.Rotate(0,0,1.0f * Time.deltaTime);

    }

    void pameandoNoClick(){
		
		if (screenRect.Contains (Input.mousePosition)) {
			
			if (Input.mousePosition.x > screenBoundsWidth - margenDrag) {
                offset.x += speed * Time.deltaTime;
			}
			
			if (Input.mousePosition.x < 0 + margenDrag) {
                offset.x -= speed * Time.deltaTime;
			}
			
			if (Input.mousePosition.y > screenBoundsHeight - margenDrag) {
                offset.z += speed * Time.deltaTime;
			}
			
			if (Input.mousePosition.y < 0 + margenDrag) {
                offset.z -= speed * Time.deltaTime;
			}   
			
		
			transform.position = new Vector3 (Mathf.Clamp (offset.x, -30, 30),20,Mathf.Clamp (offset.z, -30, 30));  
		}
	}

   
}