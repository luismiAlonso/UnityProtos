using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MakeJiggly : MonoBehaviour
{
    Mesh m;
    Vector3[] vertices;
    Dictionary<SpringJoint, List<int>> vertexJointMap = new Dictionary<SpringJoint, List<int>>();

    void Start()
    {
        m = GetComponent<MeshFilter>().sharedMesh;

        vertices = m.vertices;

        ApplyJoints(vertices.Distinct().ToList());
    }

    void FixedUpdate()
    {
        foreach (KeyValuePair<SpringJoint, List<int>> kvp in vertexJointMap)
        {
            foreach (int index in kvp.Value)
                vertices[index] = kvp.Key.transform.localPosition;
        }

        m.vertices = vertices;
    }

    void ApplyJoints(List<Vector3> positions)
    {
        GameObject go = new GameObject();
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        Rigidbody rb = go.AddComponent<Rigidbody>();

        foreach (Vector3 v in positions)
        {
            GameObject appendage = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            appendage.transform.parent = transform;
            appendage.transform.localScale = Vector3.one;
            appendage.transform.localPosition = v;
            SpringJoint spring = appendage.AddComponent<SpringJoint>();
            spring.spring = 80f;
            spring.damper = 1f;
            spring.minDistance = .1f;
            spring.maxDistance = .3f;
            spring.connectedBody = rb;
            appendage.GetComponent<MeshRenderer>().enabled = false;

            vertexJointMap.Add(spring, new List<int>());
        }

        // add vertices to joint map
        for (int i = 0; i < vertices.Length; i++)
        {
            float nearest = Mathf.Infinity;
            SpringJoint best = null;

            foreach (KeyValuePair<SpringJoint, List<int>> kvp in vertexJointMap)
            {
                float dist = Vector3.Distance(kvp.Key.transform.localPosition, vertices[i]);
                if (dist < nearest)
                {
                    nearest = dist;
                    best = kvp.Key;
                }
            }

            if (best != null)
                vertexJointMap[best].Add(i);
        }
    }
}

