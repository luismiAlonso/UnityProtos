using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KinemaControl : MonoBehaviour
{
    public bool isPLayer;
    public float radiusCol;
    private Rigidbody rg;

    private void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
       /* onColl();

        */
    }

    /*void onColl()
    {
        if (isPLayer) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusCol, layerCol);

            foreach (Collider col in hitColliders)
            {
                col.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }*/

    public void setIsplayerKinema(bool _isPlayer)
    {
        isPLayer = _isPlayer;

        if (isPLayer)
        {
            rg.isKinematic = false;
        }
        else
        {
            rg.isKinematic = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusCol);
    }
}
