using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soulsImg : MonoBehaviour
{
   private bool active;
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void setActive(bool _active)
    {
        active = _active;

        if (active)
        {
            img.color = Color.cyan;
        }
    }

    public bool isActived()
    {
        return active;
    }
}
