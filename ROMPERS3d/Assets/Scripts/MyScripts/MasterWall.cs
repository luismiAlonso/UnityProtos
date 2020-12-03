using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWall : MonoBehaviour
{
    public enum DirWall { top, down, left, right }
    public DirWall dirwall;
    public int idCode;
    public float TimeResetRomper;
    List<CheckWall> listCheckWall = new List<CheckWall>();
    public List<MasterWall> listMasterWall = new List<MasterWall>();

    [SerializeField]
    private bool isGroup;

    // Start is called before the first frame update
    void Awake()
    {
        setActivaTeWalls();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void setIsGroup(bool _isGroup)
    {
        isGroup = _isGroup;
    }
   
    public void UnparentChilds()
    {
        if (listMasterWall.Count > 0)
        {
            foreach (MasterWall mw in listMasterWall)
            {
                mw.listMasterWall = new List<MasterWall>();
                mw.transform.parent = null;
                mw.setIsGroup(false);
                mw.setActivaTeWalls();
                mw.transform.parent = GeneratorLevel.instance.getSuperParentGrid().transform;
                // mw.GetComponent<MasterWall>().enabled = false;
            }
        }

    }

    public void EnparentChilds()
    {
        if (listMasterWall.Count>0) {

            GameObject objParent = new GameObject("parentMovilWall");
            objParent.AddComponent<MasterWall>();
            objParent.GetComponent<MasterWall>().dirwall = dirwall;
            objParent.GetComponent<MasterWall>().idCode = idCode;
            objParent.GetComponent<MasterWall>().setIsGroup(true);
            objParent.transform.position = transform.position;
            foreach (MasterWall mw in listMasterWall)
            {
                mw.transform.parent = objParent.transform;
                mw.setIsGroup(true);
                mw.dirwall = objParent.GetComponent<MasterWall>().dirwall;
                objParent.GetComponent<MasterWall>().listMasterWall.Add(mw);
               //mw.GetComponent<MasterWall>().enabled = false;
            }
            transform.parent = objParent.transform;

            objParent.GetComponent<MasterWall>().listMasterWall.Add(this);
            objParent.GetComponent<MasterWall>().setActivaTeWalls();
            objParent.transform.parent = GeneratorLevel.instance.getSuperParentGrid().transform;
            GetComponent<MasterWall>().dirwall = objParent.GetComponent<MasterWall>().dirwall;
            setIsGroup(true);
            //GetComponent<MasterWall>().enabled = false;
        }
        else
        {
            setIsGroup(false);
            setActivaTeWalls();
            Debug.Log("You need add node to the listMasterWall, select any MasterWall and put same idCode");
        }
    }
    

    public void setActivaTeWalls()
    {
        Transform[] movilWalls = transform.GetComponentsInChildren<Transform>(true);

        foreach (Transform mw in movilWalls)
        {

            if (mw.name == "leftWall" && dirwall != DirWall.left)
            {
                mw.gameObject.SetActive(false);
            }
            else if (mw.name == "leftWall" && dirwall == DirWall.left)
            {
                mw.gameObject.SetActive(true);
               /* mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                mw.GetComponent<CheckWall>().wallNode.setTimeToResetRomper(TimeResetRomper);*/
                listCheckWall.Add(mw.GetComponent<CheckWall>());
            }

            if (mw.name == "rightWall" && dirwall != DirWall.right)
            {
                mw.gameObject.SetActive(false);
            }
            else if (mw.name == "rightWall" && dirwall == DirWall.right)
            {
                mw.gameObject.SetActive(true);
               /* mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                mw.GetComponent<CheckWall>().wallNode.setTimeToResetRomper(TimeResetRomper);*/

                listCheckWall.Add(mw.GetComponent<CheckWall>());

            }

            if (mw.name == "topWall" && dirwall != DirWall.top)
            {
                mw.gameObject.SetActive(false);
            }
            else if (mw.name == "topWall" && dirwall == DirWall.top)
            {
                mw.gameObject.SetActive(true);
               /* mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                mw.GetComponent<CheckWall>().wallNode.setTimeToResetRomper(TimeResetRomper);*/

                listCheckWall.Add(mw.GetComponent<CheckWall>());

            }

            if (mw.name == "downWall" && dirwall != DirWall.down)
            {
                mw.gameObject.SetActive(false);
            }
            else if (mw.name == "downWall" && dirwall == DirWall.down)
            {
                mw.gameObject.SetActive(true);
                /*mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                mw.GetComponent<CheckWall>().wallNode.setTimeToResetRomper(TimeResetRomper);*/

                listCheckWall.Add(mw.GetComponent<CheckWall>());

            }

        }
    }

    public void fallwallNodes(Vector3 pointContact)
    {

        //Debug.Log(listCheckWall.Count +" : " + pointContact);
        List<CheckWall> posibleWallFall = new List<CheckWall>();
        string dirFall = "";

        for (int i=0;i< listCheckWall.Count;i++) {

           /* if (pointContact.x!=0 && pointContact.x > 0 && !listCheckWall[i].getCollisionCheckWall("front"))
            {
                posibleWallFall.Add(listCheckWall[i]);
                dirFall = "front";
               // Debug.Log("frontx");
            }
            else if(pointContact.x != 0 && pointContact.x < 0 && !listCheckWall[i].getCollisionCheckWall("back"))
            {
                posibleWallFall.Add(listCheckWall[i]);
                dirFall = "back";
               // Debug.Log("backx");

            }
            else if (pointContact.z != 0 && pointContact.z > 0 && !listCheckWall[i].getCollisionCheckWall("front"))
            {
                posibleWallFall.Add(listCheckWall[i]);
                dirFall = "front";
                Debug.Log("frontz");

            }
            else if (pointContact.z != 0 && pointContact.z < 0 && !listCheckWall[i].getCollisionCheckWall("back"))
            {
                posibleWallFall.Add(listCheckWall[i]);
                dirFall = "back";
                Debug.Log("backz");

            }*/
        }

        if (posibleWallFall.Count == listCheckWall.Count) {

          /*  foreach (CheckWall wn in posibleWallFall)
            {
                wn.wallNode.fallWall(dirFall);
            }*/
        }
    }

    

   
}
