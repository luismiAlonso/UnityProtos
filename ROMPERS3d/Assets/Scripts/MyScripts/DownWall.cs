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
    public float timeForDestroy;

    private Vector3 origScale;
    private Vector3 scale;
    private Animator anim;
    private Rigidbody rg;
    private bool action;
    private SetEffects setEffects;

    private void Awake()
    {
        origScale = transform.localScale;
        anim = GetComponent<Animator>();
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
                setEffects.PlayFx("fxDestroyWall");
                setEffects.PlaySx("SxDestroyWallBarro");
                Destroy(gameObject);
            }
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

    public void growUpWall()
    {
        if (typeSmaterial == TypeSmaterial.barro)
        {
            scale.y = Mathf.Lerp(transform.localScale.y, maxIncrement.y, Time.deltaTime * TimeScale.y);
            transform.localScale = scale;
            setEffects.PlayFx("fxGrow");
        }
        else if (typeSmaterial == TypeSmaterial.goma)
        {
            scale.y = Mathf.Lerp(transform.localScale.y, maxIncrement.y, Time.deltaTime * maxIncrement.y);
            scale.x = Mathf.Lerp(transform.localScale.x, maxIncrement.z, Time.deltaTime * maxIncrement.z);
            transform.localScale = scale;

        }else if (typeSmaterial == TypeSmaterial.metal)
        {
            if (!action) {
                
                rg.isKinematic = false;
                Manager.instance.playerControl.GetComponent<Rigidbody>().isKinematic = true;
                rg.AddForce(Vector3.down * 50,ForceMode.VelocityChange);
                action = true;
                setEffects.PlayFx("fxGrow");

            }
        }
    }

    public void ActiveSpecial()
    {
        if (typeSmaterial == TypeSmaterial.goma) {

            Manager.instance.playerControl.jumpControlPhysic.remoteJump();
            setEffects.PlaySx("SxWallGoma");
        }
        else if (typeSmaterial == TypeSmaterial.metal)
        {
            Manager.instance.playerControl.GetComponent<Rigidbody>().isKinematic = false;
            setEffects.noneFx("fxGrow");

        }else if (typeSmaterial == TypeSmaterial.barro)
        {
            setEffects.noneFx("fxGrow");

        }
    }

    public bool isActiveWall()
    {
        throw new System.NotImplementedException();
       

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (typeSmaterial == TypeSmaterial.barro)
        {

            if (timeForDestroy < 0)
            {
                Destroy(gameObject);
                setEffects.PlayFx("fxDestroyWall");
                setEffects.PlaySx("SxDestroyWallBarro");

            }
            else
            {
                timeForDestroy -= Time.deltaTime;
            }
        }
    }

    IEnumerator restoreOnGroundDownWall()
    {
        float timeAnim = 0.1f;

        while (timeAnim > 0)
        {
            scale.y = Mathf.Lerp(transform.localScale.y, 50, Time.deltaTime * 40);
            scale.x = Mathf.Lerp(transform.localScale.x, 50, Time.deltaTime * 40);
            transform.localScale = scale;
            timeAnim -= Time.deltaTime;
            yield return null;
        }

       // prefWall = null;
        // StartCoroutine("IdelayCreateWall");
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

                if (!collision.transform.GetComponent<CreateWallPlayer>().getOnAction()) {
                    Debug.Log("WORKED DOWN");
                    collision.transform.GetComponent<JumpControlPhysic>().remoteJump();
                }
            }
            else
            {
                return;
            }
        }

        if (typeSmaterial == TypeSmaterial.metal && collision.transform.tag=="ground" || collision.transform.tag=="movilWall")
        {
            if (rg!=null) {
                rg.velocity = Vector3.zero;
            }

        }

    }

    
}
