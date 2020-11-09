using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashControl : MonoBehaviour
{
    public float delayDash;
    public float forceDash;
    public float timeDash;
    //[SerializeField] private KeyCode dashKey;

    private bool OnDash;
    CharacterController charController;
    PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputDash();
    }

    public void inputDash()
    {
        if (!playerControl.checkers.remoteControl && InputControl.instance.getButtonsControl("Button3") && playerControl.checkers.canDash && !OnDash)
        {
            if (playerControl.setEffects.GetSX("sxDash") != null)
            {
                playerControl.setEffects.GetSX("sxDash").Play();
            }
            if (playerControl.setEffects.GetFX("fxDash") != null)
            {
                playerControl.setEffects.GetFX("fxDash").Play();
            }
            StartCoroutine("Idash");
        }
    }

    public void remoteDash()
    {

        if (playerControl.setEffects.GetSX("sxDash") != null)
        {
            playerControl.setEffects.GetSX("sxDash").Play();
        }
        if (playerControl.setEffects.GetFX("fxDash") != null)
        {
            playerControl.setEffects.GetFX("fxDash").Play();
        }
        StartCoroutine("Idash");

    }

    private IEnumerator Idash()
    {
        float time = 0;
        OnDash = true;

        while (time< timeDash) {
            time += Time.deltaTime;
            charController.Move(transform.forward * forceDash * Time.deltaTime);
            OnDash = true;
            yield return null;
        }

        StartCoroutine("DelayDash");
    }

    IEnumerator DelayDash()
    {
        yield return new WaitForSeconds(delayDash);
        OnDash = false;

    }

    public bool isDashing()
    {
        return OnDash;
    }
}
