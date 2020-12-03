using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SuperMaterial 
{
    public enum TypeSmaterial { barro,metal, lava , hielo, goma }
    public TypeSmaterial typeSmaterial;
    public GameObject[] Prefabs;
    public Sprite spriteWallMenu;
}
