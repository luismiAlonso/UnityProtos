using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimateText : MonoBehaviour
{
    public float timeNextChar;
    public Text textM;
    private string buffer;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void setAnimateText(string _text)
    {
        if (textM.IsActive()) {
            StopAllCoroutines();
            StartCoroutine("animaText", _text);
        }
    }

    IEnumerator animaText(string _text)
    {
        textM.text = "";
        buffer = "";
        foreach(char c in _text.ToCharArray())
        {
            buffer +=c;
            yield return new WaitForSeconds(timeNextChar);
        
            textM.text = buffer;
        }
    }
}
