using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IAcontrol))]
[RequireComponent(typeof(NavMeshAgent))]

public class IAcontrol : MonoBehaviour
{
    public enum TypeEnemy { baseE, romperE, bombE, rodeE }
    public TypeEnemy typeEnemy;
    public Transform listWayPoints;


    [SerializeField] private int IdEnemy;
    [SerializeField] private float rangeAction;
    [SerializeField] private float distanceView;
    [SerializeField] private LayerMask layerMask;

    NavMeshAgent agent;
    Vector3 target;
    RaycastHit hitInfo;
    Atack atack;

    private int state;
    private int subState;
    private int targetPoint;
    private int indexRotation;
    private int indexTimeRotate;
    private float timeRotation;
    private WayPoints[] points;
    private bool detectado;
    private bool canNextRot;


    private void Start()
    {
        atack = GetComponent<Atack>();
        agent = GetComponent<NavMeshAgent>();
        setListWayPoints();
    }

    private void Update()
    {
        stateMachine();
    }

    void setListWayPoints()
    {
        if (listWayPoints!=null) {
            points = listWayPoints.GetComponentsInChildren<WayPoints>(true);
        }
    }

    void viewTarget()
    {
        bool check = Physics.Raycast(transform.position, transform.forward, out hitInfo, distanceView, layerMask, QueryTriggerInteraction.UseGlobal);

        if (hitInfo.transform.tag=="Player")
        {
           // target = hitInfo.transform.position;
        }
    }

    void stateMachine()
    {
        if (state == 0) //stop
        {
            stopped();
            alerta();
        }
        else if (state == 1) //patrulla
        {
            alerta();
            patroll();
        }
        else if (state == 2) //action
        {
            alerta();
            doAtack();
        }
    }

    public void setState(int _state)
    {
        state = _state;
    }

    //IA States

    public void stopped()
    {
        agent.isStopped = true;
    }

    public void resume()
    {
        agent.isStopped = false;
    }

    void moveToNextPoint(Vector3 nPoint)
    {
        agent.SetDestination(nPoint);
    }

    private bool canRotate()
    {
        if (!detectado)
        {

            if (indexTimeRotate <= points[targetPoint].timeToRotation.Length - 1)
            {

                if (points[targetPoint].timeToAuxRotation.Length > 0)
                {

                    if (points[targetPoint].timeToAuxRotation[indexTimeRotate] <= 0)
                    {
                        canNextRot = true;
                    }
                    else
                    {
                        points[targetPoint].timeToAuxRotation[indexTimeRotate] -= Time.deltaTime;
                        canNextRot = false;
                    }

                }
            }
            else
            {
                points[targetPoint].resetValues();//reset time

            }
        }
        else
        {
            canNextRot = false;
        }


        return canNextRot;
    }


    void rotateToMove()
    {
        if (!detectado)
        {
            if (canRotate())
            {
                if (indexRotation <= points[targetPoint].rotatePoints.Length - 1)
                {
                    transform.eulerAngles = Util.AngleLerp(transform.rotation.eulerAngles, points[targetPoint].rotatePoints[indexRotation], timeRotation); //Vector3.Lerp(transform.rotation.eulerAngles, wayPoits[targetPoint].rotatePoints[indexRotation], Time.deltaTime * speedRot);
                    timeRotation += points[targetPoint].timeToRotation[indexRotation] * Time.deltaTime;

                    if (timeRotation > 1.0f)
                    {
                        indexRotation++;
                        indexTimeRotate++;
                        timeRotation = 0.0f;
                    }

                }
                else
                {
                    resume();
                    indexTimeRotate = 0;
                    indexRotation = 0;

                    if (targetPoint < points.Length - 1)
                    {
                        targetPoint++;
                    }
                    else
                    {
                        targetPoint = 0;

                    }

                }
            }

        }
        else
        {
            resume();
            state = 1;
        }
    }

    void alerta()
    {
        if (typeEnemy==TypeEnemy.baseE) {

            Collider[] cols = Physics.OverlapSphere(transform.GetChild(0).position, rangeAction, layerMask, QueryTriggerInteraction.Collide);

            if (cols.Length > 0)
            {
                detectado = true;
                setState(2);
            }
            else
            {
                detectado = false;
                setState(1);
            }
        }
    }

    void patroll()
    {
        if (points!=null && points.Length>0 && points[targetPoint] != null && !agent.isStopped)
        {
            moveToNextPoint(points[targetPoint].transform.position);

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
                    {
                        // Done
                        stopped();
                        rotateToMove();

                    }
                }
            }

        }
    }

    void doAtack()
    {
        atack.actacking();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.GetChild(0).position,rangeAction);
    }
}
