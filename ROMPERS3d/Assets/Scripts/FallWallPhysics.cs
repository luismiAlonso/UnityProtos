using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWallPhysics : MonoBehaviour
{
    public float pushForce;
    private  List<GameObject> checkWalls;
    private Rigidbody rg;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        setCheckWalls();
        rg = GetComponent<Rigidbody>();
      //  rg.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void fallWall(Vector3 dirPoint)
    {
        rg.isKinematic = false;
        active = true;
        rg.AddForce(dirPoint * pushForce,ForceMode.VelocityChange);
    }


    IEnumerator IEfallWall()
    {
        var rot = Quaternion.FromToRotation(transform.forward, Vector3.forward);

        while (true)
        {
            Debug.Log(new Vector3(rot.x, rot.y, rot.z));
            rg.AddTorque(new Vector3(rot.x, rot.y, rot.z) * pushForce);

            yield return null;
        }
    }

    public void unableCheckWalls()
    {
        for (int i=0;i< checkWalls.Count;i++)
        {
            checkWalls[i].SetActive(false);
        }
    }

    void setCheckWalls()
    {
        Transform[] elements = transform.GetComponentsInChildren<Transform>(true);
        checkWalls = new List<GameObject>();

        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].GetComponent<CheckWall>()!=null)
            {
                checkWalls.Add(elements[i].gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "movilWall" && active)
        {
            Debug.Log(collision.transform.name);

            if (collision.transform.GetComponent<FrontWall>()!=null || collision.transform.GetComponent<TopWall>()!=null) {
                collision.transform.GetComponent<Rigidbody>().isKinematic = false;
            }

        }else if (collision.transform.tag == "movilWall" && !active)
        {
           // Destroy();
        }
    }
}
