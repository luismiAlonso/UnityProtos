using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    #region Singleton
    private static Manager manager;
    public static Manager instance
    {
        get
        {
            if (manager == null)
            {
                manager = FindObjectOfType<Manager>();
            }
            return manager;
        }
    }
    #endregion Singleton

    public bool fullStop;
    public PlayerControl playerControl;
    public List<Transform> checkPoints = new List<Transform>();
    public bool GlobalUsePad;
    public DataLevel[] levels;

    [HideInInspector]
    public bool onTriggerMenu;

    private int indexCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("waitFineshedComponent");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void pause()
    {
        
    }

    IEnumerator waitFineshedComponent()
    {
        fullStop = true;
        yield return new WaitForSeconds(2.5f);
        fullStop = false;

    }

    public void updateCheckPoint()
    {

    }




}
