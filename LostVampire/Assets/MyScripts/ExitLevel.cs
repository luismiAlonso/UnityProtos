using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "player")
        {
            ControlSouls.instance.resetSouls();
            SceneManager.LoadScene("level"+Manager.instance.indexLevel);
        }
    }
    
}
