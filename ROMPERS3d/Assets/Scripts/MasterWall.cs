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
                objParent.GetComponent<MasterWall>().listMasterWall.Add(mw);
               //mw.GetComponent<MasterWall>().enabled = false;
            }
            transform.parent = objParent.transform;
            objParent.GetComponent<MasterWall>().listMasterWall.Add(this);
            objParent.GetComponent<MasterWall>().setActivaTeWalls();
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
        Debug.Log(this);
        foreach (Transform mw in movilWalls)
        {
            if (mw.tag=="movilWall")
            {
                if (mw.name== "leftWall" && dirwall!=DirWall.left)
                {
                    mw.gameObject.SetActive(false);
                }
                else if(mw.name == "leftWall" && dirwall == DirWall.left)
                {
                    mw.gameObject.SetActive(true);
                    mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                    mw.GetComponent<CheckWall>().wallNode.masterWall = this;

                    listCheckWall.Add(mw.GetComponent<CheckWall>());
                }

                if (mw.name == "rightWall" && dirwall != DirWall.right)
                {
                    mw.gameObject.SetActive(false);
                }
                else if (mw.name == "rightWall" && dirwall == DirWall.right)
                {
                    mw.gameObject.SetActive(true);
                    mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                    mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                    listCheckWall.Add(mw.GetComponent<CheckWall>());

                }

                if (mw.name == "topWall" && dirwall != DirWall.top)
                {
                    mw.gameObject.SetActive(false);
                }
                else if (mw.name == "topWall" && dirwall == DirWall.top)
                {
                    mw.gameObject.SetActive(true);
                    mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                    mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                    listCheckWall.Add(mw.GetComponent<CheckWall>());

                }

                if (mw.name == "downWall" && dirwall != DirWall.down)
                {
                    mw.gameObject.SetActive(false);
                }
                else if (mw.name == "downWall" && dirwall == DirWall.down)
                {
                    mw.gameObject.SetActive(true);
                    mw.GetComponent<CheckWall>().wallNode.setIdCode(idCode);
                    mw.GetComponent<CheckWall>().wallNode.masterWall = this;
                    listCheckWall.Add(mw.GetComponent<CheckWall>());

                }
            }
        }
    }

    public void fallwallNodes(Vector3 pointContact)
    {
       // Debug.Log(pointContact);

        for (int i=0;i< listCheckWall.Count;i++) {

            if (pointContact.x!=0 && pointContact.x > 0 && !listCheckWall[i].getCollisionCheckWall("front"))
            {
                listCheckWall[i].wallNode.fallWall( "front");
            }
            else if(pointContact.x != 0 && pointContact.x < 0 && !listCheckWall[i].getCollisionCheckWall("back"))
            {
                listCheckWall[i].wallNode.fallWall( "back");
            }
            else if (pointContact.z != 0 && pointContact.z > 0 && !listCheckWall[i].getCollisionCheckWall("front"))
            {
                listCheckWall[i].wallNode.fallWall("front");
            }
            else if (pointContact.z != 0 && pointContact.z < 0 && !listCheckWall[i].getCollisionCheckWall("back"))
            {
                listCheckWall[i].wallNode.fallWall("back");
            }
        }
    }
}
