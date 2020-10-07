using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void myStart()
    {
        SceneManager.LoadScene("level0");
    }
}
