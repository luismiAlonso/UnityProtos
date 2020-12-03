using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWall : MonoBehaviour
{
    public float timeRompers;
    public Animator anim;

    private bool activeRomper;
    private Vector3 oldRotation;
    // Start is called before the first frame update
    void Start()
    {
        oldRotation = transform.localEulerAngles;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fallWall()
    {
         StartCoroutine("IEfallWal");
      
    }

    IEnumerator IEfallWal()
    {
        Quaternion targetAngle = Quaternion.identity;
        targetAngle = Quaternion.Euler(90,transform.parent.eulerAngles.y,transform.parent.eulerAngles.z);

        while (Quaternion.Angle(transform.rotation, targetAngle) >= 0.01f)
        {
            activeRomper = true;
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetAngle, timeRompers * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="Player")
        {
           // Debug.Log("colision");
        }
    }

}
