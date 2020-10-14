using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullets : MonoBehaviour {

    public float damage = 0.5f;
    public float speedBullet = 0.5f;
    public ParticleSystem bloodParticle;
    public GameObject particleImpactBlood;
    public bool playerEnemi;
    public LayerMask layerMask;
    public int type = 0;
    private Rigidbody rg;
    [SerializeField]
    private int indexWeapon;

    void Start () {
       rg= GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(transform.position, transform.position+ transform.forward * 0.5f, Color.red);

        if (type == 0)
        {
            rg.velocity = transform.TransformDirection(Vector3.forward * speedBullet);
            Destroy(gameObject, 1.5f);
        }
        else if (type == 1)
        {
            rg.velocity = transform.forward * speedBullet;//transform.TransformDirection(Vector3.up * speedBullet);
            Destroy(gameObject, 1.5f);
        }

    
    }

    public void setNameWeapon(int nameW)
    {
        indexWeapon = nameW;
    }

    public int getIndexWeapon()
    {
        return indexWeapon;
    }

   
    void OnTriggerEnter(Collider collision)
    {
       // Debug.Log(collision.transform.position+"/"+collision.transform.localPosition);
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<ControlInteract>().settingLife(collision.transform.GetComponent<ControlInteract>().getLife()- damage);
            Debug.Log("colisiono con Player");
            Destroy(gameObject);

        }else if (collision.transform.tag == "NPC" && collision.GetComponent<BodyChange>()!=null && collision.GetComponent<BodyChange>().dominate)
        {
            Debug.Log("colisiono con NPC");
            Manager.instance.playerControl.transform.GetComponent<ControlInteract>().settingMana(0);
            collision.transform.GetComponent<BodyChange>().prepareToExpulsion();
        }
        
    }

    

    void OnCollisionEnter(Collision collision)
    {
       
    }

}
