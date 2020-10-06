using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public Vector3[] rotatePoints;
    public float[] timeToRotation;
    public Transform instanceSelf;
    [HideInInspector]
    public float[] timeToAuxRotation;

    private void Awake()
    {
        if (timeToRotation.Length!= rotatePoints.Length)
        {
            Debug.LogError("El tamaño de rotatePoints y el tamaño de timeToRotation debe ser el mismo");
        }
        else
        {
            timeToAuxRotation = new float[timeToRotation.Length];
            resetValues();
        }
    }

    private void Start()
    {
        
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
     
    }

    public void resetValues()
    {
        for (int i=0;i< timeToRotation.Length;i++)
        {
            timeToAuxRotation[i] = timeToRotation[i];
        }
    }

   
    
   
}
