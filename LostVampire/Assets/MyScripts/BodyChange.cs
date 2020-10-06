using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyChange : MonoBehaviour
{
    public bool dominate;
    public bool isPlayer;
    public bool capturate;
    public bool expulsion;
    public bool activeSouls;

    public float timeToFxBodyChange=0.3f;
    public float damageAdsorveLife;
    public float radiusBodyChange;
    public Transform locateCollision;
    public LayerMask layerBodyChange;
    PlayerControl myPlayerControl;
    PlayerControl dominatorPlayerControl;
    BodyChange bodyChangeControl;
    KinemaControl kinemaControl;
    SimpleIA simpleIA;

    Checkers checkers;
    Vector3 originalScale;

    private void Awake()
    {
        myPlayerControl = GetComponent<PlayerControl>();
        kinemaControl = GetComponent<KinemaControl>();
        checkers = myPlayerControl.checkers;
        if (!isPlayer)
        {
            myPlayerControl.enabled = false;
            simpleIA = GetComponent<SimpleIA>();
            simpleIA.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {      
        originalScale = transform.localScale;
    }



    // Update is called once per frame
    void Update()
    {
        checkBodyChange();
        reduceMana();
        AdsorveLife();
        usingAtackDominate();
    }

    private void FixedUpdate()
    {
        
        bodyChageBecomeOriginal();
        bodyChageBecomeNPC();

    }

    void AdsorveLife()
    {
        if (dominate && !isPlayer && !checkers.isDead && (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("buttonY")))
        {
            myPlayerControl.controlInteract.settingLife (myPlayerControl.setthing.life - damageAdsorveLife);

            if (myPlayerControl.setthing.life<=0) {
                checkers.isDead = true;
                myPlayerControl.controlInteract.settingLife(0);
                prepareToExpulsion();
            }

            dominatorPlayerControl.controlInteract.settingLife(dominatorPlayerControl.setthing.life + damageAdsorveLife);
        }
    }

    void reduceMana()
    {
        if (dominate && !isPlayer)
        {
            dominatorPlayerControl.controlInteract.settingDamageManaGlobal();
            delayToControl(myPlayerControl);
            if (dominatorPlayerControl.setthing.mana<=0)
            {
                prepareToExpulsion();
            }
        }
        else if(!dominate && !isPlayer)
        {
           // 
        }
    }

    void checkBodyChange()
    {

        if (!checkers.isGroundCheck() && isPlayer && !checkers.isDead && myPlayerControl.setthing.mana  > 0)
        {

            Collider[] hitColliders = Physics.OverlapSphere(locateCollision.position, radiusBodyChange, layerBodyChange);

            foreach (var hitCollider in hitColliders)
            {

                if (!capturate && !expulsion) {

                    if (!hitCollider.GetComponent<SimpleIA>().getDetectado() && hitCollider.GetComponent<SimpleIA>().typeNPC==SimpleIA.TypeNPC.normal) {
                       
                        capturate = true;
                        dominatorPlayerControl = hitCollider.transform.gameObject.GetComponent<PlayerControl>();
                        if (!hitCollider.GetComponent<BodyChange>().activeSouls)
                        {
                            hitCollider.GetComponent<BodyChange>().activeSouls = true;
                            ControlSouls.instance.activeImgSouls();
                        }

                        hitCollider.GetComponent<BodyChange>().simpleIA.getNavMeshAgent().enabled = false;
                        hitCollider.GetComponent<BodyChange>().simpleIA.enabled = false;
                        hitCollider.GetComponent<BodyChange>().dominate = true;
                        hitCollider.GetComponent<BodyChange>().setbodyChangeControl(this);
                        hitCollider.GetComponent<BodyChange>().setPlayerControlDominator(myPlayerControl);
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().enabled = true;
                        hitCollider.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        CameraControl.instance.setTarget(hitCollider.transform);

                    }else if (!hitCollider.GetComponent<SimpleIA>().getDetectado() && hitCollider.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.clero &&
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.isStuned)
                    {

                        capturate = true;
                        dominatorPlayerControl = hitCollider.transform.gameObject.GetComponent<PlayerControl>();
                        if (!hitCollider.GetComponent<BodyChange>().activeSouls)
                        {
                            hitCollider.GetComponent<BodyChange>().activeSouls = true;
                            ControlSouls.instance.activeImgSouls();
                        }

                        hitCollider.GetComponent<BodyChange>().simpleIA.getNavMeshAgent().enabled = false;
                        hitCollider.GetComponent<BodyChange>().simpleIA.enabled = false;
                        hitCollider.GetComponent<BodyChange>().dominate = true;
                        hitCollider.GetComponent<BodyChange>().setbodyChangeControl(this);
                        hitCollider.GetComponent<BodyChange>().setPlayerControlDominator(myPlayerControl);
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().enabled = true;
                        hitCollider.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        CameraControl.instance.setTarget(hitCollider.transform);
                    }
                }
            }
        }
    }

    void setPlayerControlDominator(PlayerControl pcd)
    {
        dominatorPlayerControl = pcd;
    }

    void setbodyChangeControl(BodyChange bdc)
    {
        bodyChangeControl = bdc;
    }

    void bodyChageBecomeNPC()
    {
        if (!dominate && isPlayer && capturate)
        {
            fxBecomeNPC();
            myPlayerControl.enabled = false;

        }
    }

    void bodyChageBecomeOriginal()
    {
        if (dominate) {

            if (myPlayerControl.usePad) { //

                if (dominate && !isPlayer && Input.GetButtonDown("buttonB")) {

                    prepareToExpulsion();

                } 
            }
            else
            {
                if (dominate && !isPlayer && Input.GetButtonDown("fire1"))
                {
                    prepareToExpulsion();
                }
               
            }

        }

        if (expulsion)
        {
            fxBecomeOriginalPlayer();
        }
    }

    void usingAtackDominate()
    {
        if (dominate)
        {

            if (myPlayerControl.usePad)
            {

                if (dominate && !isPlayer && Input.GetButtonDown("buttonA") && simpleIA.typeNPC == SimpleIA.TypeNPC.normal)
                {
                    prepareToThrowNPC();

                }else if (dominate && !isPlayer && Input.GetButtonDown("buttonA") && simpleIA.typeNPC == SimpleIA.TypeNPC.clero)
                {
                    simpleIA.settinAtack.Arma[simpleIA.settinAtack.indexArma].ShootingRemote();
                }
            }
            else
            {
                if (dominate && !isPlayer && Input.GetButtonDown("fire2") && simpleIA.typeNPC == SimpleIA.TypeNPC.clero)
                {
                    prepareToThrowNPC();
                }
                else if (dominate && !isPlayer && Input.GetButtonDown("buttonA") && simpleIA.typeNPC == SimpleIA.TypeNPC.clero)
                {
                    simpleIA.settinAtack.Arma[simpleIA.settinAtack.indexArma].ShootingRemote();
                }
            }

        }
    }
    
    void prepareToThrowNPC()
    {
        //myPlayerControl.enabled = false;
        //simpleIA.getNavMeshAgent().enabled = true;
        //simpleIA.enabled = true;
        myPlayerControl.checkers.remoteControl = true;
        dominatorPlayerControl.transform.gameObject.SetActive(true);
        dominatorPlayerControl.enabled = true;
        bodyChangeControl.capturate = false;
        bodyChangeControl.expulsion = true;
        dominatorPlayerControl.transform.position = transform.position; // new Vector3(transform.position.x, 2.5f, transform.position.z);
        ThrowNPC();
        checkers.canJump = false;
        checkers.canJump = true;
        dominate = false;
        CameraControl.instance.setTarget(dominatorPlayerControl.transform);
    }

    void ThrowNPC()
    {
        Vector3 normaLizeDir = Vector3.zero;
        Vector3 dir = Vector3.zero;
        normaLizeDir = transform.forward * myPlayerControl.setthing.distanceDash;
        normaLizeDir.y = 0.0f;
        StartCoroutine("ThrowIE", normaLizeDir);
    }
    
    IEnumerator ThrowIE(Vector3 dir)
    {
        /*myPlayerControl.setthing.distanceToBlockingDash = Vector3.Distance(transform.position, dir);

        while (myPlayerControl.setthing.distanceToBlockingDash >= 0.5f)
        {
            myPlayerControl.setthing.distanceToBlockingDash = Vector3.Distance(transform.position, dir);
            float step = myPlayerControl.setthing.forceDash * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dir, step);
            yield return null;
        }*/

        float time = myPlayerControl.setthing.TimeDurationDash;

        while (time > 0)
        {
            if (!myPlayerControl.checkers.checkCollisionONthrow().checkCollThrow)
            {
                myPlayerControl.GetRigidbody().MovePosition(transform.position + dir * Time.deltaTime * myPlayerControl.setthing.forceDash);
                time -= Time.deltaTime;
            }
            else
            {
                myPlayerControl.checkers.checkCollisionONthrow().objColl.GetComponent<ControlInteract>().stunnedNPC(myPlayerControl.checkers.checkCollisionONthrow().objColl.GetComponent<SimpleIA>(), 6);
                time =0 ;
            }
            yield return null;
        }

         myPlayerControl.GetRigidbody().velocity = Vector3.zero;
         myPlayerControl.GetRigidbody().isKinematic = true;
         myPlayerControl.checkers.remoteControl = false;
         myPlayerControl.GetRigidbody().velocity = Vector3.zero;
         myPlayerControl.enabled = false;
         simpleIA.getNavMeshAgent().enabled = true;
         simpleIA.enabled = true;
    }


    public void prepareToExpulsion()
    {
        myPlayerControl.enabled = false;
        simpleIA.getNavMeshAgent().enabled = true;
        simpleIA.enabled = true;
        dominatorPlayerControl.transform.gameObject.SetActive(true);
        dominatorPlayerControl.enabled = true;
        bodyChangeControl.capturate = false;
        bodyChangeControl.expulsion = true;
        //calcular la altura 
        dominatorPlayerControl.transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
        checkers.canJump = false;
        dominate = false;
        dominatorPlayerControl.remoteFasterJump();
        checkers.canJump = true;
        CameraControl.instance.setTarget(dominatorPlayerControl.transform);
    }

    void fxBecomeNPC()
    {

        if (Vector3.Distance(transform.localScale, Vector3.zero) > 0.1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * timeToFxBodyChange);
            transform.position = Vector3.Lerp(transform.position, dominatorPlayerControl.transform.position, Time.deltaTime * timeToFxBodyChange);

            //transform.position = dominatorPlayerControl.transform.position;
        }
        else
        {
            myPlayerControl.transform.gameObject.SetActive(false);
        }

    }

    void fxBecomeOriginalPlayer()
    {
        if (Vector3.Distance(transform.localScale, originalScale) > 0.1f)
        {
             transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * timeToFxBodyChange);
        }
        else
        {
            expulsion = false;
        }
        
    }

    void delayToControl(PlayerControl pc)
    {
        StartCoroutine("delayCanControl", pc);
    }

    IEnumerator delayCanControl(PlayerControl pc)
    {
        pc.checkers.canMove = false;
        yield return new WaitForSeconds(0.55f);
        pc.checkers.canMove = true;
           
    }

    private void OnDrawGizmos()
    {
        if (locateCollision!=null) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(locateCollision.position, radiusBodyChange);
           
        }
    }


}
