using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyChange : MonoBehaviour
{
    public bool isPlayer;
    [HideInInspector]
    public bool dominate;
    [HideInInspector]
    public bool expulsion;
    [HideInInspector]
    public bool activeSouls;
    [HideInInspector]
    public bool isThrowing;

    public float heightToExpulsion;
    public float timeToFxBodyChange = 0.3f;
    public float damageAdsorveLife;
    public float radiusBodyChange;
    public Transform locateCollision;
    public LayerMask layerBodyChange;

    PlayerControl myPlayerControl;
    CharacterController charController;
    PlayerControl dominatorPlayerControl;
    BodyChange bodyChangeControl;
    GrabObject grabObject;
    // KinemaControl kinemaControl;
    SimpleIA simpleIA;
    Checkers checkers;
    Vector3 originalScale;
    bool sleep;
    bool stunHit;

    private void Awake()
    {
        myPlayerControl = GetComponent<PlayerControl>();
        charController = GetComponent<CharacterController>();
        grabObject = GetComponent<GrabObject>();
        // kinemaControl = GetComponent<KinemaControl>();
        checkers = myPlayerControl.checkers;
        if (!isPlayer)
        {
            simpleIA = GetComponent<SimpleIA>();
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
        currentState();
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
            myPlayerControl.controlInteract.settingLife(myPlayerControl.setthing.life - damageAdsorveLife);

            if (myPlayerControl.setEffects.GetFX("fxDamageBlood") != null)
            {
                myPlayerControl.setEffects.PlayFx("fxDamageBlood");
            }
            if (myPlayerControl.setEffects.GetSX("sxDamageBlood") != null)
            {
                myPlayerControl.setEffects.GetSX("sxDamageBlood").Play();
            }

            if (myPlayerControl.setthing.life <= 0)
            {

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

    void currentState()
    {
        if (dominate && !isPlayer)
        {
            dominatorPlayerControl.controlInteract.settingDamageManaGlobal();

            if (dominatorPlayerControl.setthing.mana <= 0)
            {
                prepareToExpulsion();
            }
            myPlayerControl.enabled = true;
            // Debug.Log("Si estoy dominado");

        }
        else if (!dominate && !isPlayer)
        {
            // 
            myPlayerControl.enabled = false;
            // Debug.Log("no estoy dominado");
        }
    }

    void checkBodyChange()
    {

        if (!charController.isGrounded && isPlayer && !checkers.isDead && myPlayerControl.setthing.mana > 0)
        {

            Collider[] hitColliders = Physics.OverlapSphere(locateCollision.position, radiusBodyChange, layerBodyChange);

            foreach (var hitCollider in hitColliders)
            {

                if (!myPlayerControl.checkers.isCaptured && !dominate)
                {

                    if (hitCollider.GetComponent<SimpleIA>() != null &&
                        !hitCollider.GetComponent<SimpleIA>().getDetectado() &&
                        !hitCollider.GetComponent<BodyChange>().sleep &&
                        !hitCollider.GetComponent<BodyChange>().isThrowing  &&
                        hitCollider.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.normal)
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
                        if (hitCollider.GetComponent<SetEffects>().GetSX("sxCaptureNPC") != null)
                        {
                            hitCollider.GetComponent<SetEffects>().GetSX("sxCaptureNPC").Play();
                        }
                        hitCollider.GetComponent<JumpControl>().enabled = true;
                        hitCollider.GetComponent<BodyChange>().simpleIA.getNavMeshAgent().enabled = false;
                        //hitCollider.GetComponent<Rigidbody>().isKinematic = true;
                        hitCollider.GetComponent<BodyChange>().simpleIA.enabled = false;
                        hitCollider.GetComponent<BodyChange>().dominate = true;
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.isDominated = true;
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.remoteControl = true;
                        hitCollider.GetComponent<BodyChange>().setbodyChangeControl(this);
                        hitCollider.GetComponent<BodyChange>().setPlayerControlDominator(myPlayerControl);
                        hitCollider.GetComponent<BodyChange>().delayToControl(hitCollider.transform.gameObject.GetComponent<PlayerControl>());
                        CameraControl.instance.setTarget(hitCollider.transform);

                    }
                    else if (hitCollider.GetComponent<SimpleIA>() != null &&
                       !hitCollider.GetComponent<SimpleIA>().getDetectado() &&
                       (hitCollider.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.clero
                       || hitCollider.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.tanque) &&
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
                        if (hitCollider.GetComponent<SetEffects>().GetSX("sxCaptureNPC") != null)
                        {
                            hitCollider.GetComponent<SetEffects>().GetSX("sxCaptureNPC").Play();
                        }

                        /*if (GetComponent<SetEffects>().GetFX("fxJump"))
                        {
                            GetComponent<SetEffects>().noneFx("fxJump");
                        }*/
                        hitCollider.GetComponent<JumpControl>().enabled = true;
                        hitCollider.GetComponent<BodyChange>().simpleIA.getNavMeshAgent().enabled = false;
                        hitCollider.GetComponent<BodyChange>().simpleIA.enabled = false;
                       // hitCollider.GetComponent<Rigidbody>().isKinematic = true;
                        hitCollider.GetComponent<BodyChange>().dominate = true;
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.isDominated = true;
                        hitCollider.transform.gameObject.GetComponent<PlayerControl>().checkers.remoteControl = true;
                        hitCollider.GetComponent<BodyChange>().setbodyChangeControl(this);
                        hitCollider.GetComponent<BodyChange>().setPlayerControlDominator(myPlayerControl);
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
               // transform.GetComponent<Rigidbody>().isKinematic = false;
                transform.GetComponent<JumpControl>().enabled = false;

                if (myPlayerControl.setEffects.GetFX("fxCaptureNPC") != null)
                {
                    myPlayerControl.setEffects.PlayFx("fxCaptureNPC");
                }
            }
        }
      

       /* if (expulsion)
        {
            fxBecomeOriginalPlayer();
        }*/
    }

    void usingAtackDominate()
    {
        if (dominate)
        {

            if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button3") && simpleIA.typeNPC == SimpleIA.TypeNPC.normal)
            {
                prepareToThrowNPC();
            }
            else if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button3") && simpleIA.typeNPC == SimpleIA.TypeNPC.clero)
            {
                simpleIA.settinAtack.Arma[simpleIA.settinAtack.indexArma].ShootingRemote();

            }
            else if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button3")
               && simpleIA.typeNPC == SimpleIA.TypeNPC.tanque && myPlayerControl.characterController.isGrounded && !grabObject.getGrabObject())
            {
                  myPlayerControl.jumpControl.StartStump();

            }
            else if (dominate && !isPlayer && InputControl.instance.getButtonsControl("Button3")
                && simpleIA.typeNPC == SimpleIA.TypeNPC.tanque && !myPlayerControl.characterController.isGrounded)
            {
                myPlayerControl.jumpControl.stumpNPC();

            }
            else if (dominate && !isPlayer && InputControl.instance.getButtonsControlOnPress("Button3")
               && simpleIA.typeNPC == SimpleIA.TypeNPC.tanque && myPlayerControl.characterController.isGrounded && grabObject.getGrabObject()!=null)
            {
                grabObject.grabObject();
                //grab
            }
            else if (dominate && !isPlayer && InputControl.instance.getButtonsControlOnRelease("Button3")
               && simpleIA.typeNPC == SimpleIA.TypeNPC.tanque && myPlayerControl.characterController.isGrounded && grabObject.getGrabObject()!=null)
            {
                //throw         
                grabObject.getGrabObject().GetComponent<JumpControl>().throwUP();
                grabObject.throwObject();
            }
        }
    }

    void prepareToThrowNPC()
    {
        isThrowing = true;
        dominatorPlayerControl.transform.gameObject.SetActive(true);
        dominatorPlayerControl.enabled = true;
        dominatorPlayerControl.checkers.isCaptured = false;
        dominatorPlayerControl.transform.position = new Vector3(transform.position.x, heightToExpulsion, transform.position.z);
        dominatorPlayerControl.jumpControl.remoteJump();
        dominate = false;
        myPlayerControl.checkers.isDominated = false;
        throwNpc();
        CameraControl.instance.setTarget(dominatorPlayerControl.transform);
    }

    void throwNpc()
    {
        StartCoroutine("ThrowIE");
    }


    IEnumerator ThrowIE()
    {
        float time = 0;

        while (time < myPlayerControl.dashControl.timeDash)
        {
            if (!myPlayerControl.checkers.checkCollisionONthrow().checkCollThrow && !stunHit)
            {
                charController.Move(transform.forward * myPlayerControl.dashControl.forceDash * Time.deltaTime);
                time += Time.deltaTime;
            }
            else if(myPlayerControl.checkers.checkCollisionONthrow().checkCollThrow && !stunHit)
            {
                stunHit = true;
                isThrowing = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                myPlayerControl.checkers.checkCollisionONthrow().objColl.GetComponent<ControlInteract>().stunnedNPC(myPlayerControl.checkers.checkCollisionONthrow().objColl.GetComponent<SimpleIA>(), 6);
                //stune self
                time = 0;
                myPlayerControl.checkers.remoteControl = false;
                transform.GetComponent<ControlInteract>().stunnedNPC(simpleIA, 6);
                // StartCoroutine("IEreboteDash", dir);

            }

            yield return null;
        }

        if (!myPlayerControl.checkers.checkCollisionONthrow().checkCollThrow)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            myPlayerControl.checkers.remoteControl = false;
            myPlayerControl.enabled = false;
            simpleIA.getNavMeshAgent().enabled = true;
            stunHit = false;
            isThrowing = false;

        }

    }
    

    public void prepareToExpulsion()
    {
        isThrowing = true;
        simpleIA.getNavMeshAgent().enabled = true;
        simpleIA.enabled = true;
        dominatorPlayerControl.transform.gameObject.SetActive(true);
        dominatorPlayerControl.enabled = true;
        dominatorPlayerControl.checkers.isCaptured = false;
        dominatorPlayerControl.checkers.remoteControl = true;
        //calcular la altura 
        dominatorPlayerControl.transform.position = new Vector3(transform.position.x, heightToExpulsion, transform.position.z);
        dominatorPlayerControl.jumpControl.remoteJump();
        dominatorPlayerControl.checkers.remoteControl = false;
        dominate = false;
        myPlayerControl.checkers.isDominated = false;
        delayToCapture();
        CameraControl.instance.setTarget(dominatorPlayerControl.transform);
    }

    void fxBecomeNPC()
    {
        myPlayerControl.transform.gameObject.SetActive(false);
        transform.position = dominatorPlayerControl.transform.position;
    }

    void fxBecomeOriginalPlayer()
    {
        //Debug.Log(Vector3.Distance(transform.localScale, originalScale));
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

    void delayToCapture()
    {
        StartCoroutine("IdelayToCapture");
    }

    IEnumerator IdelayToCapture()
    {
        yield return new WaitForSeconds(0.1f);
        isThrowing = false;
    }

    private void OnDrawGizmos()
    {
        if (locateCollision != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(locateCollision.position, radiusBodyChange);

        }
    }




}
