using UnityEngine;
using System.Collections;

public class mouseControl : MonoBehaviour {

	GameObject player;
    public bool usePad;
    public Camera secondaryCamera;
    private Quaternion previusAngle;
    private float deadZone;

    void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
        //transform.position = player.transform.position;
	}
	

	void LateUpdate () {
		mouseController();

    }

    
	void mouseController(){

        Vector3 mousePos = Util.getMousePointWorld(usePad); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (!usePad) {

            transform.position =new Vector3( mousePos.x,0,mousePos.z);
            player.transform.rotation = Quaternion.LookRotation(Vector3.down, transform.position - player.transform.position);
        }
        else
        {
            
              if (mousePos.sqrMagnitude > 0.0f)
              {

                  previusAngle = Quaternion.LookRotation(Vector3.down, mousePos);
                  //player.transform.rotation = previusAngle;
                  player.transform.rotation = Quaternion.Lerp(transform.rotation, previusAngle, Time.deltaTime*10);

              }
              else
              {

                  if (!mousePos.Equals(Vector3.zero)) {
                      player.transform.rotation = previusAngle; //Quaternion.LookRotation(Vector3.down, mousePos);
                      player.transform.rotation = Quaternion.Lerp(transform.rotation, previusAngle, Time.deltaTime*10);
                  }
              }
              
        }

    }
}
