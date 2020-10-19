using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MasterWall))]
public class SetMasterWall : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MasterWall myScript = (MasterWall)target;

        if (GUILayout.Button("Create Group walls"))
        {
            myScript.EnparentChilds();
        }

        if (GUILayout.Button("Reset Group walls"))
        {
            myScript.UnparentChilds();
            DestroyImmediate(myScript.gameObject);
        }

    }

}
