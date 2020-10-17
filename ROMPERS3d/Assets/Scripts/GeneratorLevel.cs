﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLevel : MonoBehaviour
{
    #region Singleton
    private static GeneratorLevel generatorLevel;
    public static GeneratorLevel instance
    {
        get
        {
            if (generatorLevel == null)
            {
                generatorLevel = FindObjectOfType<GeneratorLevel>();
            }
            return generatorLevel;
        }
    }

    #endregion Singleton

    public MapNode[] mapsNodeToGenerate;
    public Vector3 offsetCenterMap;

    public int indexLevel;

    List<Vector3> listPoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMap()
    {
        GameObject newEmptyGameObject = new GameObject("Grid");
        // following line is probably not neccessary

        int widthMap = mapsNodeToGenerate[indexLevel].getMapLayer("grounds").width;
        int heightMap = mapsNodeToGenerate[indexLevel].getMapLayer("grounds").height;

        //Grounds
       
        for (int x = 0; x < widthMap; x++)
        {
            for (int y = 0; y < heightMap; y++)
            {
                GameObject cloneG = createGround(x,y, mapsNodeToGenerate[indexLevel]);
                if (cloneG != null)
                {
                   cloneG.transform.parent = newEmptyGameObject.transform;
                   Vector3 positions = new Vector3(cloneG.transform.position.x, 0, cloneG.transform.position.z);
                   listPoints.Add(positions);
                }

                GameObject cloneW=createWalls(x, y, mapsNodeToGenerate[indexLevel]);
                if (cloneW!=null)
                {
                    cloneW.transform.parent= newEmptyGameObject.transform;
                }

                GameObject cloneI=createItems(x, y, mapsNodeToGenerate[indexLevel]);
                if (cloneI!=null)
                {
                    cloneI.transform.parent = newEmptyGameObject.transform;

                }
                GameObject cloneE=createEnemies(x, y, mapsNodeToGenerate[indexLevel]);
                if (cloneE!=null)
                {
                    cloneE.transform.parent = newEmptyGameObject.transform;

                }
            }


        }
        newEmptyGameObject.transform.position = offsetCenterMap;


    }

    GameObject createGround(int x, int y, MapNode mpnd)
    {
        Color color = mpnd.getMapLayer("grounds").GetPixel(x,y);

        GameObject go = null;

        if (color.a==0)
        {
            //void
        }

        foreach (ColorToPrefab cp in mpnd.grounsPrefabs)
        {
            if (cp.color.Equals(color))
            {
                Vector3 pos = new Vector3(x, cp.prefab.transform.position.y, y);
                go= Instantiate(cp.prefab,pos,Quaternion.identity);
            }
        }

        return go;
    }

    GameObject createWalls(int x,int y,  MapNode mpnd)
    {
        Color color = mpnd.getMapLayer("walls").GetPixel(x,y);
        GameObject go = null;

        if (color.a == 0)
        {
            //void
        }
        else
        {
            foreach (ColorToPrefab cp in mpnd.wallPrefabs)
            {
                if (cp.color.Equals(color))
                {
                    Vector3 pos = new Vector3(x, cp.prefab.transform.position.y, y);
                    go = Instantiate(cp.prefab, pos, Quaternion.identity);

                }
               
            }
        }

        return go;
    }

    GameObject createItems(int x, int y,  MapNode mpnd)
    {
        Color color = mpnd.getMapLayer("items").GetPixel(x, y);
        GameObject go = null;

        if (color.a == 0)
        {
            //void
        }
        else
        {
            foreach (ColorToPrefab cp in mpnd.itemsPrefabs)
            {
                if (cp.color.Equals(color))
                {
                    Vector3 pos = new Vector3(x, cp.prefab.transform.position.y, y);
                    go=Instantiate(cp.prefab, pos, Quaternion.identity);
                }
            }
        }

        return go;
    }

    GameObject createEnemies(int x, int y,  MapNode mpnd)
    {
        Color color = mpnd.getMapLayer("enemies").GetPixel(x, y);
        GameObject go = null;

        if (color.a == 0)
        {
            //void
        }
        else
        {
            foreach (ColorToPrefab cp in mpnd.EnemiesPrefabs)
            {
                if (cp.color.Equals(color))
                {
                    Vector3 pos = new Vector3(x, cp.prefab.transform.position.y, y);
                    go=Instantiate(cp.prefab, pos, Quaternion.identity);
                }
            }
        }
        return go;
    }
}