using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    public class CharacterPadInput : CharacterInput
    {
        public override float GetHorizontalMovementInput()
        {
            //Debug.Log(InputControl.instance.getAxisControl().x);
            return InputControl.instance.getAxisControl().x;
        }

        public override float GetVerticalMovementInput()
        {
            //Debug.Log(InputControl.instance.getAxisControl().y);
            return InputControl.instance.getAxisControl().y;
        }

        public override bool IsJumpKeyPressed()
        {
            return  InputControl.instance.getButtonsControl("Button0");
        }
    }
}
