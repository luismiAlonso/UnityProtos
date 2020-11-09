using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetEffects))]
[RequireComponent(typeof(Animator))]
public class DestructionWall : MonoBehaviour
{

    Animator anim;
    SetEffects setEffects;
    // Start is called before the first frame update
    void Start()
    {
        setEffects = GetComponent<SetEffects>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shakingWall() {
        if (setEffects.GetFX("fxTemblor")!=null)
        {
            setEffects.PlayFx("fxTemblor");
        }
        if (setEffects.GetFX("sxTemblor") != null)
        {
            setEffects.PlayFx("sxTemblor");
        }
        anim.Play("shakingWall");
    }

    public void destroyWall() {

        if (setEffects.GetFX("fxDestroyWall") != null)
        {
            setEffects.PlayFx("fxDestroyWall");
        }
        if (setEffects.GetFX("sxDestroyWall") != null)
        {
            setEffects.PlayFx("sxDestroyWall");
        }

        Destroy(gameObject);
    } 
}
