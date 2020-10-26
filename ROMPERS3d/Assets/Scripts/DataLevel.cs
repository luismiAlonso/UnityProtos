using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevel: MonoBehaviour
{
    public List<Vector3> listPoints = new List<Vector3>();
    public List<CheckWall> allCheckWalls = new List<CheckWall>();
    public List<IAcontrol> allEnemies = new List<IAcontrol>();


    private void Start()
    {
        Transform[] elementDataLevel = transform.GetComponentsInChildren<Transform>(true);

        if (allEnemies.Count==0) {

            foreach (Transform elem in elementDataLevel)
            {
                if (elem.tag == "Enemy")
                {
                    allEnemies.Add(elem.GetComponent<IAcontrol>());
                }
            }

        }

        if (allCheckWalls.Count==0)
        {
            foreach (Transform elem in elementDataLevel)
            {
                if (elem.GetComponent<CheckWall>()!=null)
                {
                    allCheckWalls.Add(elem.GetComponent<CheckWall>());
                }
            }
        }

        setEnemies();


    }

    public Transform getRandomWall()
    {
        int randIndex = Random.Range(0, allCheckWalls.Count);
        int randDirWall = Random.Range(0, allCheckWalls[randIndex].checkersWall.Length);
        Debug.Log(allCheckWalls[randIndex].checkersWall.Length);
        return allCheckWalls[randIndex].checkersWall[randDirWall].transform;
    }


    public void setEnemies()
    {
        foreach (IAcontrol Ia in allEnemies)
        {
           // Ia.setDataLevel(this);
        }
    }
    
}
