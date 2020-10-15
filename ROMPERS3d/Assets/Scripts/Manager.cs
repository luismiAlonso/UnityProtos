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
    public PlayerController playerControl;
    public bool GlobalUsePad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
