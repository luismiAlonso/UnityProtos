using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    public GameObject projectil;
    public Transform pointDisparador;
    public Vector3 dirProjectil;
    public float delayToNext;
    public bool auto;
    public bool alwaysActive;
    public float rangeActivation;
    public LayerMask layerMask;

    private bool active;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        active = alwaysActive;
    }

    // Update is called once per frame
    void Update()
    {
        AreaAction();
        shootProjectil();
    }

    void shootProjectil()
    {
        if (!Manager.instance.fullStop) {

            if (active && !isShooting)
            {
                isShooting = true;
                GameObject proj = Instantiate(projectil,pointDisparador.position,Quaternion.identity);
                proj.transform.eulerAngles = dirProjectil;
                StartCoroutine("delayShoot");
            }
            else if(auto && !isShooting)
            {
                isShooting = true;
                GameObject proj = Instantiate(projectil, pointDisparador.position, Quaternion.identity);
                proj.transform.eulerAngles = dirProjectil;
                StartCoroutine("delayShoot");
            }
        }

       
    }


    void AreaAction()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position,rangeActivation, layerMask);

        if (cols.Length>0)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }


    IEnumerator delayShoot()
    {
        yield return new WaitForSeconds(delayToNext);
        isShooting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeActivation);
    }
}
