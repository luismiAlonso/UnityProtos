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

    public float forceThrow; 
    bool isPorting;
    Rigidbody rg;

    private Vector3 origScale;

    private void Awake()
    {
        origScale = transform.localScale;
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
        isPorting = true;
    }

    public void ThrowWallTop()
    {
        rg.isKinematic = false;
        transform.parent = null;
        rg.AddForce(transform.forward* forceThrow,ForceMode.VelocityChange);
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
                //Debug.Log("WORKED DOWN");
                if (!collision.transform.GetComponent<CreateWallPlayer>().getOnAction())
                {
                    collision.transform.GetComponent<JumpControl>().remoteJump(3);
                }
            }
            else
            {
                return;
            }
        }

    }

    public Animator getAnim()
    {
        throw new System.NotImplementedException();
    }
}
