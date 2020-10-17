using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
{
    public wallNode wallNode;
    public CheckFallWall[] checkersWall;
   
    public bool getCollisionCheckWall(string dirFall)
    {
        bool checkWall=false;

        for (int i=0;i<checkersWall.Length;i++)
        {
            if (dirFall == "front" &&  checkersWall[i].dirwall !=CheckFallWall.DirWall.front && checkersWall[i].isCollisionWall())
            {
                checkWall = true;
            }else if (dirFall == "back" && checkersWall[i].dirwall != CheckFallWall.DirWall.back && checkersWall[i].isCollisionWall())
            {
                checkWall = true;
            }
        }

        return checkWall;
    }
}
