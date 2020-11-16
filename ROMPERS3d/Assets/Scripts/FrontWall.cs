using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWall : MonoBehaviour,IWall
{
    public enum TypeSmaterial { barro, metal, lava, hielo, goma }
    public TypeSmaterial typeSmaterial;
    public float lifeWall;
    public Vector3 TimeScale;
    public Vector3 maxIncrement;

    private Vector3 origScale;

    private void Awake()
    {
        origScale = transform.localScale;
    }

    public void DesgasteWall(float value)
    {
        lifeWall -= value;
        if (lifeWall<0)
        {
            Destroy(gameObject);
        }
       // throw new System.NotImplementedException();
    }

    public string getTypeWall()
    {
        return typeSmaterial.ToString();
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getTimeScale()
    {
        return TimeScale;
    }

    public Vector3 getMaxIncrement()
    {
        return maxIncrement;
    }

    public Vector3 getOriginalScale()
    {
        return origScale;
    }

    public Animator getAnim()
    {
        throw new System.NotImplementedException();
    }
}
