using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRestart : MonoBehaviour
{
    public Vector3 dirOfDead;
    public bool AlwaysDead;

    private bool canDead;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
            if (AlwaysDead) {

                other.transform.position = Manager.instance.checkPoints[0].position; 
            }
            else
            {
                checkDirectionDead(other.transform.position);

                if (canDead)
                {
                    other.transform.position = Manager.instance.checkPoints[0].position;

                }
            }
        }
    }

    //check if dir move is oposite to player
    void checkDirectionDead(Vector3 dirPress)
    {
        Vector3 dirToDead = (transform.position - dirPress).normalized;

        if (dirOfDead.x!=0 && dirToDead.x < 0)
        {

            if (dirOfDead.x < 0 )
            {
                canDead = true;
            }

        }
        else if (dirOfDead.x != 0 && dirToDead.x > 0)
        {
            if (dirOfDead.x > 0)
            {
                canDead = true;
            }

        }else if (dirOfDead.y != 0 && dirToDead.y < 0)
        {
            if (dirOfDead.y < 0)
            {
                canDead = true;
            }
        }

        Debug.Log(dirToDead);
    }
}
