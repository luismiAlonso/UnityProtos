using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallNode : MonoBehaviour
{

    public enum DirWall { top, down, left, right }
    public DirWall dirwall;
    public float timeRompers;
    public MasterWall masterWall;
    public bool solidWall;
    private int idCode;
    private Vector3 dirImpulse;
    private Quaternion oldRotation;
    private float timeToresetWall;
    private float timeAuxResetWall;
    bool activeRomper;
    bool wallIsDown;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        resetWall();
    }

    public void fallWall(string dirHit)
    {

        StartCoroutine("IEfallWall", dirHit);
       // Debug.Log(dir);   
    }

    public void prepareFallWall(Vector3 pointContact)
    {
        if (!solidWall) {
            masterWall.fallwallNodes(pointContact);
            dirImpulse = pointContact;
        }
    }

    public IEnumerator IEfallWall(string dirHit)
    {
        Quaternion targetAngle =Quaternion.identity;
        oldRotation = transform.rotation;

        if (dirHit=="front"  && (dirwall==DirWall.left || dirwall == DirWall.right)) {

             targetAngle = Quaternion.Euler(-180, 90, 0);
        }
        else if (dirHit == "back" && (dirwall == DirWall.left || dirwall == DirWall.right))
        {
            targetAngle = Quaternion.Euler(0, 90, 0);

        }else if (dirHit == "front" && (dirwall == DirWall.top || dirwall == DirWall.down))
        {
            targetAngle = Quaternion.Euler(-180, 0, 0);
           // Debug.Log("top");

        }else if (dirHit == "back"  && (dirwall == DirWall.top || dirwall == DirWall.down))
        {
            targetAngle = Quaternion.Euler(0, 0, 0);
           // Debug.Log("back");

        }

        while (Quaternion.Angle(transform.rotation, targetAngle) >= 0.01f )
        {
            activeRomper = true;
            // Debug.Log(Quaternion.Angle(transform.rotation, targetAngle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, timeRompers*Time.deltaTime);
            yield return null;
        }
        wallIsDown = true;
        activeRomper = false;
        transform.rotation = targetAngle;

    }

    void resetWall()
    {
        if (wallIsDown)
        {
            if (timeAuxResetWall==timeToresetWall)
            {
                //sin animacion o fx
                transform.rotation = oldRotation;
                wallIsDown = false;
            }
            else
            {
                timeAuxResetWall += Time.deltaTime;
            }
        }
    } 

    public void setTimeToResetRomper(float _timeToResetRomper)
    {
        timeToresetWall = _timeToResetRomper;
    }

    public bool getActiveRomper()
    {
        return activeRomper;
    }

    public void setIdCode(int _idCode)
    {
        idCode = _idCode;
    }

    public int getCode()
    {
        return idCode;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player")
        {

        }else if (collision.transform.tag == "movilWall" && idCode!=collision.transform.GetComponent<wallNode>().getCode())
        {
            collision.transform.GetComponent<wallNode>().prepareFallWall(dirImpulse);
        }
    }

    void OnDrawGizmos()
    {
        
    }

}
