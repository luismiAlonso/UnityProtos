using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWall 
{
     string getTypeWall();
     void DesgasteWall(float value);
     Vector3 getTimeScale();
     Vector3 getMaxIncrement();
     Vector3 getOriginalScale();
     Animator getAnim();
     void growUpWall();
     bool isActiveWall();
     void ActiveSpecial();
}
