using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    public Animator[] animatores;

   public Animator getAnimator(string nameAnimator)
    {
        if (nameAnimator=="super")
        {
           return  animatores[0];

        }else if (nameAnimator=="other")
        {

        }

        return animatores[0];
    }


}
