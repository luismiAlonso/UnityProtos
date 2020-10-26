using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceWall : MonoBehaviour
{

    public GameObject[] prefWall;

    private int indexWall;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Round(transform.eulerAngles.y));
        if (InputControl.instance.getButtonsControl("Button2"))
        {
            if (Mathf.Round(transform.eulerAngles.y) == 90) {
                GameObject wall = Instantiate(prefWall[indexWall], transform.position + transform.forward, Quaternion.identity);
                wall.GetComponent<MasterWall>().dirwall = MasterWall.DirWall.right;
                wall.GetComponent<MasterWall>().setActivaTeWalls();
            }
            else if (Mathf.Round(transform.eulerAngles.y) == 270)
            {
                GameObject wall = Instantiate(prefWall[indexWall], transform.position + transform.forward, Quaternion.identity);
             
            }
            else if (Mathf.Round(transform.eulerAngles.y) == 180)
            {
                GameObject wall = Instantiate(prefWall[indexWall], transform.position + transform.forward, Quaternion.identity);
                wall.GetComponent<MasterWall>().dirwall = MasterWall.DirWall.down;
                wall.GetComponent<MasterWall>().setActivaTeWalls();

            }
            else if (Mathf.Round(transform.eulerAngles.y) == 0)
            {
                GameObject wall = Instantiate(prefWall[indexWall], transform.position + transform.forward, Quaternion.identity);
                wall.GetComponent<MasterWall>().dirwall = MasterWall.DirWall.top;
                wall.GetComponent<MasterWall>().setActivaTeWalls();

            }
        }
    }
}
