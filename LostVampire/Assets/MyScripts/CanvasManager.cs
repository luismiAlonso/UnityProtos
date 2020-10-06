using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager canvasManager;
    public static CanvasManager instance
    {
        get
        {
            if (canvasManager == null)
            {
                canvasManager = FindObjectOfType<CanvasManager>();
            }
            return canvasManager;
        }
    }

    public HealhtBar healhtBar;
    public ManaBar manaBar;

    
}
