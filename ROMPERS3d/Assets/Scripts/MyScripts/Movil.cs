using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movil : MonoBehaviour
{
    public Vector3 dirMove;
    public float speedMoveOne,speedMoveTwo;
    public float distance;
    public float timeToNextAction;
    public TriggerComp triggerComp;
    public bool isLoop;
    public bool AlwaysDead;

    //private TriggerRestart triggerRestart;
    private int automata;
    private bool onAction;
    // Start is called before the first frame update
    void Start()
    {
       // checkTriggerDead();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerComp!=null)
        {
            if (triggerComp.active)
            {
                Move();
            }
            else
            {
                if (automata==1)
                {
                    Move();
                }
            }

        }else
        {
            Move();
        }

       
    }

    private void Move()
    {
        if (!onAction && automata == 0)
        {
            StartCoroutine("IEMoveOne");

        }else if (isLoop && !onAction && automata==2)
        {
            StartCoroutine("IEMoveTwo");
        }
    }

    IEnumerator IEMoveOne()
    {
        Vector3 dir = transform.position + (dirMove * distance);
        automata = 1;
        onAction = true;
        
        while (Vector3.Distance(transform.position,dir)>0.1f)
        {
            transform.position = Vector3.Lerp(transform.position,dir,Time.deltaTime * speedMoveOne);          
            yield return null;
        }

        StartCoroutine("delayNextMove");
        //Debug.Log("finished");
    }

    IEnumerator IEMoveTwo()
    {
        Vector3 dir = transform.position - (dirMove * distance);
        onAction = true;
        while (Vector3.Distance(transform.position, dir) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, dir, Time.deltaTime * speedMoveTwo);
            yield return null;
        }

        StartCoroutine("delayNextMove");
        //Debug.Log("finished");
    }

    IEnumerator delayNextMove()
    {
        yield return new WaitForSeconds(timeToNextAction);

        if (automata==1)
        {
            automata = 2;

        }else if (automata==2)
        {
            automata = 0;
        }

        onAction = false;

    }

    /*void checkTriggerDead()
    {
        Transform[] tDead = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in tDead)
        {
            if (t.tag=="triggerDead")
            {
                triggerRestart = t.GetComponent<TriggerRestart>();
            }
        }
    }*/
}
