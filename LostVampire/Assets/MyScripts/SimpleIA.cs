using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerControl))]
public class SimpleIA : MonoBehaviour , IatackNPC, Ibehaviuour
{
    public WayPoints[] wayPoits;
    public enum TypeNPC { normal=0,clero=1 };
    public TypeNPC typeNPC;
    public SettingAtacksIA settinAtack;
    public SettingBehaviourIA settinBehaviour;

    private NavMeshAgent agent;
    private int targetPoint;
    private int indexRotation;
    private int indexTimeRotate;
    private float timeRotation;

    private bool canNextRot;
    private bool detectado;
    private bool stop;
    private int state;

    private float distanceAtack;
    private Transform target;

    PlayerControl playerControl;
    SetEffects setEffects;

    private void Start()
    {
        setEffects = GetComponent<SetEffects>();
        agent = GetComponent<NavMeshAgent>();
        playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.fullStop) {

            if (!playerControl.checkers.isDead) {

                stateMachine();
                stopped();
            }
            else
            {
                agent.enabled = false;
                playerControl.transform.eulerAngles = new Vector3(90, 0, 0);
                this.enabled = false;

            }
        }
    }

    public NavMeshAgent getNavMeshAgent()
    {
        return agent;
    }

    public void setState(int _state)
    {
        state = _state;
    }

    void stateMachine()
    {
        if (state == 0)
        {
            PatrullaWayPointsAndStop();

        }
        else if (state == 1)
        {

            if (target != null)
            {

                if (typeNPC == TypeNPC.clero)
                {

                    if (Vector3.Distance(transform.position, target.position) <= settinAtack.distanceAtack && !Manager.instance.playerControl.checkers.isDead)
                    {
                        stop = true;
                        transform.LookAt(target);
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                        actackDistance();
                    }
                    else
                    {
                        stop = false;
                        moveto(target);
                    }

                }else if (typeNPC == TypeNPC.normal )
                {
                    if ((Vector3.Distance(transform.position, target.position) <= settinAtack.distanceAtackMelee) && playerControl.checkers.canAtack)
                    {
                        stop = true;
                        actackMelee();
                        
                    }else if ((Vector3.Distance(transform.position, target.position) <= settinAtack.distanceAtackMelee) && !playerControl.checkers.canAtack)
                    {
                        stop = true;
                        transform.LookAt(target);
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

                    }
                    else
                    {
                        transform.LookAt(target);
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                        stop = false;
                        moveto(target);
                    }
                }

                //ActionRangeAlerta();
            }
        }
        else if (state == 2)
        {
            waitAndGo();
        }

    }

    //incluir dentro de una interfaz bodyChage
    public bool ActionRangeAlerta()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, settinBehaviour.rangeAlerta, settinBehaviour.layerRangeALerta);
        bool check = false;

        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.tag == "PLayer")
            {
                check = true;
            }

            /* else if (hitCollider.tag == "NPC" && hitCollider.transform.GetComponent<BodyChange>().dominate)
             {
                 check = true;
             }*/
        }

        return check;
    }

    void PatrullaWayPointsAndStop()
    {
        
        if (!detectado)
        {
            if (wayPoits != null && wayPoits.Length > 0 && wayPoits[targetPoint] != null)
            {
                movetoPoint(wayPoits[targetPoint].transform.position);

                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
                        {
                            // Done
                            stop = true;
                            rotateToMove();

                        }
                    }
                }
            }
            else
            {

            }
        }
        else
        {
            state = 1;
        }

    }

    void rotateToMove()
    {
        if (!detectado)
        {
            if (canRotate())
            {
                if (indexRotation <= wayPoits[targetPoint].rotatePoints.Length - 1)
                {
                    transform.eulerAngles = Util.AngleLerp(transform.rotation.eulerAngles, wayPoits[targetPoint].rotatePoints[indexRotation], timeRotation); //Vector3.Lerp(transform.rotation.eulerAngles, wayPoits[targetPoint].rotatePoints[indexRotation], Time.deltaTime * speedRot);
                    timeRotation += wayPoits[targetPoint].timeToRotation[indexRotation] * Time.deltaTime;

                    if (timeRotation > 1.0f)
                    {
                        indexRotation++;
                        indexTimeRotate++;
                        timeRotation = 0.0f;
                    }

                }
                else
                {
                    stop = false;
                    indexTimeRotate = 0;
                    indexRotation = 0;

                    if (targetPoint < wayPoits.Length - 1)
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
            stop = false;
            state = 1;
        }

    }

    void PatrullaWayPointsNonStop()
    {
        if (!detectado)
        {
            movetoPoint(wayPoits[targetPoint].transform.position);

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // Done
                        if (targetPoint >= wayPoits.Length - 1)
                        { // if it's a last point
                            targetPoint = 0;
                        }
                        else
                        {
                            targetPoint++;
                        }

                    }
                }
            }

        }
        else
        {
            state = 1;
            stop = true;
        }
    }

    private bool canRotate()
    {
        if (!detectado) {

            if (indexTimeRotate <= wayPoits[targetPoint].timeToRotation.Length - 1)
            {

                if (wayPoits[targetPoint].timeToAuxRotation.Length > 0) {

                    if (wayPoits[targetPoint].timeToAuxRotation[indexTimeRotate] <= 0)
                    {
                        canNextRot = true;
                    }
                    else
                    {
                        wayPoits[targetPoint].timeToAuxRotation[indexTimeRotate] -= Time.deltaTime;
                        canNextRot = false;
                    }

                }
            }
            else
            {
                wayPoits[targetPoint].resetValues();//reset time

            }
        }
        else
        {
            canNextRot = false;
        }


        return canNextRot;
    }

    public void stopped()
    {
        if (!stop)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public void setStop(bool _stop)
    {
        agent.isStopped = _stop;
    }

    public void moveto(Transform pos)
    {

        if (!stop)
        {
            agent.SetDestination(pos.position);
        }
    }

    public void movetoPoint(Vector3 pos)
    {

        if (!stop)
        {
            agent.SetDestination(pos);
        }

    }

    public void setDetectado(bool _detectado)
    {
        Manager.instance.playerControl.checkers.isDetectado = _detectado;
        detectado = _detectado;
    }

    public bool getDetectado()
    {
        return detectado;
    }

    public void setTarget(Transform _target)
    {
        target = _target;
    }

    public void actackDistance()
    {
        if (typeNPC==TypeNPC.clero && playerControl.checkers.canAtack)
        {
            StartCoroutine("IatackDistance");

        }else if (typeNPC == TypeNPC.normal && playerControl.checkers.canAtack)
        {

        }
    }

    IEnumerator IatackDistance()
    {
        settinAtack.Arma[settinAtack.indexArma].shooting();
        playerControl.checkers.canAtack = false;
        yield return new WaitForSeconds(settinAtack.timeUseArmaDistancia);
        playerControl.checkers.canAtack = true;
    }

    public void actackMelee()
    {
        //throw new System.NotImplementedException();
        if (typeNPC == TypeNPC.clero && playerControl.checkers.canAtack)
        {
            // StartCoroutine("IatackDistance");

        }
        else if (typeNPC == TypeNPC.normal && playerControl.checkers.canAtack)
        {
            settinAtack.ArmaMelee[settinAtack.indexArma].hitMelee();

            if (setEffects.GetFX("fxCupule") !=null) {
                setEffects.GetFX("fxCupule").Play();
            }

            playerControl.checkers.canAtack = false;
        }
    }

    public void resetAreaMelee()
    {
        
    }

    public void runAway()
    {
        //throw new System.NotImplementedException();
    }

    public void waitAndGo()
    {
        //throw new System.NotImplementedException();
        if (typeNPC == TypeNPC.normal)
        {
            StartCoroutine("IwaitAndGo");

        }else if (typeNPC == TypeNPC.clero)
        {
            StartCoroutine("IwaitAndGo");
        }
    }

    IEnumerator IwaitAndGo()
    {
        stop = true;
        yield return new WaitForSeconds(settinBehaviour.TimeWaitToNext);
        stop = false;
        //return to patruye
        state = 0;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, settinBehaviour.rangeAlerta);
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag=="Player")
        {

            setDetectado(true);
            setTarget(collision.transform);
            transform.LookAt(collision.transform);
            transform.eulerAngles=new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);
            setState(1);
        }
    }

}
