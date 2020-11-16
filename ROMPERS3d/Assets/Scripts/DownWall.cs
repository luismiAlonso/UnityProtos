using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DownWall : MonoBehaviour,IWall
{
    public enum TypeSmaterial { barro, metal, lava, hielo, goma }
    public TypeSmaterial typeSmaterial;
    public float lifeWall;
    public Vector3 TimeScale;
    public Vector3 maxIncrement;

    private Vector3 origScale;
    private Animator anim;

    private void Awake()
    {
        origScale = transform.localScale;
        anim = GetComponent<Animator>();
    }

    public void DesgasteWall(float value)
    {
        lifeWall -= value;
        if (lifeWall < 0)
        {
            Destroy(gameObject);
        }
    }

    public string getTypeWall()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag=="Player" && typeSmaterial==TypeSmaterial.goma)
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
                if (!collision.transform.GetComponent<CreateWallPlayer>().getOnAction()) {
                    collision.transform.GetComponent<JumpControl>().remoteJump(3);
                }
            }
            else
            {
                return;
            }
        }
    }

   
}
