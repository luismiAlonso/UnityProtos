using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlIso : MonoBehaviour
{
    [SerializeField]
    private float m_SmoothTime = 0.3f;

    [SerializeField]
    private Transform m_Target;

    private Vector3 m_Velocity = new Vector3(0, 0, 0);

    // Update is called once per frame
    private void Update()
    {
        //cameraFollowIsometricView();
    }

    private void LateUpdate()
    {
        // cameraFollowIsometricView();
        cameraFollowTopview();
    }

    void cameraFollowTopview()
    {
        if (m_Target)
        {
            Vector3 targetPos = m_Target.transform.position;

            //Keep camera height the same
            targetPos.y = transform.position.y;

            //targetPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * m_LerpSpeed);
            targetPos = Vector3.SmoothDamp(transform.position, targetPos, ref m_Velocity, m_SmoothTime);

            transform.position = targetPos;
        }
    }

    void cameraFollowIsometricView()
    {
        float distance = 30;
        transform.position = Vector3.Lerp(transform.position, m_Target.transform.position + new Vector3(-distance, distance, -distance), 0.5f * Time.deltaTime);
        //transform.LookAt(m_Target.transform);
    }
}
