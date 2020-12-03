using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TopWall : MonoBehaviour,IWall
{

    public enum TypeSmaterial { barro ,metal, lava, hielo, goma }
    public TypeSmaterial typeSmaterial;
    public float lifeWall;
    public Vector3 TimeScale;
    public Vector3 maxIncrement;
    public Vector3 rescaleWall;


    public float maxForceThrow;
    public float incForce;
    bool isPorting;
    Rigidbody rg;

    private float auxGrowForce;
    private Vector3 origScale;
    private Vector3 scale;
    private SetEffects setEffects;

    private void Awake()
    {
        origScale = rescaleWall;
        scale = rescaleWall;
        setEffects = GetComponent<SetEffects>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayer(Transform _player)
    {
        transform.parent = _player.transform;
        transform.localScale = rescaleWall;
        isPorting = true;
    }

    public void ThrowWallTop()
    {
        rg.isKinematic = false;
        transform.parent = null;
        rg.AddForce(transform.forward* Mathf.Clamp(auxGrowForce, incForce, maxForceThrow), ForceMode.VelocityChange);
    }

    void movingWallTop()
    {
       // Vector3 posPlayer = player.position;
       // transform.position = new Vector3(player.position.x,transform.position.y, transform.position.z);
    }  

    public string getTypeWall()
    {
        return typeSmaterial.ToString();
    }

    public void incerementForce()
    {
        auxGrowForce += Time.deltaTime * incForce;
        if (typeSmaterial == TypeSmaterial.barro) {
            setEffects.PlaySx("SxWallBarro");
        }else if (typeSmaterial == TypeSmaterial.metal)
        {
            setEffects.PlaySx("SxWallMetal");

        }
        // Debug.Log(auxGrowForce);
    }

    public void DesgasteWall(float value)
    {
        lifeWall -= value;
        if (lifeWall < 0)
        {
            Destroy(gameObject);
        }
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

    public bool isActiveWall()
    {
        throw new System.NotImplementedException();
    }

    public void ActiveSpecial()
    {
        if (typeSmaterial == TypeSmaterial.goma)
        {
            Manager.instance.playerControl.GetComponent<IngravityFall>().setIngravity(false, this.gameObject);

        }
        
    }

    public void growUpWall()
    {
        if (typeSmaterial == TypeSmaterial.goma)
        {
            scale.y = Mathf.Lerp(transform.localScale.y, getMaxIncrement().y, Time.deltaTime * getTimeScale().y);
            scale.x = Mathf.Lerp(transform.localScale.x, getMaxIncrement().x, Time.deltaTime * getTimeScale().x);
            scale.z = Mathf.Lerp(transform.localScale.z, getMaxIncrement().z, Time.deltaTime * getTimeScale().z);
            transform.localScale = scale;

            Manager.instance.playerControl.GetComponent<IngravityFall>().setIngravity(true, this.gameObject);
            Manager.instance.playerControl.GetComponent<IngravityFall>().pushUp();
            setEffects.PlaySx("SxWallGoma");

        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player" && typeSmaterial == TypeSmaterial.goma)
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
                Debug.Log("WORKED DOWN");
               
            }
            else
            {
                return;
            }
        }

        Debug.Log("colision ground "+ collision.transform.tag);

        if (collision.transform.tag == "ground" && typeSmaterial == TypeSmaterial.barro)
        {
            //Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ground" && typeSmaterial == TypeSmaterial.goma)
        {
            Manager.instance.playerControl.GetComponent<IngravityFall>().setIngravity(false, this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
