using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyChange : MonoBehaviour
{
    public bool dominate;
    public bool isPlayer;
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
   // KinemaControl kinemaControl;
    SimpleIA simpleIA;
    SetEffects setEffects;
    Checkers checkers;
    Vector3 originalScale;
    bool sleep;

    private void Awake()
    {
        myPlayerControl = GetComponent<PlayerControl>();
        setEffects = GetComponent<SetEffects>();
       // kinemaControl = GetComponent<KinemaControl>();
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
        if (dominate && !isPlayer && !checkers.isDead && !sleep && InputControl.instance.getButtonsControl("Button0"))
        {
            myPlayerControl.controlInteract.settingLife (myPlayerControl.setthing.life - damageAdsorveLife);

            if (setEffects.GetFX("fxDamageBlood")!=null) {
                setEffects.PlayFx("fxDamageBlood");
            }
            if (myPlayerControl.setthing.life<=0) {

                myPlayerControl.controlInteract.settingLife(0);
                prepareToExpulsion();
                FalseDead();
            }

            dominatorPlayerControl.controlInteract.settingLife(dominatorPlayerControl.setthing.life + damageAdsorveLife);
        }
    }

    void FalseDead()
    {
        StartCoroutine("timeForLife");
    }

    IEnumerator timeForLife()
    {
        sleep = true;
        transform.eulerAngles = new Vector3(90, 0, 0);
        myPlayerControl.GetRigidbody().useGravity = true;
        simpleIA.getNavMeshAgent().enabled = false;
        simpleIA.enabled = false;
        yield return new WaitForSeconds(2);
        transform.eulerAngles = new Vector3(0, 0, 0);
        myPlayerControl.GetRigidbody().useGravity = false;
        simpleIA.getNavMeshAgent().enabled = true;
        simpleIA.enabled = true;
        sleep = false;
        Debug.Log("fin sleep");
    }

    void reduceMana()
    {
        if (dominate && !isPlayer)
        {
            dominatorPlayerControl.controlInteract.settingDamageManaGlobal();
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

                if (!myPlayerControl.checkers.isCaptured && !expulsion) {

                    if (hitCollider.GetComponent<SimpleIA>()!=null &&  
                        !hitCollider.GetComponent<SimpleIA>().getDetectado() && 
                        !hitCollider.GetComponent<BodyChange>().sleep &&
                        hitCollider.GetComponent<SimpleIA>().typeNPC==SimpleIA.TypeNPC.normal) {

                        myPlayerControl.checkers.isCaptured = true;
                        dominatorPlayerControl = hitCollider.transform.gameObject.GetComponent<PlayerControl>();

                        if (!hitCollider.GetComponent<BodyChange>().activeSouls)
                        {
                            hitCollider.GetComponent<BodyChange>().activeSouls = true;
                            ControlSouls.instance.activeImgSouls();
                        }

                        if (hitCollider.GetComponent<SetEffects>().GetFX("fxCaptureNPC")!=null) {
                            hitCollider.GetComponent<SetEffects>().PlayFx("fxCaptureNPC");
                        }

                        if (GetComponent<SetEffects>().GetFX("fxJump"))
                        {
                            GetComponent<SetEffects>().noneFx("fxJump");
                        }

                        hitCollider.GetComponent<BodyChange>().simpleIA.getNavMeshAgent().enabled = false;
                        hitCollider.GetComponent<BodyChange>().simpleIA.enabled = false;
                        hitCollider.GetComponent<BodyChange>().dominate = true;
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.isDominated = true;
                        hitCollider.GetComponent<BodyChange>().setbodyChangeControl(this);
                        hitCollider.GetComponent<BodyChange>().setPlayerControlDominator(myPlayerControl);
                        hitCollider.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        hitCollider.GetComponent<BodyChange>().delayToControl(hitCollider.transform.gameObject.GetComponent<PlayerControl>());
                        CameraControl.instance.setTarget(hitCollider.transform);

                    }else if (hitCollider.GetComponent<SimpleIA>()!=null && 
                        !hitCollider.GetComponent<SimpleIA>().getDetectado() &&
                        hitCollider.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.clero &&
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.isStuned)
                    {

                        myPlayerControl.checkers.isCaptured = true;
                        dominatorPlayerControl = hitCollider.transform.gameObject.GetComponent<PlayerControl>();

                        if (!hitCollider.GetComponent<BodyChange>().activeSouls)
                        {
                            hitCollider.GetComponent<BodyChange>().activeSouls = true;
                            ControlSouls.instance.activeImgSouls();
                        }

                        if (hitCollider.GetComponent<SetEffects>().GetFX("fxCaptureNPC") != null)
                        {
                            hitCollider.GetComponent<SetEffects>().PlayFx("fxCaptureNPC");
                        }
                        if (GetComponent<SetEffects>().GetFX("fxJump"))
                        {
                            GetComponent<SetEffects>().noneFx("fxJump");
                        }

                        hitCollider.GetComponent<BodyChange>().simpleIA.getNavMeshAgent().enabled = false;
                        hitCollider.GetComponent<BodyChange>().simpleIA.enabled = false;
                        hitCollider.GetComponent<BodyChange>().dominate = true;
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.isDominated = true;
                        hitCollider.GetComponent<BodyChange>().setbodyChangeControl(this);
                        hitCollider.GetComponent<BodyChange>().setPlayerControlDominator(myPlayerControl);
                        hitCollider.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        hitCollider.GetComponent<BodyChange>().delayToControl(hitCollider.transform.gameObject.GetComponent<PlayerControl>());
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

        if (!dominate && isPlayer && myPlayerControl.checkers.isCaptured)
        {
            //Debug.Log("name: " +dominatorPlayerControl.transform.name + "//" + dominate + " " + isPlayer + " " + capturate);
            //ManagerRenderCuller.instance.meshCullerPlayer.enabled = false;
            fxBecomeNPC();
            myPlayerControl.enabled = false;
        }
    }

    void bodyChageBecomeOriginal()
    {
        if (dominate)
        {
            if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button1"))
            {
                prepareToExpulsion();
                if (setEffects.GetFX("fxCaptureNPC") != null)
                {
                    setEffects.PlayFx("fxCaptureNPC");
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

            if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button3") && simpleIA.typeNPC == SimpleIA.TypeNPC.normal)
            {
                prepareToThrowNPC();

                if (setEffects.GetFX("fxDash") != null)
                {
                    setEffects.PlayFx("fxDash");
                }

            }
            else if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button3") && simpleIA.typeNPC == SimpleIA.TypeNPC.clero)
            {
                simpleIA.settinAtack.Arma[simpleIA.settinAtack.indexArma].ShootingRemote();
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
        dominatorPlayerControl.checkers.isCaptured = false;
        bodyChangeControl.expulsion = true;
        dominatorPlayerControl.transform.position = transform.position; // new Vector3(transform.position.x, 2.5f, transform.position.z);
        ThrowNPC();
        checkers.canJump = true;
        dominate = false;
        myPlayerControl.checkers.isDominated = false;
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

                if (setEffects.GetFX("fxDash") != null)
                {
                    setEffects.noneFx("fxDash");
                }
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

        myPlayerControl.GetRigidbody().isKinematic = true;
        myPlayerControl.enabled = false;
        simpleIA.getNavMeshAgent().enabled = true;
        simpleIA.enabled = true;
        dominatorPlayerControl.transform.gameObject.SetActive(true);
        dominatorPlayerControl.enabled = true;
        dominatorPlayerControl.checkers.isCaptured = false;
        bodyChangeControl.expulsion = true;
        //calcular la altura 
        dominatorPlayerControl.transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
        checkers.canJump = false; 
        dominatorPlayerControl.remoteFasterJump();
        dominate = false;
        myPlayerControl.checkers.isDominated = false;
        checkers.canJump = true;
        if (setEffects.GetFX("fxDash") != null)
        {
            setEffects.noneFx("fxDash");
        }
        CameraControl.instance.setTarget(dominatorPlayerControl.transform);
    }

    void fxBecomeNPC()
    {
        myPlayerControl.transform.gameObject.SetActive(false);
        transform.position = dominatorPlayerControl.transform.position;
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
        pc.checkers.remoteControl = true;
        yield return new WaitForSeconds(0.55f);
        pc.checkers.canMove = true;
        pc.enabled = true;

    }

    private void OnDrawGizmos()
    {
        if (locateCollision!=null) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(locateCollision.position, radiusBodyChange);
           
        }
    }


}
