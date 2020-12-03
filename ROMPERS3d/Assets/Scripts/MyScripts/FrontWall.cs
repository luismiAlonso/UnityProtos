using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWall : MonoBehaviour,IWall
{
    public enum TypeSmaterial { barro, metal, lava, hielo, goma }
    public TypeSmaterial typeSmaterial;
    public float lifeWall;
    public Vector3 TimeScale;
    public Vector3 maxIncrement;
    public float timeForDestroy;
    public float fallMultiplier;

    private Vector3 origScale;
    private Vector3 scale;
    private Rigidbody rg;
    private bool checkFloor;
    private SetEffects setEffects;

    private void Awake()
    {
        origScale = transform.localScale;
        scale = transform.localScale;
        rg = GetComponent<Rigidbody>();
        setEffects = GetComponent<SetEffects>();
    }

    public void DesgasteWall(float value)
    {
        if (typeSmaterial==TypeSmaterial.barro) {

            lifeWall -= value;

            if (lifeWall < 0)
            {
                Destroy(gameObject);
            }
        }
       // throw new System.NotImplementedException();
    }

    public string getTypeWall()
    {
       // Debug.Log(typeSmaterial.ToString());
        return typeSmaterial.ToString();
    }

    public Vector3 getTimeScale()
    {
        return TimeScale;
    }

    public Vector3 getMaxIncrement()
    {
        return maxIncrement;
    }

    public Vector3 getOriginalScale()
    {
        return origScale;
    }

    public Animator getAnim()
    {
        throw new System.NotImplementedException();
    }

    public void ActiveSpecial()
    {
        //setEffects.noneFx("fxGrow");
        // throw new System.NotImplementedException();
    }

    public void growUpWall()
    {
        setEffects.PlayFx("fxGrow");
        scale.y = Mathf.Lerp(transform.localScale.y, maxIncrement.y, Time.deltaTime * TimeScale.y);
        transform.localScale = scale;
    }

    public bool fallsHeigth()
    {
        RaycastHit hitInfo;
        bool checkGround=false;

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10f))
        {
            checkGround = true;

        }

       return checkGround;
    }

    public bool isActiveWall()
    {
        return GetComponent<FallWallPhysics>().getActive();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (typeSmaterial==TypeSmaterial.barro || typeSmaterial == TypeSmaterial.metal)
        {

            if (fallsHeigth())
            {
                rg.isKinematic = false;
                rg.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            };

            if (timeForDestroy < 0 && typeSmaterial == TypeSmaterial.barro)
            {
                Destroy(gameObject);
            }
            else
            {
                timeForDestroy -= Time.deltaTime;
            }
        }
    }



    private void OnCollisionStay(Collision collision)
    {

        if (collision.transform.tag == "Enemy" && typeSmaterial == TypeSmaterial.barro || typeSmaterial == TypeSmaterial.metal)
        {
            Vector3 normal = collision.contacts[0].normal;

            if (Util.getRoundVector(normal) == Util.getRoundVector(transform.forward))
            {
                Debug.Log("WORKED FORWARD");
            }

            else if (Util.getRoundVector(normal) == Util.getRoundVector(-(transform.forward)))
            {
                Debug.Log("WORKED BACKWARD");
            }

            else if (normal == transform.right)
            {
                Debug.Log("WORKED RIGHT");
            }

            else if (Util.getRoundVector(normal) == Util.getRoundVector(-(transform.forward)))
            {
                Debug.Log("WORKED LEFT");
            }

            else if (Util.getRoundVector(normal) == Util.getRoundVector(transform.up))
            {
                Debug.Log("CIRCLE");
            }

            else if (Util.getRoundVector(normal) == Util.getRoundVector(-(transform.up)))
            {
                Debug.Log("TOP");
            }
            else
            {
                return;
            }

        }
    }

    
}
