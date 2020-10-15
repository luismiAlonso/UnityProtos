using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapNode 
{
    public Texture2D[] mapsTexture;
    public ColorToPrefab[] wallPrefabs;
    public ColorToPrefab[] grounsPrefabs;
    public ColorToPrefab[] itemsPrefabs;
    public ColorToPrefab[] EnemiesPrefabs;

    public Texture2D getMapLayer(string nameLayer)
    {
        Texture2D textureLayer = null;

        if (nameLayer=="grounds" )
        {
            textureLayer = mapsTexture[0];

        }else if (nameLayer == "walls")
        {
            textureLayer = mapsTexture[1];

        }else if (nameLayer == "items")
        {
            textureLayer = mapsTexture[2];

        }else if (nameLayer == "enemies")
        {
            textureLayer = mapsTexture[3];
        }

        return textureLayer;
    }

}
