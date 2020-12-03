using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{

    // [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float fallOffsetGravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;

    [SerializeField] private bool activeGravity = true;

    CharacterController charController;
    PlayerMove playerMove;
    PlayerController playerControl;
    bool OnJumping;
    bool OnStartStump;
    bool OnThrowUP;
    Vector3 playerVelocity;
    Coroutine coNPC;
    Vector3 jump;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerController>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(charController.isGrounded+" - "+transform.name);
        if (activeGravity) {
            gravityFall();
        }
        
        if (isThrowing())
        {
            Vector3 dir = transform.forward;
            charController.Move(new Vector3(dir.x, playerVelocity.y, dir.z) * 6 * Time.deltaTime);

        }
    }

    public void throwUP()
    {
            OnThrowUP = true;
            OnJumping = true;
            /*
            if (playerControl.setEffects.GetSX("sxJump") != null)
            {
                playerControl.setEffects.GetSX("sxJump").Play();
            }
            if (playerControl.setEffects.GetFX("fxJump") != null)
            {
                playerControl.setEffects.GetFX("fxJump").Play();
            }*/

            playerVelocity.y = 8;
            Debug.Log("throw");
    }

    public void JumpInput()
    {
        if (InputControl.instance.getButtonsControl("Button0") && playerControl.checkers.canJump 
            && charController.isGrounded && !OnJumping)
        {
            OnJumping = true;
            /*
            if (playerControl.setEffects.GetSX("sxJump") != null)
            {
                playerControl.setEffects.GetSX("sxJump").Play();
            }
            if (playerControl.setEffects.GetFX("fxJump") != null)
            {
                playerControl.setEffects.GetFX("fxJump").Play();
            }*/

            playerVelocity.y = jumpHeight;
            // StartCoroutine(JumpEvent());
        }

       
    }

    void gravityFall()
    {
        
        //Debug.Log("char: "+charController.isGrounded+" my:"+playerControl.checkers.isGrounded);
        if (OnJumping)
        {
            playerVelocity.y -= gravity * Time.deltaTime;
            charController.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
           
            if (!charController.isGrounded)
            {
                playerVelocity.y -= (gravity - fallOffsetGravity) * Time.deltaTime;               
                charController.Move(playerVelocity * Time.deltaTime);
            }
        }
        if (charController.isGrounded)
        {
            OnJumping = false;

            if (OnThrowUP)
            {

                OnThrowUP = false;

                playerControl.GetComponent<JumpControl>().enabled = false;

            }
        }

    }

    public void setGravity(bool active)
    {
        activeGravity = active;
    }

    public Vector3 getYJump()
    {
        return playerVelocity;
    }

    public bool isJumping()
    {
        return OnJumping;
    }

    public bool isThrowing()
    {
        return OnThrowUP;
    }

    public void stumpNPC()
    {
        StartCoroutine("IstumpNPC");
    }

    IEnumerator IstumpNPC()
    {
        StopCoroutine(coNPC);

        while (OnStartStump)
        {
            playerVelocity.y -= 50 * Time.deltaTime;
            charController.Move(playerVelocity * Time.deltaTime);

            if (charController.isGrounded)
            {
               // simpleIA.settinAtack.ArmaMelee[simpleIA.settinAtack.indexArma].hitMelee();
                OnStartStump = false;
                CameraControl.instance.setState(2);
            }

            yield return null;
        }
    }

    IEnumerator IstarStump()
    {
        while (OnStartStump)
        {
            playerVelocity.y -= gravity * Time.deltaTime;
            charController.Move(playerVelocity * Time.deltaTime);

            if (charController.isGrounded)
            {               
                OnStartStump = false;
            }

            yield return null;
        }
    }

    public void StartStump()
    {
        OnStartStump = true;

       /* if (playerControl.setEffects.GetSX("sxJump") != null)
        {
            playerControl.setEffects.GetSX("sxJump").Play();
        }
        if (playerControl.setEffects.GetFX("fxJump") != null)
        {
            playerControl.setEffects.GetFX("fxJump").Play();
        }*/
        playerVelocity.y = jumpHeight;

        coNPC = StartCoroutine("IstarStump");
    }

    public void remoteJump(float jumpMod)
    {
        OnJumping = true;

        /* if (playerControl.setEffects.GetSX("sxJump") != null)
         {
             playerControl.setEffects.GetSX("sxJump").Play();
         }
         if (playerControl.setEffects.GetFX("fxJump") != null)
         {
             playerControl.setEffects.GetFX("fxJump").Play();
         }*/

        playerVelocity.y = jumpMod;

       
    }

   
}
